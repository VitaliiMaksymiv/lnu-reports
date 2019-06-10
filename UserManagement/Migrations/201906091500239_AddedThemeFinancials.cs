namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThemeFinancials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "Financial", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOfScientificWorks", "Financial");
        }
    }
}
