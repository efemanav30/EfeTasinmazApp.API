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
    public class IlceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public IlceController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Ilce
        [HttpGet]
        public async Task<ActionResult<List<Ilce>>> GetIlceler()
        {
            return await _context.Ilceler
                .Include(i => i.Il)
                .ToListAsync();
        }

        // POST: api/Ilce
       

        // GET: api/Ilce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ilce>> GetIlce(int id)
        {
            var ilce = await _context.Ilceler
                .Include(i => i.Il)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ilce == null)
            {
                return NotFound();
            }

            return ilce;
        }
    }
}
*/
using Microsoft.AspNetCore.Mvc;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlceController : ControllerBase
    {
        private readonly IIlceService _ilceService;

        public IlceController(IIlceService ilceService)
        {
            _ilceService = ilceService;
        }

        // GET: api/Ilce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetIlceler()
        {
           
            var ilceler = await _ilceService.GetAllAsync();
            return Ok(ilceler);
        }

        // GET: api/Ilce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ilce>> GetIlce(int id)
        {
                

            var ilce = await _ilceService.GetByIdAsync(id);
            if (ilce == null)
            {
                return NotFound();
            }
            return Ok(ilce);
        }
        [HttpGet("by-il/{ilId}")]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetIlcelerByIlId(int ilId)
        {
            var ilceler = await _ilceService.GetIlcelerByIlIdAsync(ilId);
            if (ilceler == null || ilceler.Count == 0)
            {
                return NotFound();
            }
            return Ok(ilceler);
        }


    }
}
