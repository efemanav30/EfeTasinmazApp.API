using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities.Concrete;

namespace EfeTasinmazApp.API.Business.Abstract
{
    public interface IIlService
    {
        Task<List<Il>> GetAllAsync();
        Task<Il> GetByIdAsync(int id);
        
    }
}
