using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UmbAPI.Data;
using UmbAPI.DTO;
using UmbAPI.Interfaces;
using UmbAPI.Models;

namespace UmbAPI.Repos
{
    public class HumanResourcesRepo : IHumanResourcesRepo
    {
        private readonly HumanResourcesContext _context;
        private readonly IMapper _mapper;

        public HumanResourcesRepo(HumanResourcesContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public void AssignDepartment(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void CreateEmployee(EmployeeDTO employeeDto)
        {
            var dept = GetDepartment(employeeDto.DepartmentName);
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.Department = dept;
            _context.Employees.Add(employee);
            Save();
        }

        public List<DepartmentDTO> GetDepartments()
        {
            return _mapper.Map<List<DepartmentDTO>>(_context.Departments);
        }

        public List<EmployeeDTO> GetEmployees()
        {
            return _mapper.Map<List<EmployeeDTO>>(_context.Employees.Include(p => p.Department));
        }

        public List<EmployeeDTO> GetEmployeesByDepartment(string dept)
        {
            return _mapper.Map<List<EmployeeDTO>>(_context.Employees.Where(c => c.Department.DepartmentName == dept.ToUpper()).Include(p => p.Department).ToList());
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void AssignDepartment()
        {
            throw new NotImplementedException();
        }

        public void CreateDepartment(DepartmentDTO department)
        {
            _context.Departments.Add(_mapper.Map<Department>(department));
            Save();
        }

        public bool DepartmentExists(DepartmentDTO department)
        {
            var dept = _context.Departments.ToList().Where(c => c.DepartmentName.Trim().ToUpper() == department.DepartmentName.ToUpper()).FirstOrDefault();

            // return true if department exists
            return dept != null && dept.DepartmentID != 0;
        }

        public Department? GetDepartment(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return _context.Departments.FirstOrDefault(c => c.DepartmentName.Trim().ToUpper() == name.ToUpper());
        }

        public bool EmployeeExists(EmployeeDTO employee)
        {
            var existingEmployee = _context.Employees.ToList().Where(c => c.EmployeeNumber.Trim().ToUpper() == employee.EmployeeNumber.ToUpper()).FirstOrDefault();

            // return true if employee exists
            return existingEmployee != null && existingEmployee.EmployeeID != 0;
        }

        public Employee? GetEmployee(string employeeNum, string? tel)
        {
            if (string.IsNullOrEmpty(employeeNum))
                return null;

            Employee? employee = null;

            employee = _context.Employees.FirstOrDefault(c => c.EmployeeNumber.Trim().ToUpper() == employeeNum.ToUpper());

            if(employee == null)
            {
                if(!string.IsNullOrEmpty(tel))
                    employee = _context.Employees.FirstOrDefault(c => c.Telephone.Trim().ToUpper() == tel.Trim().ToUpper());
            }

            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            Save();
        }
    }
}
