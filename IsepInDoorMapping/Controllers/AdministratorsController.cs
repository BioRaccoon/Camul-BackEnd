using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/administrators")]
    [ApiController]
    public class AdministratorsController : BaseApiController
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorsController(ILoggerManager logger, IAdministratorService administratorService) : base(logger)
        {
            _administratorService = administratorService;
        }

        /// <summary>
        /// Get all Administrators
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAdministrators()
        {
            try
            {
                var administratorsDto = await _administratorService.GetAllAdministrators(trackChanges: false);
                return Ok(administratorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllAdministrators)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get an Administrator by Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("{userID}", Name = "AdministratorById")]
        public async Task<IActionResult> GetAdministratorById(string userID)
        {
            var administratorDto = await _administratorService.GetAdministratorById(new Guid(userID), trackChanges: false);
            if (administratorDto is null)
            {
                _logger.LogInfo($"Administrator with id: {userID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(administratorDto);
            }
        }

        /// <summary>
        /// Get an Administrator by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}", Name = "AdministratorByEmail")]
        public async Task<IActionResult> GetAdministratorByEmail(string email)
        {
            var administratorDto = await _administratorService.GetAdministratorByEmail(email, trackChanges: false);
            if (administratorDto is null)
            {
                _logger.LogInfo($"Administrator with email: {email} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(administratorDto);
            }
        }

        /// <summary>
        /// Add a new Administrator
        /// </summary>
        /// <param name="administrator"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAdministrator([FromBody] AdministratorCreationDto administrator)
        {
            var administratorReturn = await _administratorService.AddAdministrator(administrator);

            return CreatedAtRoute("AdministratorById",
                new
                {
                    userId = administratorReturn.UserID
                },
                administratorReturn);
        }

        /// <summary>
        /// Update an Administrator
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="administrator"></param>
        /// <returns></returns>
        [HttpPut("{userID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAdministratorExists))]
        public async Task<IActionResult> UpdateAdministrator(string userID, [FromBody] AdministratorCreationDto administrator)
        {
            var administratorData = HttpContext.Items["administrator"] as Administrator;

            var administratorDto = await _administratorService.UpdateAdministrator(administrator, administratorData);

            return Ok(administratorDto);
        }

        /// <summary>
        /// Remove an Administrator
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpDelete("{userID}")]
        public async Task<IActionResult> RemoveAdministrator(string userID)
        {
            var administrator = await _administratorService.GetAdministratorById(new Guid(userID), trackChanges: false);

            if (administrator is null)
            {
                _logger.LogInfo($"Administrator with id: {userID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _administratorService.RemoveAdministrator(administrator);
                return Ok();
            }
        }
    }
}
