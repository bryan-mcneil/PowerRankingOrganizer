namespace PowerRankingOrganizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedColumnInPlayerAddedEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "Main", c => c.Int(nullable: false));
            AlterColumn("dbo.Players", "Secondary", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Secondary", c => c.String());
            AlterColumn("dbo.Players", "Main", c => c.String(nullable: false));
        }
    }
}
