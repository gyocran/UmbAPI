using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UmbAPI.Data;
using UmbAPI.DTO;
using UmbAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UmbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly HumanResourcesContext _context;
        private readonly IMapper mapper;

        public DepartmentAPIController(HumanResourcesContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("all")]
        public IActionResult GetDepartments()
        {
            var departments = mapper.Map<List<DepartmentDto>>(_context.Departments);

            return Ok(departments);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public IActionResult AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            var existingDepartment = _context.Departments.ToList().Where(c => c.DepartmentName.Trim().ToUpper() == departmentDto.DepartmentName.ToUpper()).FirstOrDefault();

            if (existingDepartment != null)
            {
                ModelState.AddModelError("", "Department already exists");
                return BadRequest(ModelState);
            }

            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName
            };

            //var employee = mapper.Map<Employee>(employeeDto);
            _context.Departments.Add(department);
            _context.SaveChangesAsync();
            return Ok("Department successfully created");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("assigndepartment")]
        public IActionResult AssignEmployeeToDepartment([FromBody] DepartmentAssignmentDto assignment)
        {
            if (assignment == null)
                return BadRequest(ModelState);

            var existingDepartment = _context.Departments.ToList().Where(c => c.DepartmentName.Trim().ToUpper() == assignment.DepartmentName.ToUpper()).FirstOrDefault();

            if (existingDepartment == null)
            {
                ModelState.AddModelError("", "Department does not exist");
                return BadRequest(ModelState);
            }

            var existingEmployee = _context.Employees.ToList().Where(c => c.EmployeeNumber.Trim().ToUpper() == assignment.EmployeeNumber.ToUpper()).FirstOrDefault();

            if (existingEmployee == null)
            {
                ModelState.AddModelError("", "Employee does not exist");
                return BadRequest(ModelState);
            }

            existingEmployee.Department = existingDepartment;

            //var employee = mapper.Map<Employee>(employeeDto);
            _context.Update(existingEmployee);
            _context.SaveChangesAsync();
            return Ok("Department successfully assigned to employee");
        }
    }
}
