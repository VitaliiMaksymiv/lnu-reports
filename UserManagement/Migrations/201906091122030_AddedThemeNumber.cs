namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThemeNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "ThemeNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOfScientificWorks", "ThemeNumber");
        }
    }
}
