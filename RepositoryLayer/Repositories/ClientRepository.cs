using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.GenericRepository.Service;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Client>> GetAllClients(bool trackChanges)
        {
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.Username).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetActiveClients(bool trackChanges)
        {
            return await FindByConditionAsync(c => c.IsActive.Equals(true), trackChanges).Result.OrderBy(c => c.Username).ToListAsync();
        }

        public async Task<Client?> GetClientById(Guid userID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.UserID.Equals(userID), trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task<Client?> GetClientByEmail(string email, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.Email.Equals(email), trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task AddClient(Client client) => await CreateAsync(client);

        public async Task RemoveClient(Client client) => await RemoveAsync(client);

        public async Task<Client?> CheckClientCredentials(string email, string password, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.Email.Equals(email) && c.Password.Equals(password), trackChanges).Result.SingleOrDefaultAsync();
        }

    }
}