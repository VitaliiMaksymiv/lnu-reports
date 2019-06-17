namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCathedraReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CathedraReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AchivementSchool = c.String(),
                        AllDescriptionBudgetTheme = c.String(),
                        CVBudgetTheme = c.String(),
                        DefensesOfCoworkersBudgetTheme = c.String(),
                        ApplicationAndPatentsOnInventionBudgetTheme = c.String(),
                        OtherBudgetTheme = c.String(),
                        AllDescriptionThemeInWorkTime = c.String(),
                        CVThemeInWorkTime = c.String(),
                        DefensesOfCoworkersThemeInWorkTime = c.String(),
                        ApplicationAndPatentsOnInventionThemeInWorkTime = c.String(),
                        OtherThemeInWorkTime = c.String(),
                        AllDescriptionHospDohovirTheme = c.String(),
                        CVHospDohovirTheme = c.String(),
                        DefensesOfCoworkersHospDohovirTheme = c.String(),
                        ApplicationAndPatentsOnInventionHospDohovirTheme = c.String(),
                        OtherHospDohovirTheme = c.String(),
                        OtherFormsOfScientificWork = c.String(),
                        CooperationWithAcadamyOfScience = c.String(),
                        CooperationWithForeignScientificInstitution = c.String(),
                        StudentsWorks = c.String(),
                        ConferencesInUniversity = c.String(),
                        ApplicationOnInvention = c.String(),
                        Patents = c.String(),
                        Materials = c.String(),
                        PropositionForNewForms = c.String(),
                        Protocol = c.String(),
                        Date = c.DateTime(),
                        BudgetTheme_ID = c.Int(),
                        HospDohovirTheme_ID = c.Int(),
                        ThemeInWorkTime_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.BudgetTheme_ID)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.HospDohovirTheme_ID)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeInWorkTime_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.BudgetTheme_ID)
                .Index(t => t.HospDohovirTheme_ID)
                .Index(t => t.ThemeInWorkTime_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CoworkersDefenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurnameAndInitials = c.String(),
                        PositionAndCathedra = c.String(),
                        Spetiality = c.String(),
                        DateOfDefense = c.DateTime(nullable: false),
                        ThemeOfWork = c.String(),
                        CathedraReport_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID)
                .Index(t => t.CathedraReport_ID);
            
            CreateTable(
                "dbo.CathedraDefenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurnameAndInitials = c.String(),
                        ScientificHead = c.String(),
                        YearOfEnd = c.Int(nullable: false),
                        DateOfInning = c.DateTime(nullable: false),
                        DateOfDefense = c.DateTime(nullable: false),
                        ThemeOfWork = c.String(),
                        CathedraReport_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID)
                .Index(t => t.CathedraReport_ID);
            
            CreateTable(
                "dbo.OtherDefenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurnameAndInitials = c.String(),
                        ScientificHead = c.String(),
                        Spetiality = c.String(),
                        DateOfDefense = c.DateTime(nullable: false),
                        ThemeOfWork = c.String(),
                        CathedraReport_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID)
                .Index(t => t.CathedraReport_ID);
            
            CreateTable(
                "dbo.CathedraReportPublications",
                c => new
                    {
                        CathedraReport_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_ID, t.Publication_ID })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.CathedraReport_ID)
                .Index(t => t.Publication_ID);
            
            CreateTable(
                "dbo.CathedraReportPublication1",
                c => new
                    {
                        CathedraReport_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_ID, t.Publication_ID })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.CathedraReport_ID)
                .Index(t => t.Publication_ID);
            
            CreateTable(
                "dbo.CathedraReportPublication2",
                c => new
                    {
                        CathedraReport_ID = c.Int(nullable: false),
                        Publication_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_ID, t.Publication_ID })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID, cascadeDelete: true)
                .ForeignKey("dbo.Publications", t => t.Publication_ID, cascadeDelete: true)
                .Index(t => t.CathedraReport_ID)
                .Index(t => t.Publication_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CathedraReports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CathedraReports", "ThemeInWorkTime_ID", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.CathedraReportPublication2", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.CathedraReportPublication2", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReportPublication1", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.CathedraReportPublication1", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReportPublications", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.CathedraReportPublications", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReports", "HospDohovirTheme_ID", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.OtherDefenses", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraDefenses", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CoworkersDefenses", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.CathedraReports", "BudgetTheme_ID", "dbo.ThemeOfScientificWorks");
            DropIndex("dbo.CathedraReportPublication2", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "CathedraReport_ID" });
            DropIndex("dbo.OtherDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CoworkersDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReports", new[] { "User_Id" });
            DropIndex("dbo.CathedraReports", new[] { "ThemeInWorkTime_ID" });
            DropIndex("dbo.CathedraReports", new[] { "HospDohovirTheme_ID" });
            DropIndex("dbo.CathedraReports", new[] { "BudgetTheme_ID" });
            DropTable("dbo.CathedraReportPublication2");
            DropTable("dbo.CathedraReportPublication1");
            DropTable("dbo.CathedraReportPublications");
            DropTable("dbo.OtherDefenses");
            DropTable("dbo.CathedraDefenses");
            DropTable("dbo.CoworkersDefenses");
            DropTable("dbo.CathedraReports");
        }
    }
}
