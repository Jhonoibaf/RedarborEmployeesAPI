
using RedarborEmployees.Domain.Enums;

namespace RedarborEmployees.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Name { get; set; }
        public DateTime LastLogin { get; set; }
        public string Password { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Username { get; set; }


        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email is required.");
            if (CreatedOn > DateTime.Now)
                throw new ArgumentException("CreatedOn cannot be in the future.");
            if (UpdatedOn < CreatedOn)
                throw new ArgumentException("UpdatedOn cannot be earlier than CreatedOn.");
            if (DeletedOn.HasValue && StatusId == (int)RedarborEmployees.Domain.Enums.StatusId.Deleted)
                throw new ArgumentException("Deleted employees cannot have an active status.");
        }
    }

   
}
