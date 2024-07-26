using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;
using Tasinmaz_Proje.Entities;
using System;
using Tasinmaz_Proje.Services;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public UserController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdUser = await _userService.AddAsync(user);

                var log = new Log
                {
                    KullaniciId = createdUser.Id, // createdUser.Id kullanarak doğru kullanıcı ID'sini alın
                    Durum = "Başarılı",
                    IslemTip = "Kullanıcı Ekleme",
                    Aciklama = $"Kullanıcı ID: {createdUser.Id} eklendi",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                



            }
            catch (Exception e)
            {

                var log = new Log
                {
                    KullaniciId = 44, // createdUser.Id kullanarak doğru kullanıcı ID'sini alın
                    Durum = "Başarılı",
                    IslemTip = "Kullanıcı Ekleme",
                    Aciklama = e.Message,
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);
            }

            return CreatedAtAction(nameof(GetUser), new { id = 55 },  "fırat");



        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User userInfo)
        {
            if (userInfo == null)
            {
                return BadRequest();
            }

            var updatedUser = await _userService.UpdateAsync(id, userInfo);

            var log = new Log
            {
                KullaniciId = updatedUser.Id, // updatedUser.Id kullanarak doğru kullanıcı ID'sini alın
                Durum = "Başarılı",
                IslemTip = "Kullanıcı Güncelleme",
                Aciklama = $"Kullanıcı ID: {updatedUser.Id} güncellendi",
                TarihveSaat = DateTime.Now,
                KullaniciTip = "Admin"
            };
            await _logService.AddLog(log);

            return Ok(updatedUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            var log = new Log
            {
                KullaniciId = id,
                Durum = "Başarılı",
                IslemTip = "Kullanıcı Silme",
                Aciklama = $"Kullanıcı ID: {id} silindi",
                TarihveSaat = DateTime.Now,
                KullaniciTip = "Admin"
            };
            await _logService.AddLog(log);

            return NoContent();
        }
    }
}
