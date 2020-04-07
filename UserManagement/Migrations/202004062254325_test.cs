namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cathedras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Faculty_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Faculties", t => t.Faculty_ID)
                .Index(t => t.Faculty_ID);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThemeOfScientificWorks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        ThemeNumber = c.String(),
                        ScientificHead = c.String(),
                        PeriodFrom = c.DateTime(nullable: false),
                        PeriodTo = c.DateTime(nullable: false),
                        Financial = c.Int(nullable: false),
                        Cathedra_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cathedras", t => t.Cathedra_ID)
                .Index(t => t.Cathedra_ID);
            
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
                        Protocol = c.String(),
                        Date = c.DateTime(),
                        IsSigned = c.Boolean(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        ThemeOfScientificWork_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ThemeOfScientificWorks", t => t.ThemeOfScientificWork_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ThemeOfScientificWork_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OtherAuthors = c.String(),
                        Date = c.DateTime(nullable: false),
                        SizeOfPages = c.Double(nullable: false),
                        PublicationType = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        Link = c.String(),
                        Edition = c.String(),
                        Magazine = c.String(),
                        DOI = c.String(),
                        Tome = c.String(),
                        Place = c.String(),
                        MainAuthor = c.String(),
                        IsMainAuthorRegistered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PublicationCounterBeforeRegistration = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        GraduationDate = c.DateTime(nullable: false),
                        AwardingDate = c.DateTime(nullable: false),
                        DefenseYear = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        AcademicStatus_ID = c.Int(),
                        Cathedra_ID = c.Int(),
                        Position_ID = c.Int(),
                        ScienceDegree_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicStatus", t => t.AcademicStatus_ID)
                .ForeignKey("dbo.Cathedras", t => t.Cathedra_ID)
                .ForeignKey("dbo.Positions", t => t.Position_ID)
                .ForeignKey("dbo.ScienceDegrees", t => t.ScienceDegree_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.AcademicStatus_ID)
                .Index(t => t.Cathedra_ID)
                .Index(t => t.Position_ID)
                .Index(t => t.ScienceDegree_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.I18nUserInitials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Language = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FathersName = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ScienceDegrees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.CathedraReportReports",
                c => new
                    {
                        CathedraReport_ID = c.Int(nullable: false),
                        Report_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CathedraReport_ID, t.Report_ID })
                .ForeignKey("dbo.CathedraReports", t => t.CathedraReport_ID, cascadeDelete: true)
                .ForeignKey("dbo.Reports", t => t.Report_ID, cascadeDelete: true)
                .Index(t => t.CathedraReport_ID)
                .Index(t => t.Report_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reports", "ThemeOfScientificWork_ID", "dbo.ThemeOfScientificWorks");
            DropForeignKey("dbo.ReportPublication2", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication2", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.ReportPublication1", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublication1", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.ReportPublications", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ReportPublications", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.CathedraReportReports", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.CathedraReportReports", "CathedraReport_ID", "dbo.CathedraReports");
            DropForeignKey("dbo.AspNetUsers", "ScienceDegree_ID", "dbo.ScienceDegrees");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserPublications", "Publication_ID", "dbo.Publications");
            DropForeignKey("dbo.ApplicationUserPublications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Position_ID", "dbo.Positions");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.I18nUserInitials", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CathedraReports", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Cathedra_ID", "dbo.Cathedras");
            DropForeignKey("dbo.AspNetUsers", "AcademicStatus_ID", "dbo.AcademicStatus");
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
            DropForeignKey("dbo.ThemeOfScientificWorks", "Cathedra_ID", "dbo.Cathedras");
            DropForeignKey("dbo.Cathedras", "Faculty_ID", "dbo.Faculties");
            DropIndex("dbo.ReportPublication2", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication2", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublication1", new[] { "Report_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.ReportPublications", new[] { "Report_ID" });
            DropIndex("dbo.CathedraReportReports", new[] { "Report_ID" });
            DropIndex("dbo.CathedraReportReports", new[] { "CathedraReport_ID" });
            DropIndex("dbo.ApplicationUserPublications", new[] { "Publication_ID" });
            DropIndex("dbo.ApplicationUserPublications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication2", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublication1", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "Publication_ID" });
            DropIndex("dbo.CathedraReportPublications", new[] { "CathedraReport_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.I18nUserInitials", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ScienceDegree_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Position_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Cathedra_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "AcademicStatus_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.OtherDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CoworkersDefenses", new[] { "CathedraReport_ID" });
            DropIndex("dbo.CathedraReports", new[] { "User_Id" });
            DropIndex("dbo.CathedraReports", new[] { "ThemeInWorkTime_ID" });
            DropIndex("dbo.CathedraReports", new[] { "HospDohovirTheme_ID" });
            DropIndex("dbo.CathedraReports", new[] { "BudgetTheme_ID" });
            DropIndex("dbo.Reports", new[] { "User_Id" });
            DropIndex("dbo.Reports", new[] { "ThemeOfScientificWork_ID" });
            DropIndex("dbo.ThemeOfScientificWorks", new[] { "Cathedra_ID" });
            DropIndex("dbo.Cathedras", new[] { "Faculty_ID" });
            DropTable("dbo.ReportPublication2");
            DropTable("dbo.ReportPublication1");
            DropTable("dbo.ReportPublications");
            DropTable("dbo.CathedraReportReports");
            DropTable("dbo.ApplicationUserPublications");
            DropTable("dbo.CathedraReportPublication2");
            DropTable("dbo.CathedraReportPublication1");
            DropTable("dbo.CathedraReportPublications");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ScienceDegrees");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Positions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.I18nUserInitials");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.OtherDefenses");
            DropTable("dbo.CathedraDefenses");
            DropTable("dbo.CoworkersDefenses");
            DropTable("dbo.CathedraReports");
            DropTable("dbo.Publications");
            DropTable("dbo.Reports");
            DropTable("dbo.ThemeOfScientificWorks");
            DropTable("dbo.Faculties");
            DropTable("dbo.Cathedras");
            DropTable("dbo.AcademicStatus");
        }
    }
}
