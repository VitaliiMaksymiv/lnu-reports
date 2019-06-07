namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "IsSigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "IsSigned");
        }
    }
}
