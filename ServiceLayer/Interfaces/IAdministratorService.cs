using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAdministratorService
    {
        Task<AdministratorDto> AddAdministrator(AdministratorCreationDto administrator);

        Task<IEnumerable<AdministratorDto>> GetAllAdministrators(bool trackChanges);

        Task<AdministratorDto> GetAdministratorById(Guid userID, bool trackChanges);

        Task<AdministratorDto> GetAdministratorByEmail(string email, bool trackChanges);

        Task RemoveAdministrator(AdministratorDto administratorDto);

        Task<AdministratorDto> UpdateAdministrator(AdministratorCreationDto administrator, Administrator? administratorData);
        
        Task<AdministratorDto> CheckAdminLogin(string email, string password);

        AdministratorCreationDto AdministratorToAdministratorCreationDto(Administrator administrator);

        Administrator AdministratorDtoToAdministrator(AdministratorDto administratorDto);

        AdministratorDto AdministratorToAdministratorDto(Administrator administrator);
    }
}
