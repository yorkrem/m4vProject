using HealthKoppeling.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthKoppeling.Database
{
    public class UserDBContext: DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options):base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
    }
}
