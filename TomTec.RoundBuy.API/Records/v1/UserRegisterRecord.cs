using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Records.v1
{
    public class UserRegisterRecord
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public IEnumerable<int> UserRolesIds { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string AddtionalInformation { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string CountryName { get; set; }

        public UserRegisterRecord(User user)
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.UserTypeId = user.UserTypeId;
            this.UserRolesIds = user.UsersClaims.Select(ur => ur.ClaimId);
            this.Street = user.Address.Street;
            this.Number = user.Address.Number;
            this.PostalCode = user.Address.PostalCode;
            this.AddtionalInformation = user.Address.AdditionalInformation;
            this.City = user.Address.City;
            this.StateOrProvince = user.Address.StateOrProvince;
            this.CountryName = user.Address.CountryName;
        }
    }
}
