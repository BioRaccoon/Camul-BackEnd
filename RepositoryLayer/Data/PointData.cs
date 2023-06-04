using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class PointData : IEntityTypeConfiguration<Point>
    {
        public static Dictionary<string, Point> points = new Dictionary<string, Point>();

        public void Configure(EntityTypeBuilder<Point> builder)
        {
            Bootstrap();
            foreach (var point in points)
            {
                builder.HasData(point.Value);
            }
        }

        public void Bootstrap()
        {
            try
            {
                points.Add("Beacon A",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon A",
                        Description = "Beacon from Building A",
                        Image360 = "sd",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[0].Value.LocationID
                    }
                    );

                points.Add("Beacon B",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon B",
                        Description = "Beacon from Building B",
                        Image360 = "ng",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[1].Value.LocationID
                    }
                    );

                points.Add("Beacon C",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon C",
                        Description = "Beacon from Building C",
                        Image360 = "jy",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[2].Value.LocationID
                    }
                    );

                points.Add("Beacon D",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon D",
                        Description = "Beacon from Building D",
                        Image360 = "gv",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[3].Value.LocationID
                    }
                    );

                points.Add("Beacon E",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon E",
                        Description = "Beacon from Building E",
                        Image360 = "uh",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[4].Value.LocationID
                    }
                    );

                points.Add("Beacon F",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon F",
                        Description = "Beacon from Building F",
                        Image360 = "bj",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[5].Value.LocationID
                    }
                    );

                points.Add("Beacon G",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon G",
                        Description = "Beacon from Building G",
                        Image360 = "rh",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[6].Value.LocationID
                    }
                    );

                points.Add("Beacon H",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon H",
                        Description = "Beacon from Building H",
                        Image360 = "jg",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[7].Value.LocationID
                    }
                    );

                points.Add("Beacon I",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon I",
                        Description = "Beacon from Building I",
                        Image360 = "gb",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[8].Value.LocationID
                    }
                    );

                points.Add("Beacon J",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon J",
                        Description = "Beacon from Building J",
                        Image360 = "ad",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[9].Value.LocationID
                    }
                    );

                points.Add("Beacon K",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon K",
                        Description = "Beacon from Building K",
                        Image360 = "db",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[10].Value.LocationID
                    }
                    );

                points.Add("Beacon L",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon L",
                        Description = "Beacon from Building L",
                        Image360 = "cd",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[11].Value.LocationID
                    }
                    );

                points.Add("Beacon Entry Backs",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon Entry Backs",
                        Description = "Entry from parking car next to Auditorio Magno",
                        Image360 = "ws",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[12].Value.LocationID
                    }
                    );

                points.Add("Beacon Entry Main Gate",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon Entry Main Gate",
                        Description = "Entry from Building H",
                        Image360 = "rt",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[13].Value.LocationID
                    }
                    );

                points.Add("Beacon Front Entry",
                    new Point
                    {
                        PointID = Guid.NewGuid(),
                        PointName = "Beacon Front Entry",
                        Description = "Entry from building E",
                        Image360 = "gf",
                        PointLocationID = GPSCoordinatesData.locations.ToList()[14].Value.LocationID
                    }
                    );

                points.Add("Auditório Magno",
                   new Point
                   {
                       PointID = Guid.NewGuid(),
                       PointName = "Auditório Magno",
                       Description = "Auditorio Magno Building",
                       Image360 = "hb",
                       PointLocationID = GPSCoordinatesData.locations.ToList()[15].Value.LocationID
                   }
                   );
            } catch (Exception) { }
        }
    }
}
