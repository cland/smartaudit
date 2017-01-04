namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PenaltyValueInQuestionDefinition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionDefinitions", "PenaltyValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionDefinitions", "PenaltyValue");
        }
    }
}
