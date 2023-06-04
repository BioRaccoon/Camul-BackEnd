using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace RepositoryLayer.Data
{
    public class GPSCoordinatesData : IEntityTypeConfiguration<GPSCoordinates>
    {
        public static Dictionary<string, GPSCoordinates> locations = new Dictionary<string, GPSCoordinates>();

        public static GPSCoordinates defaultLocation = new GPSCoordinates
        {
            LocationID = Guid.NewGuid(),
            Latitude = 0,
            Longitude = 0
        };

        public void Configure(EntityTypeBuilder<GPSCoordinates> builder)
        {
            Bootstrap();
            foreach (var location in locations)
            {
                builder.HasData(location.Value);
            }
            builder.HasData(defaultLocation);
        }

        public void Bootstrap()
        {
            try
            {
                //Locations have same sequence of PointData
                locations.Add("Location 1",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.178472726585674,
                    Longitude = -8.608780721644287
                }
                );

                locations.Add("Location 2",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17771178866536,
                    Longitude = -8.607758693690474 
                }
                );

                locations.Add("Location 3",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17884386335604, 
                    Longitude = -8.60702514650362
                }
                );

                locations.Add("Location 4",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17911009709587, 
                    Longitude = -8.607094559440778
                }
                );

                locations.Add("Location 5",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17946964323547, 
                    Longitude = -8.605959734847422
                    //Building R
                }
                );

                locations.Add("Location 6",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17927607581299, 
                    Longitude = -8.6078482087686
                }
                );

                locations.Add("Location 7",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17756499492956, 
                    Longitude = -8.608304798140516
                }
                );

                locations.Add("Location 8",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.177967145422194, 
                    Longitude = -8.60850622623111
                }
                );

                locations.Add("Location 9",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17810459902719, 
                    Longitude = -8.60791713982245
                }
                );

                locations.Add("Location 10",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.1784298960218, 
                    Longitude = -8.607515187397732
                }
                );

                locations.Add("Location 11",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.178984721995306, 
                    Longitude = -8.608369203693613
                }
                );

                locations.Add("Location 12",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17784126304121, 
                    Longitude = -8.607998306393574
                }
                );

                locations.Add("Location 13",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.1787732645497, 
                    Longitude = -8.608947821523687
                }
                );

                locations.Add("Location 14",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.178155377732104, 
                    Longitude = -8.608349717796676
                }
                );

                locations.Add("Location 15",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17833004000633, 
                    Longitude = -8.606887751497968
                }
                );

                locations.Add("Location 16",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17860216967359, 
                    Longitude = -8.608964585237215
                }
                );

                locations.Add("Location 17",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.1794081572219, 
                    Longitude = -8.606688057095559
                    //Building P
                }
                );

                locations.Add("Location 18",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17943581502389, 
                    Longitude = -8.607097709256596
                    //Building Q
                }
                );

                locations.Add("Location 19",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17944250803648, 
                    Longitude = -8.608661643424815
                    //Building N
                }
                );

                locations.Add("Location 20",
                new GPSCoordinates
                {
                    LocationID = Guid.NewGuid(),
                    Latitude = 41.17908478989622, 
                    Longitude = -8.608728990248784
                    //Building M
                });
            } catch (Exception) { }
        }
    }
}
