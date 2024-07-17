using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EfeTasinmazApp.API.Business.Concrete
{
    public class MahalleService : IMahalleService
    {
        private readonly MyDbContext _context;

        public MahalleService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mahalle>> GetAllAsync()
        {
            return await _context.Mahalleler.Include(t=>t.Ilce).ThenInclude(t=>t.Il).ToListAsync();
        }

        public async Task<Mahalle> GetByIdAsync(int id)
        {
            return await _context.Mahalleler.FindAsync(id);
        }

        public async Task<Mahalle> AddAsync(Mahalle mahalle)
        {
            _context.Mahalleler.Add(mahalle);
            await _context.SaveChangesAsync();
            return mahalle;
        }

        public async Task<Mahalle> UpdateAsync(Mahalle mahalle)
        {
            _context.Mahalleler.Update(mahalle);
            await _context.SaveChangesAsync();
            return mahalle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mahalle = await _context.Mahalleler.FindAsync(id);
            if (mahalle == null)
            {
                return false;
            }

            _context.Mahalleler.Remove(mahalle);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Mahalle>> GetMahallelerByIlceIdAsync(int ilceId)
        {
            return await _context.Mahalleler.Where(mahalle => mahalle.IlceId == ilceId).ToListAsync();
        }
    }
}
