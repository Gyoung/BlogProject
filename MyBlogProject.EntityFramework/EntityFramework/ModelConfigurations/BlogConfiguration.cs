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
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            HasKey(p => p.BlogId);
            Property(p => p.BlogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).HasMaxLength(200);
            Property(p => p.Content);
            Property(p => p.CreateTime);
            Property(p => p.ModifyTime);
            Property(p => p.IsDelete);
            Property(p => p.UserId);
            Property(p => p.VisitCount);

            HasRequired(p => p.User).WithMany().HasForeignKey(u => u.UserId);
            HasMany(p => p.Sorts).WithMany(s => s.Blogs);
            //HasRequired(p => p.Sort).WithMany().HasForeignKey(u => u.SortId);
            HasMany(p => p.Comments).WithRequired(c => c.Blog).HasForeignKey(c => c.BlogId);

            ToTable("Blogs");
        }
    }
}
