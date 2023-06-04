using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IPointRepository
    {
        Task<IEnumerable<Point>> GetAllPoints(bool trackChanges);
        Task<Point> GetPointById(Guid pointID, bool trackChanges);
        Task AddPoint(Point point);
        Task RemovePoint(Point point);
        Task<Point> GetPointByCoordinatesId(Guid locationID,bool trackChanges);
        Task<Point> GetAuditorioMagnoPoint(bool trackChanges);
    }
}
