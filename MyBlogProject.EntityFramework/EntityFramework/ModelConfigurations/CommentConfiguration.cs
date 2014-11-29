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
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(k => k.CommentId);
            Property(p => p.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Commenter).HasMaxLength(20);
            Property(p => p.Email).HasMaxLength(50);
            Property(p => p.BlogId);
            Property(p => p.Content).HasMaxLength(1000);
            Property(p => p.CreateTime);
            Property(p => p.ModifyTime);
            Property(p => p.IsDelete);

            //HasRequired(p => p.User).WithMany().HasForeignKey(u => u.UserId);
            //HasRequired(p => p.Blog).WithMany(a => a.Comments).HasForeignKey(p => p.BlogId);
        }
    }
}
