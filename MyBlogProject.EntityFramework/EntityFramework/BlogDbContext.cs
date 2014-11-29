using MyBlogProject.DataCore.EntityFramework.ModelConfigurations;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.DataCore.EntityFramework
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
            : base("BlogSqliteConn")
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogSort> BlogSorts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MsgBoard> MsgBoards { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new BlogConfiguration())
                .Add(new CommentConfiguration())
                .Add(new MsgBoardConfiguration())
                .Add(new UserConfiguration())
                .Add(new BlogSortConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
