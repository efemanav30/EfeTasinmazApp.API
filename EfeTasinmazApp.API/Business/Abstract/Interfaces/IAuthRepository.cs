using System.Threading.Tasks;
using Tasinmaz_Proje.Entities;

namespace EfeTasinmazApp.API.Business.Abstract.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string eMail, string password);
        Task<bool> UserExists(string eMail);
    }
}
