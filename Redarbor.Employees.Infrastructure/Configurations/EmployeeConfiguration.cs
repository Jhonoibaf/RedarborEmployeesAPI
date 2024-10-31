using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedarborEmployees.Infrastructure.Models;

namespace RedarborEmployees.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            builder.Property(e => e.CompanyId)
                .HasColumnName("company_id")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(e => e.Fax)
                .HasColumnName("fax");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50);

            builder.Property(e => e.LastLogin)
                .HasColumnName("last_login");

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(e => e.PortalId)
                .HasColumnName("portal_id")
                .IsRequired();

            builder.Property(e => e.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            builder.Property(e => e.StatusId)
                .HasColumnName("status_id")
                .IsRequired();

            builder.Property(e => e.Telephone)
                .HasColumnName("telephone");

            builder.Property(e => e.CreatedOn)
                .HasColumnName("created_on");

            builder.Property(e => e.DeletedOn)
                .HasColumnName("deleted_on");
            
            builder.Property(e => e.UpdatedOn)
                .HasColumnName("updated_on");
           
            builder.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(150)
                .IsRequired();

        }
    }
}
