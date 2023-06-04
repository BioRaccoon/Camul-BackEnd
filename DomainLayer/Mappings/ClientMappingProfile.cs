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
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();

            CreateMap<ClientCreationDto, Client>().ReverseMap();

            CreateMap<ClientUpdateDto, Client>().ReverseMap();
        }
    }
}