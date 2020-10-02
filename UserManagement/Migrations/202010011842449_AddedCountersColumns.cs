namespace UserManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCountersColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MonographCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "BookCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "TrainingBookCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "OtherWritingCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConferenceCounterBeforeRegistration", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PatentCounterBeforeRegistration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PatentCounterBeforeRegistration");
            DropColumn("dbo.AspNetUsers", "ConferenceCounterBeforeRegistration");
            DropColumn("dbo.AspNetUsers", "OtherWritingCounterBeforeRegistration");
            DropColumn("dbo.AspNetUsers", "TrainingBookCounterBeforeRegistration");
            DropColumn("dbo.AspNetUsers", "BookCounterBeforeRegistration");
            DropColumn("dbo.AspNetUsers", "MonographCounterBeforeRegistration");
        }
    }
}
