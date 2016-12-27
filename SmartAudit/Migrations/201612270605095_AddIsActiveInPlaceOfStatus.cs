namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveInPlaceOfStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditDefinitions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SectionDefinitions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionDefinitions", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuestionDefinitions", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionDefinitions", "Status", c => c.String());
            DropColumn("dbo.QuestionDefinitions", "IsActive");
            DropColumn("dbo.SectionDefinitions", "IsActive");
            DropColumn("dbo.AuditDefinitions", "IsActive");
        }
    }
}
