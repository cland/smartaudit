namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationPropertiesToQuestionResults : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.QuestionResults", "QuestionDefinitionId");
            AddForeignKey("dbo.QuestionResults", "QuestionDefinitionId", "dbo.QuestionDefinitions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionResults", "QuestionDefinitionId", "dbo.QuestionDefinitions");
            DropIndex("dbo.QuestionResults", new[] { "QuestionDefinitionId" });
        }
    }
}
