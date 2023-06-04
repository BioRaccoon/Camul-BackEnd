using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IGpsCoordinatesService
    {
        Task<GPSCoordinatesDto> CreateGPSCoordinates(GPSCoordinatesCreationDto location);
        Task<IEnumerable<GPSCoordinatesDto>> GetAllGpsCoordinates(bool trackChanges);
        Task<GPSCoordinatesDto> GetGPSCoordinatesById(Guid guid, bool trackChanges);
        Task RemoveGPSCoordinates(GPSCoordinatesDto gpsCoordinates);
        Task UpdateGPSCoordinates(GPSCoordinatesCreationDto location, GPSCoordinates? gpsCoordinatesData);
    }
}
