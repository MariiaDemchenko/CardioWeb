namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCOREdata",
                c => new
                    {
                        SCOREDataId = c.Int(nullable: false, identity: true),
                        gender = c.Int(nullable: false),
                        smoker = c.Int(nullable: false),
                        lowAge = c.Int(nullable: false),
                        upAge = c.Int(nullable: false),
                        lowBP = c.Int(nullable: false),
                        upBP = c.Int(nullable: false),
                        lowCholesterol = c.Double(nullable: false),
                        upCholesterol = c.Double(nullable: false),
                        SCORElevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SCOREDataId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SCOREdata");
        }
    }
}
