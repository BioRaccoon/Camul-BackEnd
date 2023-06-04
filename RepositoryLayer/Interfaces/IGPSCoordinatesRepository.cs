using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IGPSCoordinatesRepository
    {
        Task<IEnumerable<GPSCoordinates>> GetAllGPSCoordinates(bool trackChanges);
        Task<GPSCoordinates> GetGPSCoordinatesById(Guid locationID, bool trackChanges);
        Task AddGPSCoordinates(GPSCoordinates gpsCoordinates);
        Task RemoveGPSCoordinates(GPSCoordinates gpsCoordinates);
    }
}
