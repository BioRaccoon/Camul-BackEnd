using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------Bootstrap---------------//

            modelBuilder.ApplyConfiguration(new GPSCoordinatesData());
            modelBuilder.ApplyConfiguration(new AdministratorData());
            modelBuilder.ApplyConfiguration(new ClientData());
            modelBuilder.ApplyConfiguration(new PointData());
            modelBuilder.ApplyConfiguration(new CourseData());
            modelBuilder.ApplyConfiguration(new FeedbackData());

            //---------------RelationShips---------------//

            modelBuilder.Entity<Feedback>()
                .HasOne(e => e.FeedbackLocation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(e => e.InitialPoint)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(e => e.EndPoint)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<GPSCoordinates> GPSCoordinates { get; set; }
        public DbSet<DomainLayer.Models.Point> Points { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
