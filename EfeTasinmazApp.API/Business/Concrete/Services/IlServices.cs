/* using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EfeTasinmazApp.API.Business.Concrete
{
    public class IlService : IIlService
    {
        private readonly MyDbContext _context;

        public IlService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Il>> GetAllAsync()
        {
            return await _context.Iller.ToListAsync();
        }

        public async Task<Il> GetByIdAsync(int id)
        {
            return await _context.Iller.FindAsync(id);
        }

        public async Task<Il> AddAsync(Il il)
        {
            _context.Iller.Add(il);
            await _context.SaveChangesAsync();
            return il;
        }

        public async Task<Il> UpdateAsync(Il il)
        {
            _context.Iller.Update(il);
            await _context.SaveChangesAsync();
            return il;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var il = await _context.Iller.FindAsync(id);
            if (il == null)
            {
                return false;
            }

            _context.Iller.Remove(il);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.DataAccess;
using EfeTasinmazApp.API.Entities.Concrete;
using EfeTasinmazApp.API.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EfeTasinmazApp.API.Business.Concrete
{
    public class IlService : IIlService
    {
        private readonly MyDbContext _context;

        public IlService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Il>> GetAllAsync()
        {
            return await _context.Iller.ToListAsync();
        }

        public async Task<Il> GetByIdAsync(int id)
        {
            return await _context.Iller.FindAsync(id);
        }

        public async Task<Il> AddAsync(Il il)
        {
            _context.Iller.Add(il);
            await _context.SaveChangesAsync();
            return il;
        }

        public async Task<Il> UpdateAsync(Il il)
        {
            _context.Iller.Update(il);
            await _context.SaveChangesAsync();
            return il;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var il = await _context.Iller.FindAsync(id);
            if (il == null)
            {
                return false;
            }

            _context.Iller.Remove(il);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
