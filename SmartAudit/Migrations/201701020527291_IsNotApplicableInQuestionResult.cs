namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsNotApplicableInQuestionResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionResults", "IsNotApplicable", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuestionResults", "IsApplicable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionResults", "IsApplicable", c => c.Boolean(nullable: false));
            DropColumn("dbo.QuestionResults", "IsNotApplicable");
        }
    }
}
