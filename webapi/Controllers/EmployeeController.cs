using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Entities;
namespace webapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext data;

        public EmployeeController(DataContext context)
        {
            data = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await data.Employees.Include(e=>e.Department).ToListAsync();
            return Ok(employees);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await data.Employees.Include(e=>e.Department).FirstOrDefaultAsync(e=>e.EmployeeID ==id);

            if (employee == null)
                return NotFound("employee not found");

            return Ok(employee);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            
            var existing_department = await data.Department.FirstOrDefaultAsync(d => d.DepartmentID == employee.DepartmentID);

            if (existing_department == null)
            {
                return BadRequest($"Department with ID {employee.DepartmentID} does not exist.");
            }

            employee.Department = existing_department;

            data.Employees.Add(employee);
            await data.SaveChangesAsync();

            return Ok(await data.Employees.Include(e=>e.Department).ToListAsync());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] Employee new_employee)
        {
            if (id != new_employee.EmployeeID)
                return BadRequest("Employee ID in the URL does not match the request body.");

            var existing_employee = await data.Employees.FindAsync(id);
            if (existing_employee == null)
                return NotFound($"Employee with ID {id} not found.");

            if (!string.IsNullOrEmpty(new_employee.FirstName))
                existing_employee.FirstName = new_employee.FirstName;

            if (!string.IsNullOrEmpty(new_employee.LastName))
                existing_employee.LastName = new_employee.LastName;

            if (!string.IsNullOrEmpty(new_employee.Email))
                existing_employee.Email = new_employee.Email;

            if (!string.IsNullOrEmpty(new_employee.PhoneNumber))
                existing_employee.PhoneNumber = new_employee.PhoneNumber;

            if (!string.IsNullOrEmpty(new_employee.JobTitle))
                existing_employee.JobTitle = new_employee.JobTitle;

            if (new_employee.HireDate != default)
                existing_employee.HireDate = new_employee.HireDate;

            if (new_employee.Salary > 0)
                existing_employee.Salary = new_employee.Salary;

            if (new_employee.DepartmentID > 0 && new_employee.DepartmentID != existing_employee.DepartmentID)
            {
                bool departmentExists = await data.Department.AnyAsync(d => d.DepartmentID == new_employee.DepartmentID);
                if (!departmentExists)
                    return BadRequest($"DepartmentID {new_employee.DepartmentID} does not exist.");

                existing_employee.DepartmentID = new_employee.DepartmentID;
            }
            Console.WriteLine(new_employee);
            await data.SaveChangesAsync();
            return Ok(await data.Employees.Include(e=>e.Department).FirstOrDefaultAsync(e=>e.EmployeeID==id));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await data.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            data.Employees.Remove(employee);
            await data.SaveChangesAsync();
            return NoContent();
        }


    }
}
