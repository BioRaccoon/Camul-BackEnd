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
    public class PointMappingProfile : Profile
    {
        public PointMappingProfile()
        {
            CreateMap<Point, PointDto>().ReverseMap();

            CreateMap<PointCreationDto, Point>().ReverseMap();

            CreateMap<PointUpdateDto, Point>().ReverseMap();
        }
    }
}