using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : BaseApiController
    {

        private readonly IClientService _clientService;

        public ClientsController(ILoggerManager logger, IClientService clientService) : base(logger)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Get all Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clientsDto = await _clientService.GetAllClients(trackChanges: false);
                return Ok(clientsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllClients)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get all Clients that are logged in
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllLoggedClients")]
        public async Task<IActionResult> GetActiveClients()
        {
            try
            {
                var clientsDto = await _clientService.GetActiveClients(trackChanges: false);
                return Ok(clientsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetActiveClients)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get an Client by Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("{userID}", Name = "ClientById")]
        public async Task<IActionResult> GetClientById(string userID)
        {
            var clientDto = await _clientService.GetClientById(new Guid(userID), trackChanges: false);

            if (clientDto is null)
            {
                _logger.LogInfo($"Client with id: {userID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(clientDto);
            }
        }

        /// <summary>
        /// Get an Client by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email/{email}", Name = "ClientByEmail")]
        public async Task<IActionResult> GetClientByEmail(string email)
        {
            var clientDto = await _clientService.GetClientByEmail(email, trackChanges: false);

            if (clientDto is null)
            {
                _logger.LogInfo($"Client with email: {email} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(clientDto);
            }
        }

        /// <summary>
        /// Add a new Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreationDto client)
        {
            var clientReturn = await _clientService.AddClient(client);
            return CreatedAtRoute("ClientById",
                new
                {
                    userId = clientReturn.UserID
                },
                clientReturn);
        }

        /// <summary>
        /// Update an Client
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPut("{userID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateClientExists))]
        public async Task<IActionResult> UpdateClient(string userID, [FromBody] ClientCreationDto client)
        {
            var clientData = HttpContext.Items["client"] as Client;

            var clientDto = await _clientService.UpdateClient(client, clientData);

            return Ok(clientDto);
        }

        /// <summary>
        /// Remove an Client
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpDelete("{userID}")]
        public async Task<IActionResult> RemoveClient(string userID)
        {
            var client = await _clientService.GetClientById(new Guid(userID), trackChanges: false);

            if (client is null)
            {
                _logger.LogInfo($"Client with id: {userID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _clientService.RemoveClient(client);
                return Ok();
            }
        }
    }
}