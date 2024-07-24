using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return await _context.Tasinmazlar.Include(t => t.Mahalle).ThenInclude(t => t.Ilce).ThenInclude(t => t.Il).
                Include(t => t.User).
                ToListAsync();
        }

        public async Task<Tasinmaz> GetByIdAsync(int id)
        {
            return await _context.Tasinmazlar.Include(x => x.Mahalle).ThenInclude(l => l.Ilce).ThenInclude(a => a.Il).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tasinmaz> AddAsync(Tasinmaz tasinmaz)
        {
            _context.Tasinmazlar.Add(tasinmaz);
            await _context.SaveChangesAsync();
            return tasinmaz;
        }

        public async Task<Tasinmaz> UpdateAsync(int id, Tasinmaz tasinmaz)
        {
            var yeniTasinmaz = _context.Tasinmazlar.FirstOrDefault(
                x => x.Id == id);
            yeniTasinmaz.MahalleId = tasinmaz.MahalleId;
            yeniTasinmaz.Ada = tasinmaz.Ada;
            yeniTasinmaz.Parsel = tasinmaz.Parsel;
            yeniTasinmaz.Nitelik = tasinmaz.Nitelik;
            yeniTasinmaz.KoordinatBilgileri = tasinmaz.KoordinatBilgileri;
            yeniTasinmaz.Adres = tasinmaz.Adres;


            await _context.SaveChangesAsync();
            return yeniTasinmaz;
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

        public async Task<List<Tasinmaz>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Tasinmazlar
                .Include(t => t.Mahalle)
                    .ThenInclude(m => m.Ilce)
                        .ThenInclude(i => i.Il)
                .Include(t => t.User)  // User bilgisini dahil ettiğinizden emin olun
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

    }
}