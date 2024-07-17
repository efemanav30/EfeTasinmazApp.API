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
    public class MahalleController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MahalleController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Mahalle
        [HttpGet]
        public async Task<ActionResult<List<Mahalle>>> GetMahalleler()
        {
            return await _context.Mahalleler
                .Include(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Mahalle>> GetMahalle(int id)
        {
            var mahalle = await _context.Mahalleler
                .Include(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mahalle == null)
            {
                return NotFound();
            }

            return mahalle;
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
    public class MahalleController : ControllerBase
    {
        private readonly IMahalleService _mahalleService;

        public MahalleController(IMahalleService mahalleService)
        {
            _mahalleService = mahalleService;
        }

        // GET: api/Mahalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetMahalleler()
        {
            var mahalleler = await _mahalleService.GetAllAsync();
            return Ok(mahalleler);
        }

        // GET: api/Mahalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mahalle>> GetMahalle(int id)
        {
            var mahalle = await _mahalleService.GetByIdAsync(id);
            if (mahalle == null)
            {
                return NotFound();
            }
            return Ok(mahalle);
        }

        [HttpGet("by-ilce/{ilceId}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetMahallelerByIlceId(int ilceId)
        {
            var mahalleler = await _mahalleService.GetMahallelerByIlceIdAsync(ilceId);
            if (mahalleler == null || mahalleler.Count == 0)
            {
                return NotFound();
            }
            return Ok(mahalleler);
        }


    }
}
