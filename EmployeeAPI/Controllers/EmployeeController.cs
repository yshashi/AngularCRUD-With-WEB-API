using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        private readonly UserDbContext _context;
        public EmployeeController(UserDbContext userDbContext)
        {
            _context = userDbContext;
        }

        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employeeObj)
        {
            try
            {
                if (employeeObj == null)
                {
                    return BadRequest();
                }
                else
                {
                    _context.employeeModels.Add(employeeObj);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messsage = "Employee added Successfully"
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeModel employeeObj)
        {
            if(employeeObj == null)
            {
                return BadRequest();
            }
            var user = _context.employeeModels.AsNoTracking().FirstOrDefault(x => x.Id == employeeObj.Id);
            if(user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                _context.Entry(employeeObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee Updated Successfully"
                });
            }
        }
        [HttpDelete("delete_employee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var user = _context.employeeModels.Find(id);
            if(user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "user not Found"
                });
            }
            else
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "EmployeeAPI Deleted"
                });
            }
        }
        [HttpGet("get_all_employees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _context.employeeModels.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                EmployeeDetails= employees
            });
        }
        [HttpGet("get_employee/id")]
        public IActionResult Getemployee(int id)
        {
            var employee = _context.employeeModels.Find(id);
            if(employee == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    EmployeeDetail = employee
                });
            }
        }
    }
}
