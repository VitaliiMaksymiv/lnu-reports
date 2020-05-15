namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pageschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Pages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Pages");
        }
    }
}
