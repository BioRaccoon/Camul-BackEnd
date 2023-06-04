using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClients(bool trackChanges);
        Task<IEnumerable<Client>> GetActiveClients(bool trackChanges);
        Task<Client> GetClientById(Guid userID, bool trackChanges);
        Task<Client> GetClientByEmail(string email, bool trackChanges);
        Task AddClient(Client client);
        Task RemoveClient(Client client);
        Task<Client> CheckClientCredentials(string email, string password, bool trackChanges);
    }
}
