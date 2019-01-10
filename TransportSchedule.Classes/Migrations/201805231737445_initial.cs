namespace TransportSchedule.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FavouriteStations", "Station_Id", c => c.Int());
            CreateIndex("dbo.FavouriteStations", "Station_Id");
            AddForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations", "Id");
            DropColumn("dbo.FavouriteStations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FavouriteStations", "Name", c => c.String());
            DropForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations");
            DropIndex("dbo.FavouriteStations", new[] { "Station_Id" });
            DropColumn("dbo.FavouriteStations", "Station_Id");
        }
    }
}
