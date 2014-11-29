namespace MyBlogProject.DataCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delSortId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Blogs", "SortId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "SortId", c => c.Int(nullable: false));
        }
    }
}
