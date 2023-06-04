using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ILoginService
    {
        string GetToken(User user, IConfiguration _configuration,string clientOrAdminIdentifier);
    }
}
