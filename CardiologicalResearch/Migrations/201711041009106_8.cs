namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parameter",
                c => new
                    {
                        ParameterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ParameterId);
            
            AddColumn("dbo.Data", "ParameterId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Data", "ParameterId", "dbo.Parameter", "ParameterId", cascadeDelete: true);
            CreateIndex("dbo.Data", "ParameterId");
            DropColumn("dbo.Data", "ParameterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Data", "ParameterId", c => c.Int(nullable: false));
            DropIndex("dbo.Data", new[] { "ParameterId" });
            DropForeignKey("dbo.Data", "ParameterId", "dbo.Parameter");
            DropColumn("dbo.Data", "ParameterId");
            DropTable("dbo.Parameter");
        }
    }
}
