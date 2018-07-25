namespace PowerRankingOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentPlayers", "Color", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "Color", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Color");
            DropColumn("dbo.CurrentPlayers", "Color");
        }
    }
}
