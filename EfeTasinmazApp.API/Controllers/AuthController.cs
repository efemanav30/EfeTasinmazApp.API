using EfeTasinmazApp.API.Business.Abstract.Interfaces;
using EfeTasinmazApp.API.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tasinmaz_Proje.Entities;
using Tasinmaz_Proje.Services;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;
        private readonly ILogService _logService;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration, ILogService logService)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _logService = logService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = new User
            {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Email = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                Adress = userForRegisterDto.Adress,
                Role = userForRegisterDto.Role
            };

            try
            {
                var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

                var log = new Log
                {
                    KullaniciId = createdUser.Id,
                    Durum = "Başarılı",
                    IslemTip = "Kullanıcı Eklendi",
                    Aciklama = $"Kullanıcı ID: {createdUser.Id} eklendi",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = userToCreate.Id,
                    Durum = "Başarısız",
                    IslemTip = "Kullanıcı Ekleme",
                    Aciklama = $"Kullanıcı eklenirken hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "Admin"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error adding user: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (user == null)
            {
                var failedLog = new Log
                {
                    KullaniciId = 0, // Kullanıcı bulunamadığı için ID'yi 0 yapıyoruz
                    Durum = "Başarısız",
                    IslemTip = "Kullanıcı Girişi",
                    Aciklama = $"Giriş denemesi başarısız: {userForLoginDto.Email}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "User"
                };
                await _logService.AddLog(failedLog);
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Appsettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role) // Kullanıcı rolünü token'a ekleyin
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var successLog = new Log
            {
                KullaniciId = user.Id,
                Durum = "Başarılı",
                IslemTip = "Kullanıcı Girişi",
                Aciklama = $"Kullanıcı ID: {user.Id} giriş yaptı",
                TarihveSaat = DateTime.Now,
                KullaniciTip = user.Role
            };
            await _logService.AddLog(successLog);

            return Ok(new { Token = tokenString });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            try
            {
                // Kullanıcı kimliğini JWT token içinden alıyoruz
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized();
                }

                var user = await _authRepository.GetUserById(int.Parse(userId));

                if (user == null)
                {
                    return Unauthorized();
                }

                var log = new Log
                {
                    KullaniciId = user.Id,
                    Durum = "Başarılı",
                    IslemTip = "Kullanıcı Çıkışı",
                    Aciklama = $"Kullanıcı ID: {user.Id} çıkış yaptı",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = user.Role
                };
                await _logService.AddLog(log);

                return Ok();
            }
            catch (Exception ex)
            {
                var log = new Log
                {
                    KullaniciId = 0, // Hata durumunda ID'yi 0 yapıyoruz
                    Durum = "Başarısız",
                    IslemTip = "Kullanıcı Çıkışı",
                    Aciklama = $"Çıkış işlemi sırasında hata oluştu: {ex.Message}",
                    TarihveSaat = DateTime.Now,
                    KullaniciTip = "System"
                };
                await _logService.AddLog(log);

                Console.WriteLine($"Error during logout: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var user = await _authRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
