using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.GenericRepository.Service;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class GPSCoordinatesRepository : RepositoryBase<GPSCoordinates>, IGPSCoordinatesRepository
    {
        public GPSCoordinatesRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<GPSCoordinates>> GetAllGPSCoordinates(bool trackChanges)
        {
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.LocationID.ToString()).ToListAsync();
        }

        public async Task<GPSCoordinates?> GetGPSCoordinatesById(Guid locationID, bool trackChanges)
            => await FindByConditionAsync(c => c.LocationID.Equals(locationID), trackChanges).Result.SingleOrDefaultAsync();

        public async Task AddGPSCoordinates(GPSCoordinates gpsCoordinates) => await CreateAsync(gpsCoordinates);

        public async Task RemoveGPSCoordinates(GPSCoordinates gpsCoordinates) => await RemoveAsync(gpsCoordinates);

    }
}