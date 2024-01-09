using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace TomTec.RoundBuy.Models
{
    public class User : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(120)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(120)")]
        [Required]
        public string OfficialIdentification { get; set; }

        public OfficialIdentificationType OfficialIdentificationType { get; set; }

        public int OfficialIdentificationTypeId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }

        public Image Image { get; set; }
        public int ImageId { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Email { get; set; }

        public UserType UserType { get; set; }

        public int UserTypeId { get; set; }

        public ICollection<UsersClaims> UsersClaims { get; set; }
        public ICollection<UserLikesOnAnnouncement> UserLikesOnAnnouncements { get; set; }
        public ICollection<UserLikesOnRating> UserLikesOnRatings { get; set; }
        public ICollection<UserLikesOnComment> UserLikesOnComments { get; set; }

        [Column(TypeName = "varchar(max)")]
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        [Required]
        public bool Active { get; set; } = true;
    }
}
