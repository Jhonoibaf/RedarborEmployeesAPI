using System.ComponentModel.DataAnnotations;

namespace RedarborEmployees.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastLogin { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int PortalId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string? Telephone { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedOn { get; set; }
        [Required]
        public string Username { get; set; }
        public string? Fax { get; set; }
    }
}
