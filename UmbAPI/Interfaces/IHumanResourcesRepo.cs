using UmbAPI.DTO;
using UmbAPI.Models;

namespace UmbAPI.Interfaces
{
    public interface IHumanResourcesRepo
    {
        List<EmployeeDTO> GetEmployees();
        void UpdateEmployee(Employee employee);
        Employee? GetEmployee(string employeeNum, string tel);
        List<EmployeeDTO> GetEmployeesByDepartment(string dept);
        List<DepartmentDTO> GetDepartments();
        void CreateEmployee(EmployeeDTO employee);
        void CreateDepartment(DepartmentDTO department);
        void AssignDepartment();
        Department? GetDepartment(string name);
        bool DepartmentExists(DepartmentDTO dept);
        bool EmployeeExists(EmployeeDTO employee);
    }
}
