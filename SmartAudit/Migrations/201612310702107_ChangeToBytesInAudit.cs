namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToBytesInAudit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Audits", "AuditStatus_Id", "dbo.AuditStatus");
            DropForeignKey("dbo.Audits", "PeriodType_Id", "dbo.PeriodTypes");
            DropIndex("dbo.Audits", new[] { "AuditStatus_Id" });
            DropIndex("dbo.Audits", new[] { "PeriodType_Id" });
            DropIndex("dbo.Audits", new[] { "Quarter_Id" });
            DropColumn("dbo.Audits", "AuditStatusId");
            DropColumn("dbo.Audits", "PeriodTypeId");
            DropColumn("dbo.Audits", "QuarterId");
            RenameColumn(table: "dbo.Audits", name: "AuditStatus_Id", newName: "AuditStatusId");
            RenameColumn(table: "dbo.Audits", name: "PeriodType_Id", newName: "PeriodTypeId");
            RenameColumn(table: "dbo.Audits", name: "Quarter_Id", newName: "QuarterId");
            AlterColumn("dbo.Audits", "DateInspectionCompleted", c => c.DateTime());
            AlterColumn("dbo.Audits", "PeriodTypeId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Audits", "QuarterId", c => c.Byte());
            AlterColumn("dbo.Audits", "AuditStatusId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Audits", "AuditStatusId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Audits", "PeriodTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Audits", "PeriodTypeId");
            CreateIndex("dbo.Audits", "QuarterId");
            CreateIndex("dbo.Audits", "AuditStatusId");
            AddForeignKey("dbo.Audits", "AuditStatusId", "dbo.AuditStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Audits", "PeriodTypeId", "dbo.PeriodTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Audits", "PeriodTypeId", "dbo.PeriodTypes");
            DropForeignKey("dbo.Audits", "AuditStatusId", "dbo.AuditStatus");
            DropIndex("dbo.Audits", new[] { "AuditStatusId" });
            DropIndex("dbo.Audits", new[] { "QuarterId" });
            DropIndex("dbo.Audits", new[] { "PeriodTypeId" });
            AlterColumn("dbo.Audits", "PeriodTypeId", c => c.Byte());
            AlterColumn("dbo.Audits", "AuditStatusId", c => c.Byte());
            AlterColumn("dbo.Audits", "AuditStatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Audits", "QuarterId", c => c.Int());
            AlterColumn("dbo.Audits", "PeriodTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Audits", "DateInspectionCompleted", c => c.DateTime(nullable: false));
            RenameColumn(table: "dbo.Audits", name: "QuarterId", newName: "Quarter_Id");
            RenameColumn(table: "dbo.Audits", name: "PeriodTypeId", newName: "PeriodType_Id");
            RenameColumn(table: "dbo.Audits", name: "AuditStatusId", newName: "AuditStatus_Id");
            AddColumn("dbo.Audits", "QuarterId", c => c.Int());
            AddColumn("dbo.Audits", "PeriodTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Audits", "AuditStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Audits", "Quarter_Id");
            CreateIndex("dbo.Audits", "PeriodType_Id");
            CreateIndex("dbo.Audits", "AuditStatus_Id");
            AddForeignKey("dbo.Audits", "PeriodType_Id", "dbo.PeriodTypes", "Id");
            AddForeignKey("dbo.Audits", "AuditStatus_Id", "dbo.AuditStatus", "Id");
        }
    }
}
