namespace SmartAudit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameOrderToRankAndSeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PeriodTypes", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.Quarters", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.SectionDefinitions", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionDefinitions", "Rank", c => c.Int(nullable: false));
            DropColumn("dbo.Quarters", "Rank");
            DropColumn("dbo.SectionDefinitions", "Rank");
            DropColumn("dbo.QuestionDefinitions", "Rank");

            Sql("INSERT INTO Quarters(Name,Rank) VALUES('Q1',1)");
            Sql("INSERT INTO Quarters(Name,Rank) VALUES('Q2',2)");
            Sql("INSERT INTO Quarters(Name,Rank) VALUES('Q3',3)");
            Sql("INSERT INTO Quarters(Name,Rank) VALUES('Q4',4)");

            Sql("INSERT INTO PeriodTypes(Name,Rank) VALUES('Yearly',1)");
            Sql("INSERT INTO PeriodTypes(Name,Rank) VALUES('Monthly',2)");
            Sql("INSERT INTO PeriodTypes(Name,Rank) VALUES('Quarterly',3)");
            Sql("INSERT INTO PeriodTypes(Name,Rank) VALUES('Special',4)");


        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionDefinitions", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.SectionDefinitions", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.Quarters", "Rank", c => c.Int(nullable: false));
            DropColumn("dbo.QuestionDefinitions", "Rank");
            DropColumn("dbo.SectionDefinitions", "Rank");
            DropColumn("dbo.Quarters", "Rank");
            DropColumn("dbo.PeriodTypes", "Rank");
        }
    }
}
