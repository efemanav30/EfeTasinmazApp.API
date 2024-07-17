using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities.Concrete;

namespace EfeTasinmazApp.API.Business.Abstract
{
    public interface IIlceService
    {
        Task<List<Ilce>> GetAllAsync();
        Task<Ilce> GetByIdAsync(int id);

        Task<Ilce> AddAsync(Ilce ilce);
        Task<Ilce> UpdateAsync(Ilce ilce);
        Task<bool> DeleteAsync(int id);
        Task<List<Ilce>> GetIlcelerByIlIdAsync(int ilId);

    }
}
