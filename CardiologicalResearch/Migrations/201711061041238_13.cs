namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestResult", "ExaminationId", "dbo.Examination");
            DropIndex("dbo.TestResult", new[] { "ExaminationId" });
            CreateTable(
                "dbo.ParameterValue",
                c => new
                    {
                        ParameterValueId = c.Int(nullable: false, identity: true),
                        ParameterId = c.Int(nullable: false),
                        IntValue = c.Int(nullable: false),
                        StringValue = c.String(),
                    })
                .PrimaryKey(t => t.ParameterValueId)
                .ForeignKey("dbo.Parameter", t => t.ParameterId, cascadeDelete: true)
                .Index(t => t.ParameterId);
            
            CreateTable(
                "dbo.ExaminationResult",
                c => new
                    {
                        ExaminationResultId = c.Int(nullable: false, identity: true),
                        ExaminationId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExaminationResultId)
                .ForeignKey("dbo.Examination", t => t.ExaminationId, cascadeDelete: true)
                .Index(t => t.ExaminationId);
            
            AddColumn("dbo.TestResult", "Name", c => c.String());
            AddColumn("dbo.SCORE", "IsSmoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "IsSmoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "MinAge", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "MaxAge", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "MinwBP", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "MaxBP", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "MinCholesterol", c => c.Double(nullable: false));
            AddColumn("dbo.SCOREdata", "MaxCholesterol", c => c.Double(nullable: false));
            AlterColumn("dbo.SCORE", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.SCOREdata", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.TestResult", "ExaminationId");
            DropColumn("dbo.TestResult", "TestId");
            DropColumn("dbo.TestResult", "Result");
            DropColumn("dbo.SCORE", "smoker");
            DropColumn("dbo.SCOREdata", "smoker");
            DropColumn("dbo.SCOREdata", "lowAge");
            DropColumn("dbo.SCOREdata", "upAge");
            DropColumn("dbo.SCOREdata", "lowBP");
            DropColumn("dbo.SCOREdata", "upBP");
            DropColumn("dbo.SCOREdata", "lowCholesterol");
            DropColumn("dbo.SCOREdata", "upCholesterol");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SCOREdata", "upCholesterol", c => c.Double(nullable: false));
            AddColumn("dbo.SCOREdata", "lowCholesterol", c => c.Double(nullable: false));
            AddColumn("dbo.SCOREdata", "upBP", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "lowBP", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "upAge", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "lowAge", c => c.Int(nullable: false));
            AddColumn("dbo.SCOREdata", "smoker", c => c.Int(nullable: false));
            AddColumn("dbo.SCORE", "smoker", c => c.Int(nullable: false));
            AddColumn("dbo.TestResult", "Result", c => c.Int(nullable: false));
            AddColumn("dbo.TestResult", "TestId", c => c.Int(nullable: false));
            AddColumn("dbo.TestResult", "ExaminationId", c => c.Int(nullable: false));
            DropIndex("dbo.ExaminationResult", new[] { "ExaminationId" });
            DropIndex("dbo.ParameterValue", new[] { "ParameterId" });
            DropForeignKey("dbo.ExaminationResult", "ExaminationId", "dbo.Examination");
            DropForeignKey("dbo.ParameterValue", "ParameterId", "dbo.Parameter");
            AlterColumn("dbo.SCOREdata", "gender", c => c.Int(nullable: false));
            AlterColumn("dbo.SCORE", "gender", c => c.Int(nullable: false));
            DropColumn("dbo.SCOREdata", "MaxCholesterol");
            DropColumn("dbo.SCOREdata", "MinCholesterol");
            DropColumn("dbo.SCOREdata", "MaxBP");
            DropColumn("dbo.SCOREdata", "MinwBP");
            DropColumn("dbo.SCOREdata", "MaxAge");
            DropColumn("dbo.SCOREdata", "MinAge");
            DropColumn("dbo.SCOREdata", "IsSmoker");
            DropColumn("dbo.SCORE", "IsSmoker");
            DropColumn("dbo.TestResult", "Name");
            DropTable("dbo.ExaminationResult");
            DropTable("dbo.ParameterValue");
            CreateIndex("dbo.TestResult", "ExaminationId");
            AddForeignKey("dbo.TestResult", "ExaminationId", "dbo.Examination", "ExaminationId", cascadeDelete: true);
        }
    }
}
