using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer(new Initializer());
        }

        public Context() : base("DefaultConnection") { }

        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteStation> RouteStations { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<FavouriteStation> FavouriteStations { get; set; }

        public DbSet<User> Users { get; set; }

        public class Initializer : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                IRepository _fileRepository = Factory.Default.GetRepository<FileRepository>();
                _fileRepository.FillRoutesAndStations();

                foreach (var route in _fileRepository.Routes)
                    context.Routes.Add(route);

                foreach (var st in _fileRepository.Stations)
                    context.Stations.Add(st);

                context.SaveChanges();
            }
        }
    }
}
