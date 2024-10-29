using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Infrastructure.Configurations;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Infrastructure.Data
{
    internal class ApplicationDbContext    : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmployeeModel> Candidates => Set<EmployeeModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

    }
}
