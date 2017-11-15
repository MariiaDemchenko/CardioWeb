namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02112017 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Data", "ParameterId");
            DropColumn("dbo.ExaminationResult", "TestName");
            DropColumn("dbo.ExaminationResult", "Result");
            DropColumn("dbo.SCORE", "gender");
            DropColumn("dbo.SCORE", "smoker");
            DropColumn("dbo.SCOREdata", "gender");
            DropColumn("dbo.SCOREdata", "smoker");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SCOREdata", "smoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "gender", c => c.Int(nullable: false));
            AddColumn("dbo.SCORE", "smoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCORE", "gender", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "Result", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "TestName", c => c.Int(nullable: false));
            AddColumn("dbo.Data", "ParameterId", c => c.Int(nullable: false));
        }
    }
}
