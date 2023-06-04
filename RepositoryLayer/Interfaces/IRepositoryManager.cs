using DomainLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IRepositoryManager
    {
        IAdministratorRepository Administrator { get; }
        IClientRepository Client { get; }
        ICourseRepository Course { get; }
        IGPSCoordinatesRepository GPSCoordinates { get; }
        IPointRepository Point { get; }
        IFeedbackRepository Feedback { get; }
        Task SaveAsync();
    }
}
