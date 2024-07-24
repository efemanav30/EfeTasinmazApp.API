using EfeTasinmazApp.API.Business.Abstract.Interfaces;
using EfeTasinmazApp.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tasinmaz_Proje.Entities;

namespace EfeTasinmazApp.API.Business.Concrete.Services
{
    public class AuthRepository : IAuthRepository
    {
      private MyDbContext _dbContext;

        public AuthRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User>Register(User user, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User>Login(string eMail, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == eMail);
            if(user==null)
            {
                return null;
            }
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null ;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] UserPasswordHash, byte[] userPasswordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != UserPasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<bool>UserExists(string eMail)
        {
            if(await _dbContext.Users.AnyAsync(x=>x.Email == eMail))
            {

            return true;
            }
            return false;
        }
    }
}
