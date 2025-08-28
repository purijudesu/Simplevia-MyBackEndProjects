namespace EmployeeAdminPortal.Models.Entities
{
    public class Employee
    {
        // primary key as employee
        // add some properties to signify the columns in the table for sql database creation
        // globally unique identifer (guid)
        public Guid Id { get; set; }

        public required string Name { get; set; }
        // use 'required' keyword for non-nullable property
        // whenever Employee object is initialized, Name must be filled out otherwise, it will throw a compilation error
        public required string Email { get; set; }

        public string? Phone { get; set; }
        // user '?' to make a nullable property (not required)
        public decimal Salary { get; set; }
    }
}
