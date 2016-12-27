namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomethingChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuditDefinitions", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuditDefinitions", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
