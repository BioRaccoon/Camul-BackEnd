using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<Administrator>> GetAllAdministrators(bool trackChanges);
        Task<Administrator> GetAdministratorById(Guid userID, bool trackChanges);
        Task<Administrator> GetAdministratorByEmail(string email, bool trackChanges);
        Task AddAdministrator(Administrator administrator);
        Task RemoveAdministrator(Administrator administrator);
        Task<Administrator> CheckAdminCredentials(string email, string password, bool trackChanges);
    }
}
