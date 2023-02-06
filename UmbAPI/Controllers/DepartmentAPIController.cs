using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UmbAPI.Data;
using UmbAPI.DTO;
using UmbAPI.Interfaces;
using UmbAPI.Models;

namespace UmbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly IHumanResourcesRepo _repo;

        public DepartmentAPIController(IHumanResourcesRepo repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("All")]
        public IActionResult GetDepartments()
        {
            var departments = _repo.GetDepartments();

            return Ok(departments);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public IActionResult AddDepartment([FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
                return BadRequest(ModelState);

            var exists = _repo.DepartmentExists(departmentDto);

            if (exists)
            {
                ModelState.AddModelError("Error", "Department already exists");
                return BadRequest(ModelState);
            }

            _repo.CreateDepartment(departmentDto);
            return Ok("Department successfully created");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AssignDepartment")]
        public IActionResult AssignEmployeeToDepartment([FromBody] DepartmentAssignmentDTO assignment)
        {
            if (assignment == null)
                return BadRequest(ModelState);

            var dept = _repo.GetDepartment(assignment.DepartmentName);

            if (dept == null)
            {
                ModelState.AddModelError("Error", "Department does not exist");
                return BadRequest(ModelState);
            }

            var employee = _repo.GetEmployee(assignment.EmployeeNumber, null);

            if (employee == null)
            {
                ModelState.AddModelError("Error", "Employee does not exist");
                return BadRequest(ModelState);
            }

            employee.Department = dept;

            _repo.UpdateEmployee(employee);
            return Ok("Department successfully assigned to employee");
        }
    }
}
