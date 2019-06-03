namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParticipationInGrands = c.String(),
                        ScientificTrainings = c.String(),
                        ScientificControlDoctorsWork = c.String(),
                        ScientificControlStudentsWork = c.String(),
                        ApplicationForInevention = c.String(),
                        PatentForInevention = c.String(),
                        ReviewForTheses = c.String(),
                        MembershipInCouncils = c.String(),
                        Other = c.String(),
                        ThemeOfScientificWorkDescription = c.String(),
                        ThemeOfScientificWork_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWork_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ThemeOfScientificWork_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ReportPublications",
                c => new
                    {
                        Report_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ID, t.Publication_ID })
                .ForeignKey("dbo.Reports", t => t.Report_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.Report_ID)
                .Index(t => t.Publication_ID);
            
            CreateTable(
                "dbo.ReportPublication1",
                c => new
                    {
                        Report_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ID, t.Publication_ID })
                .ForeignKey("dbo.Reports", t => t.Report_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.Report_ID)
                .Index(t => t.Publication_ID);
            
            CreateTable(
                "dbo.ReportPublication2",
                c => new
                    {
                        Report_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ID, t.Publication_ID })
                .ForeignKey("dbo.Reports", t => t.Report_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.Report_ID)
                .Index(t => t.Publication_ID);
            
            AddColumn("dbo.Publications", "MainAuthor", c => c.String());
            AddColumn("dbo.Publications", "IsMainAuthorRegistered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reports", "ThemeOfScientificWork_ID", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ReportPublication2", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication2", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.ReportPublication1", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication1", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.ReportPublications", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublications", "Report_ID", "dbo.Reports");
            DropIndex("dbo.ReportPublication2", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication2", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Report_ID" });
            DropIndex("dbo.Reports", new[] { "User_Id" });
            DropIndex("dbo.Reports", new[] { "ThemeOfScientificWork_ID" });
            DropColumn("dbo.Publications", "IsMainAuthorRegistered");
            DropColumn("dbo.Publications", "MainAuthor");
            DropTable("dbo.ReportPublication2");
            DropTable("dbo.ReportPublication1");
            DropTable("dbo.ReportPublications");
            DropTable("dbo.Reports");
        }
    }
}
