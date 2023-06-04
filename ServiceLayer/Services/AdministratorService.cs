using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AdministratorService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Administrator AdministratorDtoToAdministrator(AdministratorDto administratorDto)
        {
            return _mapper.Map<Administrator>(administratorDto);
        }

        public AdministratorDto AdministratorToAdministratorDto(Administrator administrator)
        {
            return _mapper.Map<AdministratorDto>(administrator);
        }

        public AdministratorCreationDto AdministratorToAdministratorCreationDto(Administrator administrator)
        {
            return _mapper.Map<AdministratorCreationDto>(administrator);
        }

        public async Task<AdministratorDto> CheckAdminLogin(string email, string password)
        {
            var adminCredentials = await _repository.Administrator.CheckAdminCredentials(email, password, trackChanges: false);

            if (adminCredentials != null)
            {
                adminCredentials.IsActive = true;

                AdministratorCreationDto administratorCreationDto = _mapper.Map<AdministratorCreationDto>(adminCredentials);

                //await UpdateAdministrator(administratorCreationDto, adminCredentials);

                var credentialsDto = _mapper.Map<AdministratorDto>(adminCredentials);

                return credentialsDto;

            }

            await _repository.SaveAsync();

            return null;
        }

        public async Task<IEnumerable<AdministratorDto>> GetAllAdministrators(bool trackChanges)
        {
            var administrators = await _repository.Administrator.GetAllAdministrators(trackChanges: false);

            var administratorsDto = _mapper.Map<IEnumerable<AdministratorDto>>(administrators);

            return administratorsDto;
        }

        public async Task<AdministratorDto> GetAdministratorById(Guid userID, bool trackChanges)
        {
            var administrator = await _repository.Administrator.GetAdministratorById(userID, trackChanges: false);

            var administratorDto = _mapper.Map<AdministratorDto>(administrator);

            return administratorDto;
        }

        public async Task<AdministratorDto> GetAdministratorByEmail(string email, bool trackChanges)
        {
            var administrator = await _repository.Administrator.GetAdministratorByEmail(email, trackChanges: false);

            var administratorDto = _mapper.Map<AdministratorDto>(administrator);

            return administratorDto;
        }

        public async Task<AdministratorDto> AddAdministrator(AdministratorCreationDto administrator)
        {
            var administratorData = _mapper.Map<Administrator>(administrator);

            administratorData.IsActive = false;

            await _repository.Administrator.AddAdministrator(administratorData);

            await _repository.SaveAsync();

            var administratorReturn = _mapper.Map<AdministratorDto>(administratorData);

            return administratorReturn;
        }

        public async Task<AdministratorDto> UpdateAdministrator(AdministratorCreationDto administrator, Administrator? administratorData)
        {
            var administratorTemp = _mapper.Map(administrator, administratorData);
            await _repository.SaveAsync();

            var administratorDto = _mapper.Map<AdministratorDto>(administratorTemp);

            return administratorDto;
        }

        public async Task RemoveAdministrator(AdministratorDto administratorDto)
        {
            var administrator = _mapper.Map<Administrator>(administratorDto);
            await _repository.Administrator.RemoveAdministrator(administrator);
            await _repository.SaveAsync();
        }
    }
}
