namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentIdInSectionAndQuestionDefinition : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SectionDefinitions", "AuditDefinition_Id", "dbo.AuditDefinitions");
            DropForeignKey("dbo.QuestionDefinitions", "SectionDefinition_Id", "dbo.SectionDefinitions");
            DropIndex("dbo.SectionDefinitions", new[] { "AuditDefinition_Id" });
            DropIndex("dbo.QuestionDefinitions", new[] { "SectionDefinition_Id" });
            RenameColumn(table: "dbo.SectionDefinitions", name: "AuditDefinition_Id", newName: "AuditDefinitionId");
            RenameColumn(table: "dbo.QuestionDefinitions", name: "SectionDefinition_Id", newName: "SectionDefinitionId");
            AlterColumn("dbo.SectionDefinitions", "AuditDefinitionId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionDefinitions", "SectionDefinitionId", c => c.Int(nullable: false));
            CreateIndex("dbo.SectionDefinitions", "AuditDefinitionId");
            CreateIndex("dbo.QuestionDefinitions", "SectionDefinitionId");
            AddForeignKey("dbo.SectionDefinitions", "AuditDefinitionId", "dbo.AuditDefinitions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionDefinitions", "SectionDefinitionId", "dbo.SectionDefinitions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionDefinitions", "SectionDefinitionId", "dbo.SectionDefinitions");
            DropForeignKey("dbo.SectionDefinitions", "AuditDefinitionId", "dbo.AuditDefinitions");
            DropIndex("dbo.QuestionDefinitions", new[] { "SectionDefinitionId" });
            DropIndex("dbo.SectionDefinitions", new[] { "AuditDefinitionId" });
            AlterColumn("dbo.QuestionDefinitions", "SectionDefinitionId", c => c.Int());
            AlterColumn("dbo.SectionDefinitions", "AuditDefinitionId", c => c.Int());
            RenameColumn(table: "dbo.QuestionDefinitions", name: "SectionDefinitionId", newName: "SectionDefinition_Id");
            RenameColumn(table: "dbo.SectionDefinitions", name: "AuditDefinitionId", newName: "AuditDefinition_Id");
            CreateIndex("dbo.QuestionDefinitions", "SectionDefinition_Id");
            CreateIndex("dbo.SectionDefinitions", "AuditDefinition_Id");
            AddForeignKey("dbo.QuestionDefinitions", "SectionDefinition_Id", "dbo.SectionDefinitions", "Id");
            AddForeignKey("dbo.SectionDefinitions", "AuditDefinition_Id", "dbo.AuditDefinitions", "Id");
        }
    }
}
