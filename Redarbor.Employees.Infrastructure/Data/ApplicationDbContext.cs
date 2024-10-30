using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Infrastructure.Configurations;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<EmployeeModel> Employees => Set<EmployeeModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

    }
}
