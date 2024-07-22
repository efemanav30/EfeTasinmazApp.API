using System.Collections.Generic;
using System.Threading.Tasks;
using EfeTasinmazApp.API.Entities.Concrete;
using Tasinmaz_Proje.Entities;

namespace EfeTasinmazApp.API.Business.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(int id, User user);
        Task<bool> DeleteAsync(int id);
    }
}
