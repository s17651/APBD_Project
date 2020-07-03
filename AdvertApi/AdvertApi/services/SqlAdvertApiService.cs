using AdvertApi.DTOs;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AdvertApi.services.tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.services
{
    public class SqlAdvertApiService : IDbAdvertService
    {

        private readonly AdvertDbContext _context;
        private readonly IConfiguration _configuration;

        public SqlAdvertApiService(AdvertDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Campaign AddCampaign(AddCampaignRequest request)
        {
            var client = _context.Clients
                .Where(c => c.IdClient == request.IdClient)
                .SingleOrDefault();

            if (client == null)
            {
                throw new ClientDoesntExistsException("Client with this Id doesn't exists.");
            }

            var fromBuilding = _context.Buildings
                .Where(b => b.IdBuilding == request.FromIdBuilding)
                .FirstOrDefault();
            var toBuilding = _context.Buildings
                .Where(b => b.IdBuilding == request.ToIdBuilding)
                .FirstOrDefault();

            if (fromBuilding.Street != toBuilding.Street)
            {
                throw new BuildingsStreetException("Buildings are not on the same street.");
            }

            var newCampaing = new Campaign
            {
                IdClient = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding
            };

            _context.Attach(newCampaing);
            _context.Add(newCampaing);
            _context.SaveChanges();

            return newCampaing;
        }

        public Client AddClient(AddClientRequest request)
        {
            //czy klient istnieje już w bazie danych 
            var client = _context.Clients
                .Where(c => 
                    c.FirstName == request.FirstName && 
                    c.LastName == request.LastName)
                    .SingleOrDefault();

            if (client != null)
            {
                throw new ClientAlreadyExistsException("This Client already exists.");
            }

            //czy login nie jest zajęty
            var login = _context.Clients
                .Where(c => c.Login == request.Login)
                .FirstOrDefault();

            if (login != null)
            {
                throw new LoginAlreadyExistsException("Login already taken.");
            }

            var salt = PasswordConfig.GenerateSalt();
            var hashedPassword = PasswordConfig.GenerateHashedPassword(request.Password, salt);

            var newClient = new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Login = request.Login,
                Password = hashedPassword,
                Salt = salt
            };

            _context.Attach(newClient);
            _context.Add(newClient);
            _context.SaveChangesAsync();

            return newClient;
        }

        public IEnumerable<Campaign> GetCampaigns()
        {
            var campaigns = _context.Campaings
                .Include(c => c.Client)
                .Include(c => c.Banners)
                .OrderByDescending(c => c.StartDate)
                .ToList();

            return campaigns;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var client = _context.Clients
                .Where(c => c.Login == request.Login)
                .FirstOrDefault();

            //czy istnieje login w bazie
            if (client == null)
            {
                throw new LoginOrPasswordException("Login or password is incorrect.");
            }

            //czy podane hasło jest właściwe
            var hashedPassword = PasswordConfig.GenerateHashedPassword(request.Password, client.Salt);
            if (hashedPassword != client.Password)
            {
                throw new LoginOrPasswordException("Login or password is incorrect.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString()),
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "s17651",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

            var refreshToken = Guid.NewGuid();

            client.RefreshToken = refreshToken.ToString();
            _context.SaveChanges();

            return new LoginResponse
            {
                AccesToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            };
        }

        public LoginResponse RefreshToken(string RequestRefreshToken)
        {
            var client = _context.Clients
                .Where(c => c.RefreshToken == RequestRefreshToken)
                .SingleOrDefault();

            if (client == null)
            {
                throw new RefreshTokenException("Refresh token is incorrect.");
            }

            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString()),
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "s17651",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

            var refreshToken = Guid.NewGuid();

            client.RefreshToken = refreshToken.ToString();
            _context.SaveChanges();

            return new LoginResponse
            {
                AccesToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            };
        }
    }
}
