namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId", "dbo.MedicalRecord");
            DropForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId1", "dbo.MedicalRecord");
            DropForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId2", "dbo.MedicalRecord");
            DropForeignKey("dbo.Data", "Testing_TestingId", "dbo.Testing");
            DropIndex("dbo.Data", new[] { "MedicalRecord_MedicalRecordId" });
            DropIndex("dbo.Data", new[] { "MedicalRecord_MedicalRecordId1" });
            DropIndex("dbo.Data", new[] { "MedicalRecord_MedicalRecordId2" });
            DropIndex("dbo.Data", new[] { "Testing_TestingId" });
            RenameColumn(table: "dbo.Data", name: "MedicalRecord_MedicalRecordId", newName: "MedicalRecordId");
            AddForeignKey("dbo.Data", "MedicalRecordId", "dbo.MedicalRecord", "MedicalRecordId", cascadeDelete: true);
            CreateIndex("dbo.Data", "MedicalRecordId");
            DropColumn("dbo.Data", "MedicalRecord_MedicalRecordId1");
            DropColumn("dbo.Data", "MedicalRecord_MedicalRecordId2");
            DropColumn("dbo.Data", "Testing_TestingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Data", "Testing_TestingId", c => c.Int());
            AddColumn("dbo.Data", "MedicalRecord_MedicalRecordId2", c => c.Int());
            AddColumn("dbo.Data", "MedicalRecord_MedicalRecordId1", c => c.Int());
            DropIndex("dbo.Data", new[] { "MedicalRecordId" });
            DropForeignKey("dbo.Data", "MedicalRecordId", "dbo.MedicalRecord");
            RenameColumn(table: "dbo.Data", name: "MedicalRecordId", newName: "MedicalRecord_MedicalRecordId");
            CreateIndex("dbo.Data", "Testing_TestingId");
            CreateIndex("dbo.Data", "MedicalRecord_MedicalRecordId2");
            CreateIndex("dbo.Data", "MedicalRecord_MedicalRecordId1");
            CreateIndex("dbo.Data", "MedicalRecord_MedicalRecordId");
            AddForeignKey("dbo.Data", "Testing_TestingId", "dbo.Testing", "TestingId");
            AddForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId2", "dbo.MedicalRecord", "MedicalRecordId");
            AddForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId1", "dbo.MedicalRecord", "MedicalRecordId");
            AddForeignKey("dbo.Data", "MedicalRecord_MedicalRecordId", "dbo.MedicalRecord", "MedicalRecordId");
        }
    }
}
