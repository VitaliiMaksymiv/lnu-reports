namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: true, defaultValue: ""));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: true, defaultValue: ""));
            AddColumn("dbo.AspNetUsers", "FathersName", c => c.String(nullable: true, defaultValue: ""));
        }
        
        public override void Down()
        {
        }
    }
}
