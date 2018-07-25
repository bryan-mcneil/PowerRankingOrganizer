namespace PowerRankingOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BonusMeme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentPlayers", "Bonus", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Bonus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Bonus");
            DropColumn("dbo.CurrentPlayers", "Bonus");
        }
    }
}
