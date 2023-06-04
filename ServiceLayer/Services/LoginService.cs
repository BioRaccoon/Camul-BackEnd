using AutoMapper;
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer.Services
{
    public class LoginService: ILoginService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public LoginService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public string GetToken(User user, IConfiguration _configuration,string clientOrAdminIdentifier)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserID", user.UserID.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: singIn
                );

            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Token+"-"+clientOrAdminIdentifier;
        }
    }
}
