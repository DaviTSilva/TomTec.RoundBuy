using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class TechnicalInfo : BaseEntity
    {
        public string Title { get; set; }
        public string InfoSeparetedBySemicolon { get; set; }
    }
}
