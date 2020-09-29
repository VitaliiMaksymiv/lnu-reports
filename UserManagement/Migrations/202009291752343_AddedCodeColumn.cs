namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCodeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOfScientificWorks", "Code");
        }
    }
}
