namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedLookupValues : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Audits", "AuditStatusId", "dbo.AuditStatus");
            DropForeignKey("dbo.Audits", "PeriodTypeId", "dbo.PeriodTypes");
            DropForeignKey("dbo.Audits", "QuarterId", "dbo.Quarters");
            DropIndex("dbo.Audits", new[] { "PeriodTypeId" });
            DropIndex("dbo.Audits", new[] { "QuarterId" });
            DropIndex("dbo.Audits", new[] { "AuditStatusId" });
            DropPrimaryKey("dbo.AuditStatus");
            DropPrimaryKey("dbo.PeriodTypes");
            DropPrimaryKey("dbo.Quarters");
            AddColumn("dbo.Audits", "AuditStatus_Id", c => c.Byte());
            AddColumn("dbo.Audits", "PeriodType_Id", c => c.Byte());
            AddColumn("dbo.Audits", "Quarter_Id", c => c.Byte());
            AlterColumn("dbo.AuditStatus", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.PeriodTypes", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Quarters", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.AuditStatus", "Id");
            AddPrimaryKey("dbo.PeriodTypes", "Id");
            AddPrimaryKey("dbo.Quarters", "Id");
            CreateIndex("dbo.Audits", "AuditStatus_Id");
            CreateIndex("dbo.Audits", "PeriodType_Id");
            CreateIndex("dbo.Audits", "Quarter_Id");
            AddForeignKey("dbo.Audits", "AuditStatus_Id", "dbo.AuditStatus", "Id");
            AddForeignKey("dbo.Audits", "PeriodType_Id", "dbo.PeriodTypes", "Id");
            AddForeignKey("dbo.Audits", "Quarter_Id", "dbo.Quarters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Audits", "Quarter_Id", "dbo.Quarters");
            DropForeignKey("dbo.Audits", "PeriodType_Id", "dbo.PeriodTypes");
            DropForeignKey("dbo.Audits", "AuditStatus_Id", "dbo.AuditStatus");
            DropIndex("dbo.Audits", new[] { "Quarter_Id" });
            DropIndex("dbo.Audits", new[] { "PeriodType_Id" });
            DropIndex("dbo.Audits", new[] { "AuditStatus_Id" });
            DropPrimaryKey("dbo.Quarters");
            DropPrimaryKey("dbo.PeriodTypes");
            DropPrimaryKey("dbo.AuditStatus");
            AlterColumn("dbo.Quarters", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PeriodTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AuditStatus", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Audits", "Quarter_Id");
            DropColumn("dbo.Audits", "PeriodType_Id");
            DropColumn("dbo.Audits", "AuditStatus_Id");
            AddPrimaryKey("dbo.Quarters", "Id");
            AddPrimaryKey("dbo.PeriodTypes", "Id");
            AddPrimaryKey("dbo.AuditStatus", "Id");
            CreateIndex("dbo.Audits", "AuditStatusId");
            CreateIndex("dbo.Audits", "QuarterId");
            CreateIndex("dbo.Audits", "PeriodTypeId");
            AddForeignKey("dbo.Audits", "QuarterId", "dbo.Quarters", "Id");
            AddForeignKey("dbo.Audits", "PeriodTypeId", "dbo.PeriodTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Audits", "AuditStatusId", "dbo.AuditStatus", "Id", cascadeDelete: true);
        }
    }
}
