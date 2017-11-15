namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCORE",
                c => new
                    {
                        SCOREId = c.Int(nullable: false, identity: true),
                        gender = c.Int(nullable: false),
                        smoker = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SCOREId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SCORE");
        }
    }
}
