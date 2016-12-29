namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAuditStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditStatus", "Rank", c => c.Int(nullable: false));
            DropColumn("dbo.AuditStatus", "Rank");

            Sql("INSERT INTO AuditStatus(Name,Rank) VALUES('In-Progress',1)");
            Sql("INSERT INTO AuditStatus(Name,Rank) VALUES('Submitted',2)");
            Sql("INSERT INTO AuditStatus(Name,Rank) VALUES('Completed',3)");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditStatus", "Rank", c => c.Int(nullable: false));
            DropColumn("dbo.AuditStatus", "Rank");
        }
    }
}
