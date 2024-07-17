using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EfeTasinmazApp.API.Business.Concrete
{
    public class TasinmazService : ITasinmazService
    {
        private readonly MyDbContext _context;

        public TasinmazService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tasinmaz>> GetAllAsync()
        {
            return await _context.Tasinmazlar.Include(t => t.Mahalle).ThenInclude(t=>t.Ilce).ThenInclude(t=>t.Il).ToListAsync();
        }

        public async Task<Tasinmaz> GetByIdAsync(int id)
        {
            return await _context.Tasinmazlar.FindAsync(id);
        }

        public async Task<Tasinmaz> AddAsync(Tasinmaz tasinmaz)
        {
            _context.Tasinmazlar.Add(tasinmaz);
            await _context.SaveChangesAsync();
            return tasinmaz;
        }

        public async Task<Tasinmaz> UpdateAsync(Tasinmaz tasinmaz)
        {
            _context.Tasinmazlar.Update(tasinmaz);
            await _context.SaveChangesAsync();
            return tasinmaz;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tasinmaz = await _context.Tasinmazlar.FindAsync(id);
            if (tasinmaz == null)
            {
                return false;
            }

            _context.Tasinmazlar.Remove(tasinmaz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
