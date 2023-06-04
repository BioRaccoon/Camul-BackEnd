using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class PointService : IPointService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public PointService(IRepositoryManager repository,IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PointDto>> GetAllPoints(bool trackChanges)
        {            
            var points = await _repository.Point.GetAllPoints(trackChanges: false);

            var pointsDto = _mapper.Map<IEnumerable<PointDto>>(points);
                
            return pointsDto;
        }

        public async Task<PointDto> GetPointById(Guid pointID, bool trackChanges)
        {
            var point = await _repository.Point.GetPointById(pointID, trackChanges: false);

            var pointDto = _mapper.Map<PointDto>(point);

            return pointDto;
        }

        public async Task<PointDto> AddPoint(PointCreationDto point)
        {
            var pointData = _mapper.Map<Point>(point);

            await _repository.Point.AddPoint(pointData);

            await _repository.SaveAsync();

            var pointReturn = _mapper.Map<PointDto>(pointData);

            return pointReturn;
        }

        public async Task UpdatePoint(PointCreationDto point, Point? pointData)
        {
            _mapper.Map(point, pointData);
            await _repository.SaveAsync();
        }

        public async Task RemovePoint(PointDto pointDto)
        {
            var point = _mapper.Map<Point>(pointDto);
            await _repository.Point.RemovePoint(point);
            await _repository.SaveAsync();
        }
    }
}
