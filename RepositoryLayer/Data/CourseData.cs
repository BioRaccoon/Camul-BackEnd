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
    public class CourseData : IEntityTypeConfiguration<Course>
    {
        Dictionary<string, Course> courses = new Dictionary<string, Course>();

        public void Configure(EntityTypeBuilder<Course> builder)
        {
            Bootstrap();
            foreach (var course in courses)
            {
                builder.HasData(course.Value);
            }
        }

        public void Bootstrap()
        {
            try
            {
                courses.Add("Path from A to I",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from A to I",
                    InitialPointID = PointData.points.ToList()[0].Value.PointID,
                    EndPointID = PointData.points.ToList()[8].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from I to A",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from I to A",
                    InitialPointID = PointData.points.ToList()[8].Value.PointID,
                    EndPointID = PointData.points.ToList()[0].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from I to B",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from I to B",
                    InitialPointID = PointData.points.ToList()[8].Value.PointID,
                    EndPointID = PointData.points.ToList()[1].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from B to I",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from B to I",
                    InitialPointID = PointData.points.ToList()[1].Value.PointID,
                    EndPointID = PointData.points.ToList()[8].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from B to G",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from B to G",
                    InitialPointID = PointData.points.ToList()[1].Value.PointID,
                    EndPointID = PointData.points.ToList()[6].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from G to B",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from G to B",
                    InitialPointID = PointData.points.ToList()[6].Value.PointID,
                    EndPointID = PointData.points.ToList()[1].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from G to H",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from G to H",
                    InitialPointID = PointData.points.ToList()[6].Value.PointID,
                    EndPointID = PointData.points.ToList()[7].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from H to G",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from H to G",
                    InitialPointID = PointData.points.ToList()[7].Value.PointID,
                    EndPointID = PointData.points.ToList()[6].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from H to A",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from H to A",
                    InitialPointID = PointData.points.ToList()[7].Value.PointID,
                    EndPointID = PointData.points.ToList()[0].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from A to H",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from A to H",
                    InitialPointID = PointData.points.ToList()[0].Value.PointID,
                    EndPointID = PointData.points.ToList()[7].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from A to AuditorioMagno",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from A to AuditorioMagno",
                    InitialPointID = PointData.points.ToList()[0].Value.PointID,
                    EndPointID = PointData.points.ToList()[15].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from AuditorioMagno to A ",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from AuditorioMagno to A ",
                    InitialPointID = PointData.points.ToList()[15].Value.PointID,
                    EndPointID = PointData.points.ToList()[0].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from H to AuditorioMagno",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from H to AuditorioMagno",
                    InitialPointID = PointData.points.ToList()[7].Value.PointID,
                    EndPointID = PointData.points.ToList()[15].Value.PointID,
                    IncapacityAcessible = true
                }
                );

                courses.Add("Path from AuditorioMagno to H",
                new Course
                {
                    CourseID = Guid.NewGuid(),
                    Description = "Path from AuditorioMagno to H",
                    InitialPointID = PointData.points.ToList()[15].Value.PointID,
                    EndPointID = PointData.points.ToList()[7].Value.PointID,
                    IncapacityAcessible = true
                }
                );
            } catch (Exception) { }
        }
    }
}
