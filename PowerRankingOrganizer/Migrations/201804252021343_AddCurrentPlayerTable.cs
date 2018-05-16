namespace PowerRankingOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentPlayerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RegisteredTime = c.DateTime(nullable: false),
                        Main = c.Int(nullable: false),
                        Secondary = c.Int(nullable: false),
                        PowerRank = c.Int(nullable: false),
                        SetWins = c.Int(nullable: false),
                        SetLoses = c.Int(nullable: false),
                        MatchWins = c.Int(nullable: false),
                        MatchLoses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrentPlayers");
        }
    }
}
