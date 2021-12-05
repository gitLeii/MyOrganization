using MyOrganization.Models;
using System.Collections.Generic;

namespace MyOrganization.DAL
{
    public class OrgInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrgContext>
    {
        protected override void Seed(OrgContext context)
        {
            var organizations = new List<Organization>
            {
                new Organization{Type = Organization.OrganizationType.Individual, Name = "A", Address = "Demo"},
                new Organization{Type = Organization.OrganizationType.Company, Name = "B", Address = "Demo"},
                new Organization{Type = Organization.OrganizationType.Individual, Name = "C", Address = "Demo"},
                new Organization{Type = Organization.OrganizationType.Company, Name = "D", Address = "Demo"},
                new Organization{Type = Organization.OrganizationType.Individual, Name = "E", Address = "Demo"},
                new Organization{Type = Organization.OrganizationType.Company, Name = "F", Address = "Demo"},
            };
            organizations.ForEach(o => context.Organizations.Add(o));
            context.SaveChanges();
            var employees = new List<Employee>
            {
                new Employee{Type = Employee.EmployeeType.FullTime, Name = "A", OrganizationID = 1, Email = "A@gmail.com"},
                new Employee{Type = Employee.EmployeeType.PartTime, Name = "B", OrganizationID = 2, Email = "B@gmail.com"},
                new Employee{Type = Employee.EmployeeType.FullTime, Name = "D", OrganizationID = 2, Email = "D@gmail.com"},
                new Employee{Type = Employee.EmployeeType.FullTime, Name = "E", OrganizationID = 2, Email = "E@gmail.com"},
            };
            employees.ForEach(o => context.Employees.Add(o));
            context.SaveChanges();
            var assets = new List<Asset>
            {
                new Asset{Name = "Table", Amount = 2, OrganizationID = 3},
                new Asset{Name = "Chair", Amount = 2, OrganizationID = 3},
                new Asset{Name = "Wardrobe", Amount = 2, OrganizationID = 2},
                new Asset{Name = "Sofa", Amount = 2, OrganizationID = 4},
                new Asset{Name = "Desk", Amount = 2, OrganizationID = 2},
                new Asset{Name = "Table", Amount = 2, OrganizationID = 1,},
            };
            assets.ForEach(o => context.Assets.Add(o));
            context.SaveChanges();
        }
    }
}