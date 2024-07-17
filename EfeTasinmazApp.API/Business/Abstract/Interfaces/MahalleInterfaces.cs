using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities.Concrete;

namespace EfeTasinmazApp.API.Business.Abstract
{
    public interface IMahalleService
    {
        Task<List<Mahalle>> GetAllAsync();
        Task<Mahalle> GetByIdAsync(int id);
        Task<Mahalle> AddAsync(Mahalle mahalle);
        Task<Mahalle> UpdateAsync(Mahalle mahalle);
        Task<bool> DeleteAsync(int id);
        Task<List<Mahalle>> GetMahallelerByIlceIdAsync(int ilceId);

    }
}
