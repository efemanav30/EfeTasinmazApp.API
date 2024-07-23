using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;
using Tasinmaz_Proje.Entities;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
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
            return NoContent();
        }
    }
}
