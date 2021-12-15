using Extenso.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutofacTypeVarianceIssue.Data
{
    public class ApplicationDbContextFactory : IDbContextFactory
    {
        private DbContextOptions<ApplicationDbContext> options;

        private DbContextOptions<ApplicationDbContext> Options
        {
            get
            {
                if (options == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    optionsBuilder.UseInMemoryDatabase("AutofacTypeVarianceIssue");
                    options = optionsBuilder.Options;
                }
                return options;
            }
        }

        public DbContext GetContext()
        {
            return new ApplicationDbContext(Options);
        }

        public DbContext GetContext(string connectionString) // in this case, "connectionString" should be an in-memory db name..
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}