namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testing",
                c => new
                    {
                        TestingId = c.Int(nullable: false, identity: true),
                        TestingDate = c.DateTime(nullable: false),
                        MedicalRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestingId)
                .ForeignKey("dbo.MedicalRecord", t => t.MedicalRecordId, cascadeDelete: true)
                .Index(t => t.MedicalRecordId);
            
            CreateTable(
                "dbo.ExaminationResult",
                c => new
                    {
                        ExaminationResultId = c.Int(nullable: false, identity: true),
                        TestingId = c.Int(nullable: false),
                        TestName = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExaminationResultId)
                .ForeignKey("dbo.Testing", t => t.TestingId, cascadeDelete: true)
                .Index(t => t.TestingId);
            
            AddColumn("dbo.Data", "Testing_TestingId", c => c.Int());
            AddForeignKey("dbo.Data", "Testing_TestingId", "dbo.Testing", "TestingId");
            CreateIndex("dbo.Data", "Testing_TestingId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExaminationResult", new[] { "TestingId" });
            DropIndex("dbo.Testing", new[] { "MedicalRecordId" });
            DropIndex("dbo.Data", new[] { "Testing_TestingId" });
            DropForeignKey("dbo.ExaminationResult", "TestingId", "dbo.Testing");
            DropForeignKey("dbo.Testing", "MedicalRecordId", "dbo.MedicalRecord");
            DropForeignKey("dbo.Data", "Testing_TestingId", "dbo.Testing");
            DropColumn("dbo.Data", "Testing_TestingId");
            DropTable("dbo.ExaminationResult");
            DropTable("dbo.Testing");
        }
    }
}
