namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Data", "ParameterId", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "TestName", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "Result", c => c.Int(nullable: false));
            AddColumn("dbo.SCORE", "gender", c => c.Int(nullable: false));
            AddColumn("dbo.SCORE", "smoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "gender", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "smoker", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCOREdata", "smoker");
            DropColumn("dbo.SCOREdata", "gender");
            DropColumn("dbo.SCORE", "smoker");
            DropColumn("dbo.SCORE", "gender");
            DropColumn("dbo.ExaminationResult", "Result");
            DropColumn("dbo.ExaminationResult", "TestName");
            DropColumn("dbo.Data", "ParameterId");
        }
    }
}
