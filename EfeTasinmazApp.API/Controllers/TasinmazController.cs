/* using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using System;

namespace EfeTasinmazApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TasinmazController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasinmaz
        [HttpGet]
        public async Task<ActionResult<List<Tasinmaz>>> GetTasinmazlar()
        {
            return await _context.Tasinmazlar
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .ToListAsync();
        }

        // POST: api/Tasinmaz
        [HttpPost]
        public async Task<ActionResult<Tasinmaz>> PostTasinmaz(Tasinmaz tasinmaz)
        {
            var mahalle = await _context.Mahalleler
                                        .Include(m => m.Ilce)
                                        .ThenInclude(i => i.Il)
                                        .FirstOrDefaultAsync(m => m.Id == tasinmaz.MahalleId);

            if (mahalle == null)
            {
                return BadRequest("Invalid Mahalle ID");
            }

            tasinmaz.Mahalle = mahalle;

            _context.Tasinmazlar.Add(tasinmaz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTasinmaz), new { id = tasinmaz.Id }, tasinmaz);
        }

        // GET: api/Tasinmaz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasinmaz>> GetTasinmaz(int id)
        {
            var tasinmaz = await _context.Tasinmazlar
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tasinmaz == null)
            {
                return NotFound();
            }

            return tasinmaz;
        }

        // PUT: api/Tasinmaz/5
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutTasinmaz(int id, Tasinmaz tasinmaz)
         {
             if (id != tasinmaz.Id)
             {
                 return BadRequest("ID mismatch");
             }

             var mahalle = await _context.Mahalleler
                                         .Include(m => m.Ilce)
                                         .ThenInclude(i => i.Il)
                                         .FirstOrDefaultAsync(m => m.Id == tasinmaz.MahalleId);

             if (mahalle == null)
             {
                 return BadRequest("Invalid Mahalle ID");
             }

             tasinmaz.Mahalle = mahalle;

             _context.Entry(tasinmaz).State = EntityState.Modified;

             if (!await TasinmazExists(id))
             {
                 return NotFound();
             }

             await _context.SaveChangesAsync();

             return NoContent();
         }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz tasinmazBilgi)
        {
            if (id != tasinmazBilgi.Id)
            {
                return BadRequest("ID mismatch");
            }

            var tasinmaz = await _context.Tasinmazlar.FirstOrDefaultAsync(x => x.Id == id);

            if (tasinmaz == null)
            {
                return NotFound("Tasinmaz bulunamadı");
            }

            tasinmaz.MahalleId = tasinmazBilgi.MahalleId;
            tasinmaz.Ada = tasinmazBilgi.Ada;
            tasinmaz.Parsel = tasinmazBilgi.Parsel;
            tasinmaz.Nitelik = tasinmazBilgi.Nitelik;
            tasinmaz.KoordinatBilgileri = tasinmazBilgi.KoordinatBilgileri;


            return NoContent();
        }
        */
/*
[HttpPut("{id}")]
public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz tasinmazBilgi)
{
    if (id != tasinmazBilgi.Id)
    {
        return BadRequest();
    }

    var existingTasinmaz = await _context.Tasinmazlar.FirstOrDefaultAsync(x => x.Id == id);
    if (existingTasinmaz == null)
    {
        return NotFound();
    }

    existingTasinmaz.MahalleId = tasinmazBilgi.MahalleId;
    existingTasinmaz.Ada = tasinmazBilgi.Ada;
    existingTasinmaz.Parsel = tasinmazBilgi.Parsel;
    existingTasinmaz.Nitelik = tasinmazBilgi.Nitelik;
    existingTasinmaz.KoordinatBilgileri = tasinmazBilgi.KoordinatBilgileri;

    _context.Entry(existingTasinmaz).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}

// DELETE: api/Tasinmaz/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTasinmaz(int id)
{
    var tasinmaz = await _context.Tasinmazlar.FindAsync(id);
    if (tasinmaz == null)
    {
        return NotFound();
    }

    _context.Tasinmazlar.Remove(tasinmaz);
    await _context.SaveChangesAsync();

    return NoContent();
}

private async Task<bool> TasinmazExists(int id)
{
    return await _context.Tasinmazlar.AnyAsync(e => e.Id == id);
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
    public class TasinmazController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazService;

        public TasinmazController(ITasinmazService tasinmazService)
        {
            _tasinmazService = tasinmazService;
        }

        // GET: api/Tasinmaz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmazlar()
        {
            var tasinmazlar = await _tasinmazService.GetAllAsync();
            return Ok(tasinmazlar);
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
        public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz tasinmaz)
        {
            if (id != tasinmaz.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedTasinmaz = await _tasinmazService.UpdateAsync(tasinmaz);
            if (updatedTasinmaz == null)
            {
                return NotFound();
            }

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
