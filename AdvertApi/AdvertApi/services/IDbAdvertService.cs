using AdvertApi.DTOs;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using System.Collections.Generic;

namespace AdvertApi.services
{
    public interface IDbAdvertService
    {
        public Client AddClient(AddClientRequest request);

        public LoginResponse RefreshToken(string refreshToken);

        public LoginResponse Login(LoginRequest request);

        public IEnumerable<Campaign> GetCampaigns();

        public Campaign AddCampaign(AddCampaignRequest request);
    }
}
