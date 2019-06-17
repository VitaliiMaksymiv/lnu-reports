namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPlaceFieldToPublication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Place", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Place");
        }
    }
}
