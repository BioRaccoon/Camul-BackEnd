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
    public class AdministratorRepository : RepositoryBase<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Administrator>> GetAllAdministrators(bool trackChanges)
        { 
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.Username).ToListAsync();
        }

        public async Task<Administrator?> GetAdministratorById(Guid userID, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.UserID.Equals(userID), trackChanges).Result.SingleOrDefaultAsync();
        }

        public async Task<Administrator?> GetAdministratorByEmail(string email, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.Email.Equals(email), trackChanges).Result.SingleOrDefaultAsync();
        }
        public async Task AddAdministrator(Administrator administrator) => await CreateAsync(administrator);

        public async Task RemoveAdministrator(Administrator administrator) => await RemoveAsync(administrator);

        public async Task<Administrator?> CheckAdminCredentials(string email, string password, bool trackChanges)
        {
            return await FindByConditionAsync(c => c.Email.Equals(email) && c.Password.Equals(password), trackChanges).Result.SingleOrDefaultAsync();
        }
    }
}
