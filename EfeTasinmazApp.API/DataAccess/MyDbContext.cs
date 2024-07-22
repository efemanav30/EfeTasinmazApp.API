/* using Microsoft.EntityFrameworkCore;
using EfeTasinmazApp.API.Entities.Concrete;

namespace EfeTasinmazApp.API.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Tasinmaz> Tasinmazlar { get; set; }

       
    }
}
*/
using Microsoft.EntityFrameworkCore;
using EfeTasinmazApp.API.Entities.Concrete;
using Tasinmaz_Proje.Entities;

namespace EfeTasinmazApp.API.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Tasinmaz> Tasinmazlar { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Il> Iller { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Log> Logs { get; set; }

    }
}
