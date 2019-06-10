namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedI18nUsersInitials : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.I18nUserInitials", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.I18nUserInitials", new[] { "User_Id" });
            DropTable("dbo.I18nUserInitials");
        }
    }
}
