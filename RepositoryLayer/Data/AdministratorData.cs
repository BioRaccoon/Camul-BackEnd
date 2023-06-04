using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using System.Linq.Expressions;

namespace RepositoryLayer.Data
{
    public class AdministratorData : IEntityTypeConfiguration<Administrator>
    {
        Dictionary<string, Administrator> administrators = new Dictionary<string, Administrator>();

        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            Bootstrap();
            foreach (var administrator in administrators)
            {
                builder.HasData(administrator.Value);
            }
        }

        public void Bootstrap()
        {
            try
            {
                administrators.Add("Mario",
                    new Administrator
                    {
                        UserID = Guid.NewGuid(),
                        Username = "Mario",
                        Age = 18,
                        Avatar = "hg",
                        Email = "mariopasta@gmail.com",
                        IsActive = false,
                        Password = "itsTheEndOfThe$Pasta1",
                        ConfirmPassword = "itsTheEndOfThe$Pasta1"
                    });

                administrators.Add("admin",
                new Administrator
                {
                    UserID = Guid.NewGuid(),
                    Username = "admin",
                    Age = 21,
                    Avatar = "ku",
                    Email = "adminer@gmail.com",
                    IsActive = false,
                    Password = "thatsHowMafia7$Works",
                    ConfirmPassword = "thatsHowMafia7$Works"
                });
            } catch (Exception) { }
        }
    }
}
