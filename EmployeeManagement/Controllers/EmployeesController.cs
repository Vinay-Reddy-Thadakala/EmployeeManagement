
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = _context.Employees.ToList();

            return Ok(allEmployees);
        }

        // GET: api/employees/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = _context.Employees.Find(id);

            if(employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }            

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            _context.Employees.Add(employeeEntity);
            _context.SaveChanges();

            return Ok(employeeEntity);
        }

        // PUT: api/employees/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            _context.SaveChanges();

            return Ok(employee);
        }

        // DELETE: api/employees/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _context.Employees.Find(id);
            if(employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges(); 

            return Ok(employee);
        }
    }
}
