using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UmbAPI.Data;
using UmbAPI.DTO;
using UmbAPI.Models;

namespace UmbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly HumanResourcesContext _context;
        private readonly IMapper mapper;

        public EmployeeAPIController(HumanResourcesContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("all")]
        public IActionResult GetEmployees()
        {
            //var employees = mapper.Map<List<EmployeeDto>>(_context.Employees);
            var employees = _context.Employees.Include(p => p.Department);

            return Ok(employees);
        }

        [Authorize(Roles = "Administrator,Employee")]
        [HttpPost("getemployeesbydepartment")]
        public IActionResult GetDepartmentEmployees([FromBody] string department)
        {
            if (string.IsNullOrEmpty(department))
                return BadRequest(ModelState);

            var existingDepartment = _context.Departments.ToList().Where(c => c.DepartmentName.Trim().ToUpper() == department.ToUpper()).FirstOrDefault();

            if (existingDepartment == null)
            {
                ModelState.AddModelError("", "Department does not exist");
                return BadRequest(ModelState);
            }

            var employees = _context.Employees.Where(c => c.Department.DepartmentID == existingDepartment.DepartmentID).Include(p => p.Department).ToList();
            var employeeDtoList = new List<EmployeeDto>();

            foreach(var employee in employees)
            {
                var employeeDto = new EmployeeDto
                {
                    FirstName = employee.FirstName,
                    OtherName = employee.OtherName,
                    LastName = employee.LastName,
                    Address = employee.Address,
                    Telephone = employee.Telephone,
                    Department = employee.Department,
                    Email = employee.Email,
                    DateEmployed = employee.DateEmployed,
                    EmployeeNumber = employee.EmployeeNumber
                };

                employeeDtoList.Add(employeeDto);
            }

            return Ok(employeeDtoList);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public IActionResult AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            Department? dept = null;
            var existingEmployee = _context.Employees.ToList().Where(c => c.Telephone.Trim().ToUpper() == employeeDto.Telephone.ToUpper()).FirstOrDefault();
            
            if (existingEmployee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return BadRequest(ModelState);
            }

            existingEmployee = _context.Employees.ToList().Where(c => c.EmployeeNumber.Trim().ToUpper() == employeeDto.EmployeeNumber.ToUpper()).FirstOrDefault();

            if (existingEmployee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return BadRequest(ModelState);
            }

            if (employeeDto.Department != null)
            {
                dept = _context.Departments.ToList().Where(c => c.DepartmentName.Trim().ToUpper() == employeeDto.Department.DepartmentName.ToUpper()).FirstOrDefault();

                if (dept == null)
                {
                    ModelState.AddModelError("", "Department does not exist");
                    return BadRequest(ModelState);
                }
            }

            //var empType = _context.EmployeeTypes.ToList().Where(c => c.EmpType.Trim().ToUpper() == employeeDto.EmployeeType.ToUpper()).FirstOrDefault();

            //if (empType == null)
            //{
            //    ModelState.AddModelError("", "Employee type does not exist");
            //    return BadRequest(ModelState);
            //}

            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                OtherName = employeeDto.OtherName,
                LastName = employeeDto.LastName,
                Address = employeeDto.Address,
                Telephone = employeeDto.Telephone,
                Department= dept,
                Email = employeeDto.Email,
                DateEmployed = employeeDto.DateEmployed,
                EmployeeNumber = employeeDto.EmployeeNumber,
                //EmpEmployeeTypeID = empType.EmployeeTypeID
            };

            //var employee = mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            _context.SaveChangesAsync();
            return Ok("Employee successfully created");
        }
    }
}
