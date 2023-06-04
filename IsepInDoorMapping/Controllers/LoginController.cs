using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController: BaseApiController
    {
        IConfiguration _configuration;
        private readonly IAdministratorService _administratorService;
        private readonly IClientService _clientService;
        private readonly ILoginService _loginService;

        public LoginController(IConfiguration configuration, ILoggerManager logger, IAdministratorService administratorService, IClientService clientService, ILoginService loginService) : base(logger)
        {
            _configuration = configuration;
            _administratorService = administratorService;
            _clientService = clientService;
            _loginService = loginService;
        }


        [HttpGet("{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email != null && password != null)
            {
                var resultAdminLoginCheck = await _administratorService.CheckAdminLogin(email, password);
                var resultClientLoginCheck = await _clientService.CheckClientLogin(email, password);

                if (resultAdminLoginCheck == null && resultClientLoginCheck == null)
                {
                    return BadRequest("Invalid Credentials");
                }
                else
                {
                    if (resultAdminLoginCheck != null)
                    {
                        return Ok(_loginService.GetToken(_administratorService.AdministratorDtoToAdministrator(resultAdminLoginCheck), _configuration,"A"));

                    }
                    else
                    {
                        return Ok(_loginService.GetToken(_clientService.ClientDtoToClient(resultClientLoginCheck), _configuration, "C"));
                    }
                }
            }
            return BadRequest("No data Posted");
        }
    }
}
