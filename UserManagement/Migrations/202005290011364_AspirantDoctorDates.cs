namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AspirantDoctorDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AspirantStartYear", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "AspirantFinishYear", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DoctorStartYear", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DoctorFinishYear", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DoctorFinishYear");
            DropColumn("dbo.AspNetUsers", "DoctorStartYear");
            DropColumn("dbo.AspNetUsers", "AspirantFinishYear");
            DropColumn("dbo.AspNetUsers", "AspirantStartYear");
        }
    }
}
