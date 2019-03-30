namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class additionalInfoUser : DbMigration
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
                "dbo.Positions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScienceDegrees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "GraduationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "AwardingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "DefenseYear", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "AcademicStatus_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Cathedra_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Position_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ScienceDegree_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "AcademicStatus_ID");
            CreateIndex("dbo.AspNetUsers", "Cathedra_ID");
            CreateIndex("dbo.AspNetUsers", "Position_ID");
            CreateIndex("dbo.AspNetUsers", "ScienceDegree_ID");
            AddForeignKey("dbo.AspNetUsers", "AcademicStatus_ID", "dbo.AcademicStatus", "ID");
            AddForeignKey("dbo.AspNetUsers", "Cathedra_ID", "dbo.Cathedras", "ID");
            AddForeignKey("dbo.AspNetUsers", "Position_ID", "dbo.Positions", "ID");
            AddForeignKey("dbo.AspNetUsers", "ScienceDegree_ID", "dbo.ScienceDegrees", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ScienceDegree_ID", "dbo.ScienceDegrees");
            DropForeignKey("dbo.AspNetUsers", "Position_ID", "dbo.Positions");
            DropForeignKey("dbo.AspNetUsers", "Cathedra_ID", "dbo.Cathedras");
            DropForeignKey("dbo.Cathedras", "Faculty_ID", "dbo.Faculties");
            DropForeignKey("dbo.AspNetUsers", "AcademicStatus_ID", "dbo.AcademicStatus");
            DropIndex("dbo.Cathedras", new[] { "Faculty_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "ScienceDegree_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Position_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Cathedra_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "AcademicStatus_ID" });
            DropColumn("dbo.AspNetUsers", "ScienceDegree_ID");
            DropColumn("dbo.AspNetUsers", "Position_ID");
            DropColumn("dbo.AspNetUsers", "Cathedra_ID");
            DropColumn("dbo.AspNetUsers", "AcademicStatus_ID");
            DropColumn("dbo.AspNetUsers", "DefenseYear");
            DropColumn("dbo.AspNetUsers", "AwardingDate");
            DropColumn("dbo.AspNetUsers", "GraduationDate");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropTable("dbo.ScienceDegrees");
            DropTable("dbo.Positions");
            DropTable("dbo.Faculties");
            DropTable("dbo.Cathedras");
            DropTable("dbo.AcademicStatus");
        }
    }
}
