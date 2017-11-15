namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Measurement", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Data", "MeasurementId", "dbo.Measurement");
            DropForeignKey("dbo.Data", "ParameterId", "dbo.Parameter");
            DropForeignKey("dbo.Testing", "MeasurementId", "dbo.Measurement");
            DropForeignKey("dbo.ExaminationResult", "TestingId", "dbo.Testing");
            DropIndex("dbo.Measurement", new[] { "UserId" });
            DropIndex("dbo.Data", new[] { "MeasurementId" });
            DropIndex("dbo.Data", new[] { "ParameterId" });
            DropIndex("dbo.Testing", new[] { "MeasurementId" });
            DropIndex("dbo.ExaminationResult", new[] { "TestingId" });
            CreateTable(
                "dbo.MedicalRecord",
                c => new
                    {
                        MedicalRecordId = c.Int(nullable: false, identity: true),
                        MedicalRecordDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicalRecordId)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Examination",
                c => new
                    {
                        ExaminationId = c.Int(nullable: false, identity: true),
                        ExaminationDate = c.DateTime(nullable: false),
                        MedicalRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExaminationId)
                .ForeignKey("dbo.MedicalRecord", t => t.MedicalRecordId, cascadeDelete: true)
                .Index(t => t.MedicalRecordId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TestId);
            
            AddColumn("dbo.Measurement", "ParameterId", c => c.Int(nullable: false));
            AddColumn("dbo.Measurement", "Value", c => c.String());
            AddColumn("dbo.Measurement", "MedicalRecordId", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "ExaminationId", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "TestId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Measurement", "MedicalRecordId", "dbo.MedicalRecord", "MedicalRecordId", cascadeDelete: true);
            AddForeignKey("dbo.Measurement", "ParameterId", "dbo.Parameter", "ParameterId", cascadeDelete: true);
            AddForeignKey("dbo.ExaminationResult", "ExaminationId", "dbo.Examination", "ExaminationId", cascadeDelete: true);
            CreateIndex("dbo.Measurement", "MedicalRecordId");
            CreateIndex("dbo.Measurement", "ParameterId");
            CreateIndex("dbo.ExaminationResult", "ExaminationId");
            DropColumn("dbo.Measurement", "MeasurementDate");
            DropColumn("dbo.Measurement", "UserId");
            DropColumn("dbo.ExaminationResult", "TestingId");
            DropColumn("dbo.ExaminationResult", "TestName");
            DropTable("dbo.Data");
            DropTable("dbo.Testing");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Testing",
                c => new
                    {
                        TestingId = c.Int(nullable: false, identity: true),
                        TestingDate = c.DateTime(nullable: false),
                        MeasurementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestingId);
            
            CreateTable(
                "dbo.Data",
                c => new
                    {
                        DataId = c.Int(nullable: false, identity: true),
                        ParameterId = c.Int(nullable: false),
                        Value = c.String(),
                        MeasurementId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DataId);
            
            AddColumn("dbo.ExaminationResult", "TestName", c => c.Int(nullable: false));
            AddColumn("dbo.ExaminationResult", "TestingId", c => c.Int(nullable: false));
            AddColumn("dbo.Measurement", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Measurement", "MeasurementDate", c => c.DateTime(nullable: false));
            DropIndex("dbo.ExaminationResult", new[] { "ExaminationId" });
            DropIndex("dbo.Examination", new[] { "MedicalRecordId" });
            DropIndex("dbo.Measurement", new[] { "ParameterId" });
            DropIndex("dbo.Measurement", new[] { "MedicalRecordId" });
            DropIndex("dbo.MedicalRecord", new[] { "UserId" });
            DropForeignKey("dbo.ExaminationResult", "ExaminationId", "dbo.Examination");
            DropForeignKey("dbo.Examination", "MedicalRecordId", "dbo.MedicalRecord");
            DropForeignKey("dbo.Measurement", "ParameterId", "dbo.Parameter");
            DropForeignKey("dbo.Measurement", "MedicalRecordId", "dbo.MedicalRecord");
            DropForeignKey("dbo.MedicalRecord", "UserId", "dbo.UserProfile");
            DropColumn("dbo.ExaminationResult", "TestId");
            DropColumn("dbo.ExaminationResult", "ExaminationId");
            DropColumn("dbo.Measurement", "MedicalRecordId");
            DropColumn("dbo.Measurement", "Value");
            DropColumn("dbo.Measurement", "ParameterId");
            DropTable("dbo.Test");
            DropTable("dbo.Examination");
            DropTable("dbo.MedicalRecord");
            CreateIndex("dbo.ExaminationResult", "TestingId");
            CreateIndex("dbo.Testing", "MeasurementId");
            CreateIndex("dbo.Data", "ParameterId");
            CreateIndex("dbo.Data", "MeasurementId");
            CreateIndex("dbo.Measurement", "UserId");
            AddForeignKey("dbo.ExaminationResult", "TestingId", "dbo.Testing", "TestingId", cascadeDelete: true);
            AddForeignKey("dbo.Testing", "MeasurementId", "dbo.Measurement", "MeasurementId", cascadeDelete: true);
            AddForeignKey("dbo.Data", "ParameterId", "dbo.Parameter", "ParameterId", cascadeDelete: true);
            AddForeignKey("dbo.Data", "MeasurementId", "dbo.Measurement", "MeasurementId", cascadeDelete: true);
            AddForeignKey("dbo.Measurement", "UserId", "dbo.UserProfile", "UserId", cascadeDelete: true);
        }
    }
}
