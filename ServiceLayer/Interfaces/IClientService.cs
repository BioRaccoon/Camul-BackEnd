using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> AddClient(ClientCreationDto client);

        Task<IEnumerable<ClientDto>> GetAllClients(bool trackChanges);

        Task<IEnumerable<ClientDto>> GetActiveClients(bool trackChanges);

        Task<ClientDto> GetClientById(Guid userID, bool trackChanges);

        Task<ClientDto> GetClientByEmail(string email, bool trackChanges);

        Task RemoveClient(ClientDto clientDto);

        Task<ClientDto> UpdateClient(ClientCreationDto client, Client? clientData);

        Task<ClientDto> CheckClientLogin(string email, string password);

        Client ClientDtoToClient(ClientDto clientDto);

        ClientDto ClientToClientDto(Client client);
    }
}