using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class GPSCoordinatesController : BaseApiController
    {
        private readonly IGpsCoordinatesService _gpsCoordinatesService;
        public GPSCoordinatesController(ILoggerManager logger,
            IGpsCoordinatesService _gpsCoordinatesService) : base(logger)
        {
            this._gpsCoordinatesService = _gpsCoordinatesService;
        }

        /// <summary>
        /// Get all Locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGPSCoordinates()
        {
            try
            {
                var gpsCoordinatesDto = await this._gpsCoordinatesService.GetAllGpsCoordinates(false);
                return Ok(gpsCoordinatesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllGPSCoordinates)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get an Location by Id
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        [HttpGet("{locationID}", Name = "GPSCoordinatesById")]
        public async Task<IActionResult> GetGPSCoordinatesById(string locationID)
        {
            var gpsCoordinatesDto = await _gpsCoordinatesService.GetGPSCoordinatesById(
                                            new Guid(locationID), trackChanges: false);

            if (gpsCoordinatesDto is null)
            {
                _logger.LogInfo($"GPSCoordinates with id: {locationID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(gpsCoordinatesDto);
            }
        }

        /// <summary>
        /// Add a new Location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateGPSCoordinates([FromBody] GPSCoordinatesCreationDto location)
        {

            var gpsCoordinatesReturn = await _gpsCoordinatesService.CreateGPSCoordinates(location);
            
            return CreatedAtRoute("GPSCoordinatesById",
                new
                {
                    locationId = gpsCoordinatesReturn.LocationID
                },
                gpsCoordinatesReturn);
        }

        /// <summary>
        /// Update an Location
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPut("{locationID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateGPSCoordinatesExists))]
        public async Task<IActionResult> UpdateGPSCoordinates(string locationID, [FromBody] GPSCoordinatesCreationDto location)
        {
            var gpsCoordinatesData = HttpContext.Items["gpsCoordinates"] as GPSCoordinates;

            await _gpsCoordinatesService.UpdateGPSCoordinates(location,gpsCoordinatesData);

            return NoContent();
        }

        /// <summary>
        /// Remove an Course
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        [HttpDelete("{locationID}")]
        public async Task<IActionResult> RemoveGPSCoordinates(string locationID)
        {
            var gpsCoordinates = await _gpsCoordinatesService.GetGPSCoordinatesById(new Guid(locationID),false);

            if (gpsCoordinates is null)
            {
                _logger.LogInfo($"GPSCoordinates with id: {locationID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _gpsCoordinatesService.RemoveGPSCoordinates(gpsCoordinates);
                return Ok();
            }
        }
    }
}