using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

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

        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string ProfilePictureURL { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Email { get; set; }

        public UserType UserType { get; set; }

        public int UserTypeId { get; set; }

        public ICollection<UsersClaims> UsersClaims { get; set; }

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
