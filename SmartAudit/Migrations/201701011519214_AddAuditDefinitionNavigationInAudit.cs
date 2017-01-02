namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditDefinitionNavigationInAudit : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Audits", "AuditDefinitionId");
            AddForeignKey("dbo.Audits", "AuditDefinitionId", "dbo.AuditDefinitions", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Audits", "AuditDefinitionId", "dbo.AuditDefinitions");
            DropIndex("dbo.Audits", new[] { "AuditDefinitionId" });
        }
    }
}
