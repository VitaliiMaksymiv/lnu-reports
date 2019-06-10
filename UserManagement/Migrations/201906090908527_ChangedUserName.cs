namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Language", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FathersName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FathersName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.Publications", "Language");
        }
    }
}
