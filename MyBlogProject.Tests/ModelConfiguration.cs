using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Tests
{
    class ModelConfiguration
    {
    }

    public class ModelContext : ObjectContext
    {
        public ObjectSet<Order> Orders { get; set; }

      

        public ModelContext()
            : base("Data Source=aa.db")
        {
            
        }
    }
}
