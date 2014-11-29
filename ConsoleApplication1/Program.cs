using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindContext context = new NorthwindContext();

            //var result = context.Set<Order>().SqlQuery("Select * from order").ToList();
            //TODO:编程的方式提供连接字符串
            var empList = context.Employees.OrderBy(c => c.FirstName).ToList();

             Employee employee=new Employee()
             {
                 FirstName = "Hello", LastName = "World"
             };
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine(empList.Count);
            Console.ReadLine();
        }
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
        public Int64 EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
