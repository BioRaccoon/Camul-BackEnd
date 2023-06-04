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
    public class ClientService : IClientService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ClientService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Client ClientDtoToClient(ClientDto clientDto)
        {
            return _mapper.Map<Client>(clientDto);
        }

        public ClientDto ClientToClientDto(Client client)
        {
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CheckClientLogin(string email, string password)
        {
            var clientCredentials = await _repository.Client.CheckClientCredentials(email, password, trackChanges: false);

            if (clientCredentials != null)
            {

                clientCredentials.IsActive = true;

                ClientCreationDto clientCreationDto = _mapper.Map<ClientCreationDto>(clientCredentials);

                //await UpdateClient(clientCreationDto, clientCredentials);

                var credentialsDto = _mapper.Map<ClientDto>(clientCredentials);

                return credentialsDto;

            }

            return null;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClients(bool trackChanges)
        {
            var clients = await _repository.Client.GetAllClients(trackChanges: false);

            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return clientsDto;
        }

        public async Task<IEnumerable<ClientDto>> GetActiveClients(bool trackChanges)
        {
            var clients = await _repository.Client.GetActiveClients(trackChanges: false);

            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return clientsDto;
        }

        public async Task<ClientDto> GetClientById(Guid userID, bool trackChanges)
        {
            var client = await _repository.Client.GetClientById(userID, trackChanges: false);

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }

        public async Task<ClientDto> GetClientByEmail(string email, bool trackChanges)
        {
            var client = await _repository.Client.GetClientByEmail(email, trackChanges: false);

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }

        public async Task<ClientDto> AddClient(ClientCreationDto client)
        {
            var clientData = _mapper.Map<Client>(client);

            clientData.IsActive = false;

            await _repository.Client.AddClient(clientData);

            await _repository.SaveAsync();

            var clientReturn = _mapper.Map<ClientDto>(clientData);

            return clientReturn;
        }

        public async Task<ClientDto> UpdateClient(ClientCreationDto client, Client? clientData)
        {
            var clientTemp = _mapper.Map(client, clientData);
            await _repository.SaveAsync();

            var clientDto = _mapper.Map<ClientDto>(clientTemp);

            return clientDto;
        }

        public async Task RemoveClient(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            await _repository.Client.RemoveClient(client);
            await _repository.SaveAsync();
        }
    }
}
