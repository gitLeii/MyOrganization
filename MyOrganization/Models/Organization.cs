using System.Collections.Generic;

namespace MyOrganization.Models
{
    public class Organization
    {
        public enum OrganizationType
        {
            Company,
            Individual
        }
        public int ID { get; set; }
        public OrganizationType Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}