namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReports : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Protocol", c => c.String());
            AddColumn("dbo.Reports", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "Date");
            DropColumn("dbo.Reports", "Protocol");
        }
    }
}
