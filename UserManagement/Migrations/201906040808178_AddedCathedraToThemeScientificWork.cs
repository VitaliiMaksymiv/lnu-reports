namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCathedraToThemeScientificWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOfScientificWorks", "Cathedra_ID", c => c.Int());
            CreateIndex("dbo.ThemeOfScientificWorks", "Cathedra_ID");
            AddForeignKey("dbo.ThemeOfScientificWorks", "Cathedra_ID", "dbo.Cathedras", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeOfScientificWorks", "Cathedra_ID", "dbo.Cathedras");
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "Cathedra_ID" });
            DropColumn("dbo.ThemeOfScientificWorks", "Cathedra_ID");
        }
    }
}
