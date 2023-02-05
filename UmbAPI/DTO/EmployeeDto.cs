using UmbAPI.Models;

namespace UmbAPI.DTO
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = null!;
        public string? OtherName { get; set; }
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string Telephone { get; set; } = null!;
        //public string DepartmentName { get; set; }
        public Department? Department { get; set; }
        public string? Email { get; set; }
        public DateTime DateEmployed { get; set; }
        public string EmployeeNumber { get; set; } = null!;
        //public string EmployeeType { get; set; }
    }
}
