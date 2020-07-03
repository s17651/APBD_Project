using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Campaign
    {
        public int IdCampaign { get; set; }

        public int IdClient { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double PricePerSquareMeter { get; set; }

        public int FromIdBuilding { get; set; }

        public int ToIdBuilding { get; set; }

        public Building FromBuilding { get; set; }

        public Building ToBuilding { get; set; }

        public Client Client { get; set; }

        public ICollection<Banner> Banners { get; set; }
    }
}