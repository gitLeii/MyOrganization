namespace MyOrganization.Models
{
    public class Employee
    {
        public enum EmployeeType
        {
            FullTime,
            PartTime
        }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int OrganizationID { get; set; }
        public EmployeeType Type { get; set; }
        public virtual Organization Organization { get; set; }
    }
}