namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "AuthorsOrder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "AuthorsOrder");
        }
    }
}
