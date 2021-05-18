using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Product.Microservice.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }

        public async Task<int> Save()
        {
            return await base.SaveChangesAsync();
        }
    }
}