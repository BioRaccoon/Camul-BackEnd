using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/points")]
    [ApiController]
    public class PointsController : BaseApiController
    {
        private readonly IPointService _pointService;
        public PointsController(ILoggerManager logger, IPointService pointService) : base(logger)
        {
            this._pointService = pointService;
        }

        /// <summary>
        /// Get all Points
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPoints()
        {
            try
            {
                var pointList = await this._pointService.GetAllPoints(false);
                return Ok(pointList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllPoints)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally { }
        }

        /// <summary>
        /// Get an Point by Id
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpGet("{pointID}", Name = "PointById")]
        public async Task<IActionResult> GetPointById(string pointID)
        {
            var pointDto = await this._pointService.GetPointById(new Guid(pointID),false);

            if (pointDto is null)
            {
                _logger.LogInfo($"Point with id: {pointID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(pointDto);
            }
        }

        /// <summary>
        /// Add a new Point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePoint([FromBody] PointCreationDto point)
        {

            var pointReturn = await this._pointService.AddPoint(point);

            return CreatedAtRoute("PointById",
                new
                {
                    pointId = pointReturn.PointID
                },
                pointReturn);
        }

        /// <summary>
        /// Update an Point
        /// </summary>
        /// <param name="pointID"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPut("{pointID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidatePointExists))]
        public async Task<IActionResult> UpdatePoint(string pointID, [FromBody] PointCreationDto point)
        {
            var pointData = HttpContext.Items["point"] as Point;

            await this._pointService.UpdatePoint(point,pointData);

            return NoContent();
        }

        /// <summary>
        /// Remove an Point
        /// </summary>
        /// <param name="pointID"></param>
        /// <returns></returns>
        [HttpDelete("{pointID}")]
        public async Task<IActionResult> RemovePoint(string pointID)
        {
            var point = await _pointService.GetPointById(new Guid(pointID), trackChanges: false);

            if (point is null)
            {
                _logger.LogInfo($"Point with id: {pointID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _pointService.RemovePoint(point);
                return Ok();
            }
        }
    }
}