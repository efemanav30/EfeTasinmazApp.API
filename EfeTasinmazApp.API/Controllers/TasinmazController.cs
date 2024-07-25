using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EfeTasinmazApp.API.Business.Abstract.Interfaces;
using System;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazService;
        private readonly IAuthRepository _authRepository;

        public TasinmazController(ITasinmazService tasinmazService, IAuthRepository authRepository)
        {
            _tasinmazService = tasinmazService;
            _authRepository = authRepository;
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
                // Hata loglaması
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
            var createdTasinmaz = await _tasinmazService.AddAsync(tasinmaz);
            return CreatedAtAction(nameof(GetTasinmaz), new { id = createdTasinmaz.Id }, createdTasinmaz);
        }

        // PUT: api/Tasinmaz/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Tasinmaz>> UpdateTasinmaz(int id, Tasinmaz tasinmazBilgi)
        {
            if (tasinmazBilgi == null)
            {
                return BadRequest();
            }

            var updatedTasinmaz = await _tasinmazService.UpdateAsync(id, tasinmazBilgi);
            return Ok(updatedTasinmaz);
        }

        // DELETE: api/Tasinmaz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasinmaz(int id)
        {
            var result = await _tasinmazService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
