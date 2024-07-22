using System.Collections.Generic;
using System.Threading.Tasks;
using Tasinmaz_Proje.Entities;

namespace Tasinmaz_Proje.Services
{
    public interface ILogService
    {
        Task<List<Log>> GetAllAsync();
        Task<Log> GetByIdAsync(int id);
        Task<Log> AddAsync(Log log);
        Task<Log> UpdateAsync(int id, Log log);
        Task<bool> DeleteAsync(int id);
    }
}
