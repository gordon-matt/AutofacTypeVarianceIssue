using AutofacTypeVarianceIssue.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutofacTypeVarianceIssue.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonMap());
        }
    }
}