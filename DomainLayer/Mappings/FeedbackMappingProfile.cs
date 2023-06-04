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
    public class FeedbackMappingProfile : Profile
    {
        public FeedbackMappingProfile()
        {
            CreateMap<Feedback, FeedbackDto>().ReverseMap();

            CreateMap<FeedbackCreationDto, Feedback>().ReverseMap();

            CreateMap<FeedbackUpdateDto, Feedback>().ReverseMap();
        }
    }
}