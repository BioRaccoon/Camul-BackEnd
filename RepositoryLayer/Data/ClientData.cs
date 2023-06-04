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
    public class ClientData : IEntityTypeConfiguration<Client>
    {
        public static Dictionary<string, Client> clients = new Dictionary<string, Client>();

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            Bootstrap();
            foreach (var client in clients)
            {
                builder.HasData(client.Value);
            }
        }

        public void Bootstrap()
        {
            try {
                clients.Add("Joseph",
                new Client
                {
                    UserID = Guid.NewGuid(),
                    Username = "Joseph",
                    Age = 22,
                    Avatar = "as",
                    Email = "josefino@gmail.com",
                    IsActive = true,
                    Password = "forTheMotherFoca@7",
                    ConfirmPassword = "forTheMotherFoca@7",
                    Incapacity = "",
                    UserLocationID = GPSCoordinatesData.defaultLocation.LocationID
                }) ;

                clients.Add("Carlos",
                 new Client
                 {
                     UserID = Guid.NewGuid(),
                     Username = "Carlos",
                     Age = 30,
                     Avatar = "gv",
                     Email = "carlitosbritos@gmail.com",
                     IsActive = true,
                     Password = "euGostoDeCamul123$",
                     ConfirmPassword = "euGostoDeCamul123$",
                     Incapacity = "wheelChair",
                     UserLocationID = GPSCoordinatesData.locations["Location 1"].LocationID
                 }) ;

                clients.Add("Diogo",
                 new Client
                 {
                     UserID = Guid.NewGuid(),
                     Username = "Diogo",
                     Age = 16,
                     Avatar = "bh",
                     Email = "diogomorfador@gmail.com",
                     IsActive = true,
                     Password = "queijo123ComBacon$",
                     ConfirmPassword = "queijo123ComBacon$",
                     Incapacity = "",
                     UserLocationID = GPSCoordinatesData.defaultLocation.LocationID
                 });

                clients.Add("Tiago",
                 new Client
                 {
                     UserID = Guid.NewGuid(),
                     Username = "Tiago",
                     Age = 22,
                     Avatar = "yh",
                     Email = "ceftigas@gmail.com",
                     IsActive = true,
                     Password = "sirvaPureComArroz$1",
                     ConfirmPassword = "sirvaPureComArroz$1",
                     Incapacity = "",
                     UserLocationID = GPSCoordinatesData.defaultLocation.LocationID
                 });

            } catch (Exception) { }
        }
    }
}
