using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities.Concrete;

namespace EfeTasinmazApp.API.Business.Abstract
{
    public interface ITasinmazService
    {
        Task<List<Tasinmaz>> GetAllAsync();
        Task<Tasinmaz> GetByIdAsync(int id);
        Task<Tasinmaz> AddAsync(Tasinmaz tasinmaz);
        Task<Tasinmaz> UpdateAsync(int id, Tasinmaz tasinmaz);
        Task<bool> DeleteAsync(int id, int userId);
        Task<List<Tasinmaz>> GetAllByUserIdAsync(int userId); // Bu satırı ekleyin
        Task<IEnumerable<Tasinmaz>> SearchAllAsync(string keyword);  // Yeni metod
        Task<IEnumerable<Tasinmaz>> SearchByUserIdAsync(int userId, string keyword);

    }
}