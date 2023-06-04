using RepositoryLayer.Data;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        private IAdministratorRepository _administratorRepository;
        private IClientRepository _clientRepository;
        private ICourseRepository _courseRepository;
        private IGPSCoordinatesRepository _gpsCoordinatesRepository;
        private IPointRepository _pointRepository;
        private IFeedbackRepository _feedbackRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IAdministratorRepository Administrator
        {
            get
            {
                if (_administratorRepository is null)
                    _administratorRepository = new AdministratorRepository(_repositoryContext);
                return _administratorRepository;
            }
        }

        public IClientRepository Client
        {
            get
            {
                if (_clientRepository is null)
                    _clientRepository = new ClientRepository(_repositoryContext);
                return _clientRepository;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (_courseRepository is null)
                    _courseRepository = new CourseRepository(_repositoryContext);
                return _courseRepository;
            }
        }

        public IGPSCoordinatesRepository GPSCoordinates
        {
            get
            {
                if (_gpsCoordinatesRepository is null)
                    _gpsCoordinatesRepository = new GPSCoordinatesRepository(_repositoryContext);
                return _gpsCoordinatesRepository;
            }
        }

        public IPointRepository Point
        {
            get
            {
                if (_pointRepository is null)
                    _pointRepository = new PointRepository(_repositoryContext);
                return _pointRepository;
            }
        }

        public IFeedbackRepository Feedback
        {
            get
            {
                if (_feedbackRepository is null)
                    _feedbackRepository = new FeedbackRepository(_repositoryContext);
                return _feedbackRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
