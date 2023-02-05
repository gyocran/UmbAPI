using System.ComponentModel.DataAnnotations;

namespace UmbAPI.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeID { get; set; }
        public string EmpType { get; set; } = null!;
        //public int EmployeeID { get; set; }
        //public List<Employee> Employees { get; set; }
    }
}
