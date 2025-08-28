namespace EmployeeAdminPortal.Models
{
    public class AddEmployeeDto
    {
        // DTO: Data Transfer Object
        // transfer data from one operation to another
        // will have similar properties in the entities class to accept name, email, phone, salary


        public required string Name { get; set; }
        public required string Email { get; set; }

        public string? Phone { get; set; }
        // use '?' to make a nullable property (not required)
        public decimal Salary { get; set; }
    }
}
