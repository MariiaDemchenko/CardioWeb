namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);*/
                
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
                "dbo.Data",
                c => new
                    {
                        DataId = c.Int(nullable: false, identity: true),
                        ParameterId = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        MedicalRecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DataId)
                .ForeignKey("dbo.MedicalRecord", t => t.MedicalRecordId, cascadeDelete: true)
                .Index(t => t.MedicalRecordId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Data", new[] { "MedicalRecordId" });
            DropIndex("dbo.MedicalRecord", new[] { "UserId" });
            DropForeignKey("dbo.Data", "MedicalRecordId", "dbo.MedicalRecord");
            DropForeignKey("dbo.MedicalRecord", "UserId", "dbo.UserProfile");
            DropTable("dbo.Data");
            DropTable("dbo.MedicalRecord");
            DropTable("dbo.UserProfile");
        }
    }
}
