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
    public class AdministratorMappingProfile : Profile
    {
        public AdministratorMappingProfile()
        {
            CreateMap<Administrator, AdministratorDto>().ReverseMap();

            CreateMap<AdministratorCreationDto, Administrator>().ReverseMap();

            CreateMap<AdministratorUpdateDto, Administrator>().ReverseMap();
        }
    }
}
