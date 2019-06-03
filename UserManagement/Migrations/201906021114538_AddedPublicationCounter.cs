namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublicationCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PublicationCounterBeforeRegistration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PublicationCounterBeforeRegistration");
        }
    }
}
