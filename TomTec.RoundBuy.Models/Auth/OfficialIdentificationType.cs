using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class OfficialIdentificationType : BaseEntity
    {
        [Key]
        public override int Id {get;set;}

        [Column(TypeName = "varchar(150)")]
        [Required]
        public string Name { get; set; }
    }

    public enum OffcialIdentificationTypeEnum 
    {
        CPF = 1,
        CPNJ = 2,
    }
}
