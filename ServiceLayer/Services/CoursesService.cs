using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Point = DomainLayer.Models.Point;

namespace ServiceLayer.Services
{

    public class CoursesService : ICoursesService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CoursesService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CourseDto> CreateCourse(CourseCreationDto course)
        {
            var coursedata = _mapper.Map<Course>(course);

            await _repository.Course.AddCourse(coursedata);

            await _repository.SaveAsync();

            var courseReturn = _mapper.Map<CourseDto>(coursedata);

            return courseReturn;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCourses()
        {
            var courses = await _repository.Course.GetAllCourses(trackChanges: false);
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return coursesDto;
        }

        public async Task<CourseDto> GetCourseById(Guid guid, bool trackChanges)
        {
            var course = await _repository.Course.GetCourseById(guid, trackChanges: false);
            var courseDto = _mapper.Map<CourseDto>(course);

            return courseDto;
        }

        public async Task<IEnumerable<Point>> GetShortestPathToAM(Guid userLocationId)
        {
            // Buscar todos os courses
            IEnumerable<Course> courses = await _repository.Course.GetAllCourses(false);

            List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>> CoursesPoints = await GetCoursesPoints(courses);

            //calcular as distâncias de cada Course usando o initial point e end point de cada course
            Dictionary<Course, double> coursesWithDistances = calculateCoursePointsDistances(CoursesPoints);

            //course mais próximo do utilizador a partir da sua localizacao
            var startPoint = await getCloserPointToUser(userLocationId, CoursesPoints);

            //course do auditório magno
            var endPoint = await getAuditorioMagnoPoint();

            //invocar o Dijsktra
            List<Point> shortestPath = await Dijkstra(courses.ToList(), startPoint, endPoint);

            return shortestPath;
        }

        private async Task<Point> getCloserPointToUser(Guid userLocationId, List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>> coursesPoints)
        {
            //buscar todas as localizacoes dos pontos e calcular a distancia e retornar
            //um course que contenha esse ponto

            List<GPSCoordinates> locations = new List<GPSCoordinates>();

            foreach(var coursePoint in coursesPoints)
            {
                if (!locations.Contains(coursePoint.Value.Key))
                {
                    locations.Add(coursePoint.Value.Key);
                }
                if (!locations.Contains(coursePoint.Value.Value))
                {
                    locations.Add(coursePoint.Value.Value);
                }
            }

            var userLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(userLocationId,false);

            var minDistance = double.MaxValue;
            var closerLocation = new GPSCoordinates();
            foreach(var location in locations)
            {
                var distance = calcDistBetwPoints(userLocation, location);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    closerLocation = location;
                }
            }

            Point pointOfCloserLocation = await _repository.Point.GetPointByCoordinatesId(
                closerLocation.LocationID,false);

            //var closerCourse = await _repository.Course.GetCoursesWithPointId(
            //    pointOfCloserLocation.PointID,false);

            return pointOfCloserLocation;
        }

        private async Task<Point> getAuditorioMagnoPoint()
        {
            var auditorioMagnoPoint = await _repository.Point.GetAuditorioMagnoPoint(false);
            Guid auditorioMagnoId = auditorioMagnoPoint.PointID;
            /*var courses = await _repository.Course.GetCoursesWithPointId(auditorioMagnoId,false);

            if (courses != null) {
                return courses.FirstOrDefault();
            }*/

            return auditorioMagnoPoint;
        }

        private (double, Course) GetQueueMin(List<(double, Course)> queue)
        {
            (double, Course) min = queue[0];

            foreach (var value in queue)
            {
                if(value.Item1 < min.Item1)
                {
                    min = value;
                }
            }
            return min;
        }

        private async Task<double> CalculateDistance(Point endPoint, Point initialPoint)
        {
            GPSCoordinates initialPointCoord = await _repository.GPSCoordinates.GetGPSCoordinatesById(
                initialPoint.PointLocationID, false);
            GPSCoordinates endPointCoord = await _repository.GPSCoordinates.GetGPSCoordinatesById(
                endPoint.PointLocationID, false);

            return calcDistBetwPoints(endPointCoord, initialPointCoord);
        }

        public async Task<List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>>> GetCoursesPoints(IEnumerable<Course> courses)
        {
            List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>> CoursesPoints = new List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>>();

            foreach (var course in courses)
            {
                var initialPoint = await _repository.Point.GetPointById(course.InitialPointID, false);
                var endPoint = await _repository.Point.GetPointById(course.EndPointID, false);

                var initialPointLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(initialPoint.PointLocationID, false);
                var endPointLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(endPoint.PointLocationID, false);

                CoursesPoints.Add(
                    new KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>(course,
                    new KeyValuePair<GPSCoordinates, GPSCoordinates>(initialPointLocation, endPointLocation)));

            }

            return CoursesPoints;
        }

        public List<Guid> PointsContainedInCourses(IEnumerable<Course> courses)
        {
            List<Guid> CoursesPointsIDs = new List<Guid>();

            foreach (var course in courses)
            {
                if (!CoursesPointsIDs.Contains(course.InitialPointID))
                {
                    CoursesPointsIDs.Add(course.InitialPointID);
                }
                if (!CoursesPointsIDs.Contains(course.EndPointID))
                {
                    CoursesPointsIDs.Add(course.EndPointID);
                }
            }

            return CoursesPointsIDs;
        }

        public async Task<Dictionary<Point, GPSCoordinates>> GetPointsLocations(List<Guid> CoursesPointsIDs)
        {
            Dictionary<Point, GPSCoordinates> PointsAndLocations = new Dictionary<Point, GPSCoordinates>();

            foreach (var pointID in CoursesPointsIDs)
            {
                var point = await _repository.Point.GetPointById(pointID, false);
                var pointLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(point.PointLocationID, false);
                PointsAndLocations.Add(point, pointLocation);
            }

            return PointsAndLocations;
        }

        public Dictionary<Course, double> calculateCoursePointsDistances(List<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>> CoursesPoints)
        {
            Dictionary<Course, double> CoursesPointsDistances = new Dictionary<Course, double>();

            foreach (var course in CoursesPoints)
            {
                var distance = calcDistBetwPoints(course.Value.Key, course.Value.Value);
                CoursesPointsDistances.Add(course.Key, distance);
            }

            return CoursesPointsDistances;
        }

        public static double calcDistBetwPoints(GPSCoordinates point1, GPSCoordinates point2)
        {

            Dictionary<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>, double> coursesWithDistances =
                new Dictionary<KeyValuePair<Course, KeyValuePair<GPSCoordinates, GPSCoordinates>>, double>();

            int r = 6371;  // radius of the Earth in kilometers
            double dLat = ToRadians(point2.Latitude - point1.Latitude);
            double dLon = ToRadians(point2.Longitude - point1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(ToRadians(point1.Latitude)) * Math.Cos(ToRadians(point2.Latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = r * c;

            return d;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public async Task RemoveCourse(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _repository.Course.RemoveCourse(course);
            await _repository.SaveAsync();
        }

        public async Task UpdateCourse(CourseCreationDto course, Course? courseData)
        {
            _mapper.Map(course, courseData);
            await _repository.SaveAsync();
        }

        public async Task<List<Point>> Dijkstra(List<Course> courses, Point start, Point end)
        {
            var points = new Dictionary<Guid, Point>();

            foreach (var course in courses)
            {
                if (!points.ContainsKey(course.InitialPointID))
                {
                    points[course.InitialPointID] = await _repository.Point.GetPointById(course.InitialPointID, false);
                }

                if (!points.ContainsKey(course.EndPointID))
                {
                    points[course.EndPointID] = await _repository.Point.GetPointById(course.EndPointID, false); ;
                }
            }

            var distances = new Dictionary<Guid, double>();
            foreach (var point in points.Values)
                distances[point.PointID] = double.PositiveInfinity;

            distances[start.PointID] = 0;

            var visited = new HashSet<Guid>();

            while (true)
            {
                var currentPointId = GetClosestPoint(distances, visited);
                visited.Add(currentPointId);

                if (currentPointId == end.PointID || double.IsPositiveInfinity(distances[currentPointId]))
                    break;

                var currentPoint = points[currentPointId];

                foreach (var course in courses)
                {
                    if (course.InitialPointID == currentPointId && course.IncapacityAcessible)
                    {
                        var neighbor = points[course.EndPointID];

                        var currentPointRepo = await _repository.Point.GetPointById(currentPoint.PointID, false);
                        var neighborRepo = await _repository.Point.GetPointById(neighbor.PointID, false);

                        var currentPointLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(currentPointRepo.PointLocationID, false);
                        var neighborLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(neighborRepo.PointLocationID, false);
                        
                        var distance = distances[currentPointId] + calcDistBetwPoints(currentPointLocation, neighborLocation);

                        if (distance < distances[neighbor.PointID])
                            distances[neighbor.PointID] = distance;
                    }
                    else if (course.EndPointID == currentPointId && course.IncapacityAcessible)
                    {
                        var neighbor = points[course.InitialPointID];

                        var currentPointRepo = await _repository.Point.GetPointById(currentPoint.PointID, false);
                        var neighborRepo = await _repository.Point.GetPointById(neighbor.PointID, false);

                        var currentPointLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(currentPointRepo.PointLocationID, false);
                        var neighborLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(neighborRepo.PointLocationID, false);

                        var distance = distances[currentPointId] + calcDistBetwPoints(currentPointLocation, neighborLocation);

                        if (distance < distances[neighbor.PointID])
                            distances[neighbor.PointID] = distance;
                    }
                }
            }

            return await ReconstructPath(end, points, distances);
        }

        private static Guid GetClosestPoint(Dictionary<Guid, double> distances, HashSet<Guid> visited)
        {
            return distances.Where(x => !visited.Contains(x.Key)).OrderBy(x => x.Value).FirstOrDefault().Key;
        }

        /*private static double CalculateDistance(GPSCoordinates location1, GPSCoordinates location2)
        {
            var latDiff = location2.Latitude - location1.Latitude;
            var lonDiff = location2.Longitude - location1.Longitude;
            return Math.Sqrt(latDiff * latDiff + lonDiff * lonDiff);
        }*/

        private async Task<List<Point>> ReconstructPath(Point end, Dictionary<Guid, Point> points, Dictionary<Guid, double> distances)
        {
            var path = new List<Point>();
            var current = end;

            while (current != null)
            {
                path.Insert(0, current);
                var currentId = current.PointID;
                var minDistance = double.PositiveInfinity;
                Point next = null;

                foreach (var course in points.Values)
                {
                    if (course.PointID == currentId)
                        continue;

                    var currentPointRepo = await _repository.Point.GetPointById(current.PointID, false);
                    var courseRepo = await _repository.Point.GetPointById(course.PointID, false);

                    var currentLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(currentPointRepo.PointLocationID, false);
                    var courseLocation = await _repository.GPSCoordinates.GetGPSCoordinatesById(courseRepo.PointLocationID, false);

                    var distance = calcDistBetwPoints(currentLocation, courseLocation);
                    if (distance < minDistance && distances[course.PointID] + distance == distances[currentId])
                    {
                        minDistance = distance;
                        next = course;
                    }
                }

                current = next;
            }

            return path;
        }
    }
}



