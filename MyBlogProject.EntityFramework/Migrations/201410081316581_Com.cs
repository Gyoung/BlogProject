namespace MyBlogProject.DataCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Com : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Email");
        }
    }
}
