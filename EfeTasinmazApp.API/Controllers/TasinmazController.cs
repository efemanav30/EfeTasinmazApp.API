using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EfeTasinmazApp.API.Business.Abstract.Interfaces;
using System;
using Tasinmaz_Proje.Entities;
using Tasinmaz_Proje.Services;
using System.Linq;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazService;
        private readonly IAuthRepository _authRepository;
        private readonly ILogService _logService;

        public TasinmazController(ITasinmazService tasinmazService, IAuthRepository authRepository, ILogService logService )
        {
            _tasinmazService = tasinmazService;
            _authRepository = authRepository;
            _logService = logService;
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim?.Value);
        }

        // GET: api/Tasinmaz
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmazlar()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var isAdmin = await _authRepository.IsAdmin(userEmail);

                if (isAdmin)
                {
                    var tasinmazlar = await _tasinmazService.GetAllAsync();
                    return Ok(tasinmazlar);
                }
                else
                {
                    var tasinmazlar = await _tasinmazService.GetAllByUserIdAsync(userId);
                    return Ok(tasinmazlar);
                }
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    Durum = "Başarısız",
                    IslemTip = "Taşınmazları Getirme",
                    Aciklama = $"Taşınmazları getirirken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = User.FindFirst(ClaimTypes.Role)?.Value ?? "Bilinmiyor"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error getting Tasinmazlar: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: api/Tasinmaz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasinmaz>> GetTasinmaz(int id)
        {
            var tasinmaz = await _tasinmazService.GetByIdAsync(id);
            if (tasinmaz == null)
            {
                return NotFound();
            }
            return Ok(tasinmaz);
        }

        // POST: api/Tasinmaz
        [HttpPost]
        public async Task<ActionResult<Tasinmaz>> AddTasinmaz(Tasinmaz tasinmaz)
        {
            try
            {
                var createdTasinmaz = await _tasinmazService.AddAsync(tasinmaz);
                var log = new Log
                {
                    KullaniciId = tasinmaz.UserId,
                    Durum = "Başarılı",
                    IslemTip = "Taşınmaz Eklendi",
                    Aciklama = $"Taşınmaz ID: {createdTasinmaz.Id} eklendi",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);
                return CreatedAtAction(nameof(GetTasinmaz), new { id = createdTasinmaz.Id }, createdTasinmaz);
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = tasinmaz.UserId,
                    Durum = "Başarısız",
                    IslemTip = "Taşınmaz Ekleme",
                    Aciklama = $"Taşınmaz eklenirken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error adding Tasinmaz: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        // PUT: api/Tasinmaz/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Tasinmaz>> UpdateTasinmaz(int id, Tasinmaz tasinmaz)
        {
            try
            {
                if (tasinmaz == null)
                {
                    return BadRequest();
                }

                var updatedTasinmaz = await _tasinmazService.UpdateAsync(id, tasinmaz);
                var log = new Log
                {
                    KullaniciId = tasinmaz.UserId,
                    Durum = "Başarılı",
                    IslemTip = "Taşınmaz Düzenlendi",
                    Aciklama = $"Taşınmaz ID: {updatedTasinmaz.Id} düzenlendi",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);
                return Ok(updatedTasinmaz);
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = tasinmaz.UserId,
                    Durum = "Başarısız",
                    IslemTip = "Taşınmaz Düzenleme",
                    Aciklama = $"Taşınmaz düzenlenirken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error updating Tasinmaz: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Tasinmaz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasinmaz(int id)
        {
            int userId = GetUserIdFromToken();
            try
            {
                var result = await _tasinmazService.DeleteAsync(id, userId);
                if (!result)
                {
                    return NotFound();
                }

                var log = new Log
                {
                    KullaniciId = userId,
                    Durum = "Başarılı",
                    IslemTip = "Taşınmaz Silindi",
                    Aciklama = $"Taşınmaz ID: {id} silindi",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                return NoContent();
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = userId,
                    Durum = "Başarısız",
                    IslemTip = "Taşınmaz Silme",
                    Aciklama = $"Taşınmaz silinirken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error deleting Tasinmaz: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> SearchTasinmaz([FromQuery] string keyword)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var isAdmin = await _authRepository.IsAdmin(userEmail);

                IEnumerable<Tasinmaz> tasinmazlar;

                if (isAdmin)
                {
                    tasinmazlar = await _tasinmazService.SearchAllAsync(keyword);
                }
                else
                {
                    tasinmazlar = await _tasinmazService.SearchByUserIdAsync(userId, keyword);
                }

                return Ok(tasinmazlar);
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                    Durum = "Başarısız",
                    IslemTip = "Taşınmaz Arama",
                    Aciklama = $"Taşınmaz aranırken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = User.FindFirst(ClaimTypes.Role)?.Value ?? "Bilinmiyor"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error searching Tasinmaz: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
