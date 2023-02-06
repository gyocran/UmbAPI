using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UmbAPI.Data;
using UmbAPI.DTO;
using UmbAPI.Interfaces;
using UmbAPI.Models;

namespace UmbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly IHumanResourcesRepo _repo;

        public EmployeeAPIController(IHumanResourcesRepo repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("all")]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _repo.GetEmployees();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [Authorize(Roles = "Administrator,Employee")]
        [HttpPost("GetEmployeesByDepartment")]
        public IActionResult GetDepartmentEmployees([FromBody] string department)
        {
            try
            {
                if (string.IsNullOrEmpty(department))
                    return BadRequest(ModelState);

                var employees = _repo.GetEmployeesByDepartment(department);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                if (employeeDto == null)
                    return BadRequest(ModelState);

                Department? dept = null;
                var existingEmployee = _repo.GetEmployee(employeeDto.EmployeeNumber, employeeDto.Telephone);

                if (existingEmployee != null)
                {
                    ModelState.AddModelError("Error", "Employee already exists");
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(employeeDto.DepartmentName))
                {
                    dept = _repo.GetDepartment(employeeDto.DepartmentName);

                    if (dept == null)
                    {
                        ModelState.AddModelError("Error", "Department does not exist");
                        return BadRequest(ModelState);
                    }
                }
                _repo.CreateEmployee(employeeDto);

                return Ok("Employee successfully created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }
    }
}
