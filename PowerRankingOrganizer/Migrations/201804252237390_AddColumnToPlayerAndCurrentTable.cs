namespace PowerRankingOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToPlayerAndCurrentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentPlayers", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.Players", "LastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "LastUpdated");
            DropColumn("dbo.CurrentPlayers", "LastUpdated");
        }
    }
}
