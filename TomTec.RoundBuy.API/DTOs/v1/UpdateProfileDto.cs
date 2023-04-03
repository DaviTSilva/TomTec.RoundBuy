using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class UpdateProfileDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public IEnumerable<int> ClaimsIds { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePictureURL { get; set; }
        public int AddressId { get; set; }
        public int OfficialIdentificationTypeId { get; set; }
        public string OfficialIdentification { get; set; }


        public User ToModel()
        {
            var user = new User()
            {
                UserName = this.UserName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                UserTypeId = this.UserTypeId,
                BirthDate = this.BirthDate,
                ProfileImage = new Image() 
                { 
                    Url = this.ProfilePictureURL,
                    AltText = string.Concat(this.UserName, "`s profile picture")
                },
                AddressId = this.AddressId,
                OfficialIdentificationTypeId = this.OfficialIdentificationTypeId,
                OfficialIdentification = this.OfficialIdentification,
            };
            user.UsersClaims = this.ClaimsIds.Select(id => new UsersClaims() { ClaimId = id, User = user }).ToList();

            return user;
        }
    }
}
