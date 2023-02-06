using System.ComponentModel.DataAnnotations;

namespace UmbAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
