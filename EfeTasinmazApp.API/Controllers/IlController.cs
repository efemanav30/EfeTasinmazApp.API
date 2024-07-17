/* using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlController : ControllerBase
    {
        private readonly MyDbContext _context;

        public IlController (MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Il
        [HttpGet]
        public async Task<ActionResult<List<Il>>> GetIller()
        {
            return await _context.Iller.ToListAsync();
        }


        // GET: api/Il/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Il>> GetIl(int id)
        {
            var il = await _context.Iller.FindAsync(id);

            if (il == null)
            {
                return NotFound();
            }

            return il;
        }
    }
}
*/
using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlController : ControllerBase
    {
        private readonly IIlService _ilService;

        public IlController(IIlService ilService)
        {
            _ilService = ilService;
        }

        // GET: api/Il
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Il>>> GetIller()
        {
            var iller = await _ilService.GetAllAsync();
            return Ok(iller);
        }

        // GET: api/Il/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Il>> GetIl(int id)
        {
            var il = await _ilService.GetByIdAsync(id);
            if (il == null)
            {
                return NotFound();
            }
            return Ok(il);
        }

        
    }
}
