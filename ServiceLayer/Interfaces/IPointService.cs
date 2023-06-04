using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IPointService
    {
        Task<PointDto> AddPoint(PointCreationDto point);

        Task<IEnumerable<PointDto>> GetAllPoints(bool trackChanges);

        Task<PointDto> GetPointById(Guid pointID, bool trackChanges);

        Task RemovePoint(PointDto point);
        
        Task UpdatePoint(PointCreationDto point, Point? pointData);
    }
}
