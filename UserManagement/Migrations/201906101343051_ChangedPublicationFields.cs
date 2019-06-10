namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPublicationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Link", c => c.String());
            AddColumn("dbo.Publications", "Edition", c => c.String());
            AddColumn("dbo.Publications", "Magazine", c => c.String());
            AddColumn("dbo.Publications", "DOI", c => c.String());
            AddColumn("dbo.Publications", "Tome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Tome");
            DropColumn("dbo.Publications", "DOI");
            DropColumn("dbo.Publications", "Magazine");
            DropColumn("dbo.Publications", "Edition");
            DropColumn("dbo.Publications", "Link");
        }
    }
}
