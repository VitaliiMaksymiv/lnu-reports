namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePublication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserPublications",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Publication_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Publication_ID);
            
            AddColumn("dbo.Publications", "OtherAuthors", c => c.String());
            AddColumn("dbo.Publications", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Publications", "SizeOfPages", c => c.Double(nullable: false));
            AddColumn("dbo.Publications", "PublicationType", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserPublications", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ApplicationUserPublications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserPublications", new[] { "Publication_ID" });
            DropIndex("dbo.ApplicationUserPublications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Publications", "PublicationType");
            DropColumn("dbo.Publications", "SizeOfPages");
            DropColumn("dbo.Publications", "Date");
            DropColumn("dbo.Publications", "OtherAuthors");
            DropTable("dbo.ApplicationUserPublications");
        }
    }
}
