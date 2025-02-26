using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Data
{
    public class LoginData : DbContext
    {
        public LoginData(DbContextOptions<LoginData> options) : base(options)
        {

        }
        public DbSet<AdminLogin> admins { get; set; }
    }
}
