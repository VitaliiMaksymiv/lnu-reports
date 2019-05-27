namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublicationTypeEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publications", "PublicationType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publications", "PublicationType", c => c.String());
        }
    }
}
