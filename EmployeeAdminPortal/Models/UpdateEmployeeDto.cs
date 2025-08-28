namespace EmployeeAdminPortal.Models
{
    public class UpdateEmployeeDto
        // we'll have all the properties inside the demployee collection that we want to update
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        // use '?' to make a nullable property (not required)
        public decimal Salary { get; set; }
    }
}
