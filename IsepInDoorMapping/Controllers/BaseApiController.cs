using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ILoggerManager _logger;

        public BaseApiController(ILoggerManager logger)
        {
            _logger = logger;
        }
    }
}
