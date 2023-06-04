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
    public class GpsCoordinatesService : IGpsCoordinatesService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GpsCoordinatesService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GPSCoordinatesDto> CreateGPSCoordinates(GPSCoordinatesCreationDto location)
        {
            var gpsCoordinatesData = _mapper.Map<GPSCoordinates>(location);

            await _repository.GPSCoordinates.AddGPSCoordinates(gpsCoordinatesData);
            await _repository.SaveAsync();
            
            var gpsCoordinatesReturn = _mapper.Map<GPSCoordinatesDto>(gpsCoordinatesData);

            return gpsCoordinatesReturn;
        }

        public async Task<IEnumerable<GPSCoordinatesDto>> GetAllGpsCoordinates(bool trackChanges)
        {
            var gpsCoordinates = await _repository.GPSCoordinates.GetAllGPSCoordinates(trackChanges: false);

            var gpsCoordinatesDto = _mapper.Map<IEnumerable<GPSCoordinatesDto>>(gpsCoordinates);

            return gpsCoordinatesDto;
        }

        public async Task<GPSCoordinatesDto> GetGPSCoordinatesById(Guid guid, bool trackChanges)
        {
            var gpsCoordinates = await _repository.GPSCoordinates.GetGPSCoordinatesById(guid, trackChanges: false);

            var gpsCoordinatesDto = _mapper.Map<GPSCoordinatesDto>(gpsCoordinates);

            return gpsCoordinatesDto;
        }

        public async Task RemoveGPSCoordinates(GPSCoordinatesDto gpsCoordinatesDto)
        {
            var gpsCoordinates = _mapper.Map<GPSCoordinates>(gpsCoordinatesDto);

            await _repository.GPSCoordinates.RemoveGPSCoordinates(gpsCoordinates);
            await _repository.SaveAsync();
        }

        public async Task UpdateGPSCoordinates(GPSCoordinatesCreationDto location, GPSCoordinates? gpsCoordinatesData)
        {
            _mapper.Map(location, gpsCoordinatesData);
            await _repository.SaveAsync();
        }
    }
}
