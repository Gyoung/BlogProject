using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Tests
{
    public class Model
    {

    }

    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class NorthwindContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public NorthwindContext()
            : base("NorthwindContext")
        {
            
        }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
