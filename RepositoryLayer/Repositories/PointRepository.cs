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
    public class PointRepository : RepositoryBase<Point>, IPointRepository
    {
        public PointRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Point>> GetAllPoints(bool trackChanges)
        {
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.PointName).ToListAsync();
        }

        public async Task<Point?> GetPointById(Guid pointID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.PointID.Equals(pointID), trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task AddPoint(Point point) => await CreateAsync(point);

        public async Task RemovePoint(Point point) => await RemoveAsync(point);

        public async Task<Point?> GetPointByCoordinatesId(Guid locationID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.PointLocationID.Equals(locationID),trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task<Point?> GetAuditorioMagnoPoint(bool trackChanges)
        {
            return await FindByConditionAsync(c => c.PointName.Equals("Auditório Magno"), trackChanges).Result.SingleOrDefaultAsync();
        }
    }
}