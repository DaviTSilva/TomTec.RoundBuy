using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class AddressDto
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public string AdditionalInformation { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string CountryName { get; set; }

        public Address ToModel()
        {
            return new Address()
            {
                Street = this.Street,
                Number = this.Number,
                AdditionalInformation = this.AdditionalInformation,
                PostalCode = this.PostalCode,
                City = this.City,
                State = this.State,
                CountryName = this.CountryName
            };
        }
    }
}
