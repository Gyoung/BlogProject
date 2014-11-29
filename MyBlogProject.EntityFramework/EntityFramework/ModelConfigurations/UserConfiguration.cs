using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlogProject.Entity;

namespace MyBlogProject.DataCore.EntityFramework.ModelConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(k => k.UserId);
            Property(p => p.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Email).HasMaxLength(50);
            Property(p => p.Password).HasMaxLength(50);
            Property(p => p.UserName).HasMaxLength(50);
            Property(p => p.CreateTime);
            Property(p => p.ModifyTime);
            Property(p => p.IsDelete);
        }
    }
}
