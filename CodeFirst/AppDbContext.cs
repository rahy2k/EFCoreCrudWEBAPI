
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    public class AppDbContext:DbContext
    {

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-O0SB0NN\\PWCHOME;Initial Catalog =EFCoreDb;Integrated Security=True;");
            }

        }
    }
}
