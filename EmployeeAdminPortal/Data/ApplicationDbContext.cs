using Microsoft.EntityFrameworkCore;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Data
{
    // this class it ot manage the connection between the database and application
    public class ApplicationDbContext :  DbContext
    {
        // inherits from other class, the DBContext (which is from the class from the package installed (EntityFrameworkCore)
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            // constructor, pass the parameter DbContextOptions 
        }

        public DbSet<Employee> Employees { get; set; } // property (prop) 


    }
}
