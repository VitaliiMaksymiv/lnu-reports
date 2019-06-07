namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConfirmedReportField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "IsConfirmed");
        }
    }
}
