using System.ComponentModel.DataAnnotations;

namespace UmbAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = null!;
        public string? OtherName { get; set; }
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string Telephone { get; set; } = null!;
        //public int DepartmentID { get; set; }
        public Department? Department { get; set; }
        public string? Email { get; set; }
        public DateTime DateEmployed { get; set; }  
        public string EmployeeNumber { get; set; } = null!;
        //public int EmployeeTypeID { get; set; }
        //public EmployeeType EmployeeType { get; set; }
    }
}
