using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }
        public string AltText { get; set; }
    }
}
