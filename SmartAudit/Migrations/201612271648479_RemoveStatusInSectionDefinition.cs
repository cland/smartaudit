namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStatusInSectionDefinition : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SectionDefinitions", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SectionDefinitions", "Status", c => c.String());
        }
    }
}
