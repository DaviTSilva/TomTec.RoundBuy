using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class UserType : BaseEntity
    {
        [Key]
        public override int Id { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Name { get; set; }
    }

    public enum UserTypeEnum
    {
        Prod = 1,
        Test = 2,
    }
}
