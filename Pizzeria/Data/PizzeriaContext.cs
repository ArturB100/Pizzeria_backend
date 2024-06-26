using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Data
{
    public class PizzeriaContext : DbContext
    {
        public PizzeriaContext(DbContextOptions<PizzeriaContext> options)
            : base(options)
        {
        }

        public DbSet<Pizzeria.Model.User> User { get; set; }
        public DbSet<Pizzeria.Model.Address> Address { get; set; }
    }
}
