namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConfirmedReportFieldNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reports", "Date", c => c.DateTime(nullable: false));
        }
    }
}
