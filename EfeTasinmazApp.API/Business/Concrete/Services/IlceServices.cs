using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EfeTasinmazApp.API.Business.Concrete
{
    public class IlceService : IIlceService
    {
        private readonly MyDbContext _context;

        public IlceService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ilce>> GetAllAsync()
        {
            return await _context.Ilceler.Include(t=>t.Il).ToListAsync();
        }

        public async Task<Ilce> GetByIdAsync(int id)
        {
            return await _context.Ilceler.FindAsync(id);
        }

        public async Task<Ilce> AddAsync(Ilce ilce)
        {
            _context.Ilceler.Add(ilce);
            await _context.SaveChangesAsync();
            return ilce;
        }

        public async Task<Ilce> UpdateAsync(Ilce ilce)
        {
            _context.Ilceler.Update(ilce);
            await _context.SaveChangesAsync();
            return ilce;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ilce = await _context.Ilceler.FindAsync(id);
            if (ilce == null)
            {
                return false;
            }

            _context.Ilceler.Remove(ilce);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ilce>> GetIlcelerByIlIdAsync(int ilId)
        {
            return await _context.Ilceler.Where(ilce => ilce.IlId == ilId).ToListAsync();
        }

    }
}
