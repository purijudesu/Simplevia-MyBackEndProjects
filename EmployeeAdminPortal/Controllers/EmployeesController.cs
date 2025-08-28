using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace EmployeeAdminPortal.Controllers
{
    // A controller to make different actions on the employees
    // add various endpoints, or operations like GET, POST, DELETE

    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // create and assign private field to use in different methods
        private readonly ApplicationDbContext dbContext;


        // inherting from the controllerBase class

        // Inject DbContext as a constructor, access database anywhere on the program as injected in Program.cs
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // first, read all employee from the database
        // create http method

        [HttpGet] // GET endpoint for ALL
        public IActionResult GetAllEmployees()
        {
            // return static list of employees or connect to database to get live result
            // connect to database using DbContext

            // store the employee entities as a list
            var allEmployees = dbContext.Employees.ToList(); // connecting to the database
            return Ok(allEmployees); // send an ok response, return employees back

            // we can also write that in one liner code:
            // return Ok (dbContext.Employees.ToList());
        }

        [HttpGet] // GET endpoint by ID
        [Route("{id:guid}")] // accepting an identifer in the route itself
        public IActionResult GetEmployeeById(Guid id) // make sure to match the name from the route
        {
            // call dbContext and fetch employee by id
            var employee = dbContext.Employees.Find(id); // nullable

            if (employee is null)
            {
                return NotFound(); //404
            }
            return Ok(employee);
        }

        [HttpPost] // ADD endpoint
        // accept parameters as body of this post request
        // create object as carrier of this data
        // for this type of object, we use Data Transfer Object (DTO)
        // dto: trasnfer data from one operation to another
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            // employee entity to employee dto
            var employeeEntity = new Employee()
            {
                // id is being handled by entity framework core for us
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
                /*
                   it may seem wastage of code, and why are we even accepting different type of object
                   and then converting it into an entity and passing the entity.

                   The reason for that is entities are separate from the dtos, by adding dtos,
                   we achieve a separation of concern, we are basically exposing the informaton (dto class)
                   whereas the employee entity is an image of what we have in the table/database
                   and we can have different name of the properties
                   we make our code modular, generic, and reusable
                   */
            };

            dbContext.Employees.Add(employeeEntity); // accepts entity, so we write logic for entity to dto
            // entity framework core wants us to save changes by ourselves
            dbContext.SaveChanges();
            return Ok(employeeEntity); // IActionResults needs return type to tell client that operation is sucessful
            // ideally in httppost response, when we add resource, we send 201 (resource created response)
            // for this example, we'll use 200 (ok response) to simplify

        }

        [HttpPut] // UPDATE (EDIT) resource endpoint (PUT convention)
        [Route("id:guid")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto) // accepts two parameters, first = guid, second = what we want tot update
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete] // DELETE resource endpoint by ID
        [Route("id:guid")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges(); // remember, aside from GET, who is idempotent, always save changes on the resources to reflect on the database

            return Ok();
        }
    }
} 
