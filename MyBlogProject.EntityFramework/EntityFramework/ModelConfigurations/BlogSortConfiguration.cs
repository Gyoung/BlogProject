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
    public class BlogSortConfiguration : EntityTypeConfiguration<BlogSort>
    {
        public BlogSortConfiguration()
        {
            HasKey(k => k.SortId);
            Property(p => p.SortId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.SortName).HasMaxLength(30);
            Property(p => p.CreateTime);
            Property(p => p.ModifyTime);
            Property(p => p.IsDelete);

            ToTable("BlogSorts");
        }
    }
}
