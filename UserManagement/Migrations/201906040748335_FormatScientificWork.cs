namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormatScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "ScientificHead", c => c.String());
            AddColumn("dbo.ThemeOfScientificWorks", "PeriodFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.ThemeOfScientificWorks", "PeriodTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOfScientificWorks", "PeriodTo");
            DropColumn("dbo.ThemeOfScientificWorks", "PeriodFrom");
            DropColumn("dbo.ThemeOfScientificWorks", "ScientificHead");
        }
    }
}
