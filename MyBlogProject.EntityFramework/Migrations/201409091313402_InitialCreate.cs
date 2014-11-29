namespace MyBlogProject.DataCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 200),
                        Content = c.String(),
                        UserId = c.Int(nullable: false),
                        SortId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.BlogSorts", t => t.SortId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SortId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 1000),
                        Commenter = c.String(maxLength: 20),
                        BlogId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.BlogSorts",
                c => new
                    {
                        SortId = c.Int(nullable: false, identity: true),
                        SortName = c.String(maxLength: 30),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SortId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.MsgBoards",
                c => new
                    {
                        MsgBoardId = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        UserName = c.String(maxLength: 20),
                        IpAddress = c.String(maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MsgBoardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Blogs", "SortId", "dbo.BlogSorts");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.Blogs", new[] { "SortId" });
            DropIndex("dbo.Blogs", new[] { "UserId" });
            DropTable("dbo.MsgBoards");
            DropTable("dbo.Users");
            DropTable("dbo.BlogSorts");
            DropTable("dbo.Comments");
            DropTable("dbo.Blogs");
        }
    }
}
