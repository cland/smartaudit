namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditEntitiesAndSeedLookupOptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        IsAction = c.Boolean(nullable: false),
                        QuestionResultId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionResults", t => t.QuestionResultId, cascadeDelete: true)
                .Index(t => t.QuestionResultId);
            
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuditDefinitionId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateInspectionCompleted = c.DateTime(nullable: false),
                        DateOfInspection = c.DateTime(nullable: false),
                        PeriodTypeId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(),
                        QuarterId = c.Int(),
                        AuditStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditStatus", t => t.AuditStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.PeriodTypes", t => t.PeriodTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Quarters", t => t.QuarterId)
                .Index(t => t.CandidateId)
                .Index(t => t.PeriodTypeId)
                .Index(t => t.QuarterId)
                .Index(t => t.AuditStatusId);
            
            CreateTable(
                "dbo.AuditStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PeriodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quarters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionDefinitionId = c.Int(nullable: false),
                        AuditId = c.Int(nullable: false),
                        SampleActual = c.Int(nullable: false),
                        IsApplicable = c.Boolean(nullable: false),
                        SampleDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Audits", t => t.AuditId, cascadeDelete: true)
                .Index(t => t.AuditId);

            

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionResults", "AuditId", "dbo.Audits");
            DropForeignKey("dbo.ActionComments", "QuestionResultId", "dbo.QuestionResults");
            DropForeignKey("dbo.Audits", "QuarterId", "dbo.Quarters");
            DropForeignKey("dbo.Audits", "PeriodTypeId", "dbo.PeriodTypes");
            DropForeignKey("dbo.Audits", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Audits", "AuditStatusId", "dbo.AuditStatus");
            DropIndex("dbo.QuestionResults", new[] { "AuditId" });
            DropIndex("dbo.Audits", new[] { "AuditStatusId" });
            DropIndex("dbo.Audits", new[] { "QuarterId" });
            DropIndex("dbo.Audits", new[] { "PeriodTypeId" });
            DropIndex("dbo.Audits", new[] { "CandidateId" });
            DropIndex("dbo.ActionComments", new[] { "QuestionResultId" });
            DropTable("dbo.QuestionResults");
            DropTable("dbo.Quarters");
            DropTable("dbo.PeriodTypes");
            DropTable("dbo.Candidates");
            DropTable("dbo.AuditStatus");
            DropTable("dbo.Audits");
            DropTable("dbo.ActionComments");
        }
    }
}
