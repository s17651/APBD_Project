using AdvertApi.DTOs;
using AdvertApi.Models;
using System.Collections.Generic;

namespace AdvertApi.services
{
    interface IDbAdvertService
    {
        public Client AddClient(AddClientRequest request);

        public string RefreshToken(string refreshToken);

        public string Login(LoginRequest request);

        public IEnumerable<Campaign> GetCampaigns();

        public Campaign AddCampaign(AddCampaignRequest request);
    }
}
