namespace TransportSchedule.Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavouriteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        TimeFromOrigin = c.Int(nullable: false),
                        TimeFromDest = c.Int(nullable: false),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteStations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RouteStations", "StationId", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "Route_Id", "dbo.Routes");
            DropIndex("dbo.RouteStations", new[] { "Route_Id" });
            DropIndex("dbo.RouteStations", new[] { "StationId" });
            DropIndex("dbo.FavouriteStations", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Stations");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Routes");
            DropTable("dbo.FavouriteStations");
        }
    }
}
