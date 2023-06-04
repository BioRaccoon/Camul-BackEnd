using AutoMapper;
using DomainLayer.Mappings;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryLayer.Data;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace IsepInDoorMapping.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(
                opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
        b => b.MigrationsAssembly("RepositoryLayer")));

        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<AdministratorMappingProfile>();
                map.AddProfile<ClientMappingProfile>();
                map.AddProfile<CourseMappingProfile>();
                map.AddProfile<GPSCoordinatesMappingProfile>();
                map.AddProfile<PointMappingProfile>();
                map.AddProfile<FeedbackMappingProfile>();
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }

        //Add Here
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 30
                });
            });
        }

        //Add Here
        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();


        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateAdministratorExists>();
            services.AddScoped<ValidateClientExists>();
            services.AddScoped<ValidateCourseExists>();
            services.AddScoped<ValidateGPSCoordinatesExists>();
            services.AddScoped<ValidatePointExists>();
            services.AddScoped<ValidateFeedbackExists>();

            //application services on the service layer
            services.AddScoped<IPointService, PointService>();
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICoursesService,CoursesService>();
            services.AddScoped<IGpsCoordinatesService,GpsCoordinatesService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ILoginService, LoginService>();

        }
    }
}
