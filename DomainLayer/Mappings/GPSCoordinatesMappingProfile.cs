using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Mappings
{
    public class GPSCoordinatesMappingProfile : Profile
    {
        public GPSCoordinatesMappingProfile()
        {
            CreateMap<GPSCoordinates, GPSCoordinatesDto>().ReverseMap();

            CreateMap<GPSCoordinatesCreationDto, GPSCoordinates>().ReverseMap();

            CreateMap<GPSCoordinatesUpdateDto, GPSCoordinates>().ReverseMap();
        }
    }
}