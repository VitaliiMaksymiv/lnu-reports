namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConnectionWIthReports : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CathedraReportReports", "Report_ID", "dbo.Reports");
            DropForeignKey("dbo.CathedraReportReports", "CathedraReport_ID", "dbo.CathedraReports");
            DropIndex("dbo.CathedraReportReports", new[] { "Report_ID" });
            DropIndex("dbo.CathedraReportReports", new[] { "CathedraReport_ID" });
            DropTable("dbo.CathedraReportReports");
        }
    }
}
