namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
