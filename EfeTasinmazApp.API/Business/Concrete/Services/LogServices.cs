using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities;
using EfeTasinmazApp.API.DataAccess;
using Tasinmaz_Proje.Entities;

namespace Tasinmaz_Proje.Services
{
    public class LogService : ILogService
    {
        private readonly MyDbContext _dbContext;

        public LogService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await _dbContext.Logs.ToListAsync();
        }

        public async Task<Log> GetByIdAsync(int id)
        {
            return await _dbContext.Logs.FindAsync(id);
        }

        public async Task<Log> AddAsync(Log log)
        {
            _dbContext.Logs.Add(log);
            await _dbContext.SaveChangesAsync();
            return log;
        }

        public async Task<Log> UpdateAsync(int id, Log log)
        {
            var existingLog = await _dbContext.Logs.FindAsync(id);
            if (existingLog == null)
            {
                return null;
            }

            existingLog.KullaniciId = log.KullaniciId;
            existingLog.Durum = log.Durum;
            existingLog.IslemTip = log.IslemTip;
            existingLog.Aciklama = log.Aciklama;
            existingLog.TarihveSaat = log.TarihveSaat;
            existingLog.KullaniciTip = log.KullaniciTip;

            _dbContext.Entry(existingLog).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return existingLog;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var log = await _dbContext.Logs.FindAsync(id);
            if (log == null)
            {
                return false;
            }

            _dbContext.Logs.Remove(log);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
