namespace MyBlogProject.DataCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogsort : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "SortId", "dbo.BlogSorts");
            DropIndex("dbo.Blogs", new[] { "SortId" });
            CreateTable(
                "dbo.BlogBlogSorts",
                c => new
                    {
                        Blog_BlogId = c.Int(nullable: false),
                        BlogSort_SortId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Blog_BlogId, t.BlogSort_SortId })
                .ForeignKey("dbo.Blogs", t => t.Blog_BlogId, cascadeDelete: true)
                .ForeignKey("dbo.BlogSorts", t => t.BlogSort_SortId, cascadeDelete: true)
                .Index(t => t.Blog_BlogId)
                .Index(t => t.BlogSort_SortId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogBlogSorts", "BlogSort_SortId", "dbo.BlogSorts");
            DropForeignKey("dbo.BlogBlogSorts", "Blog_BlogId", "dbo.Blogs");
            DropIndex("dbo.BlogBlogSorts", new[] { "BlogSort_SortId" });
            DropIndex("dbo.BlogBlogSorts", new[] { "Blog_BlogId" });
            DropTable("dbo.BlogBlogSorts");
            CreateIndex("dbo.Blogs", "SortId");
            AddForeignKey("dbo.Blogs", "SortId", "dbo.BlogSorts", "SortId", cascadeDelete: true);
        }
    }
}
