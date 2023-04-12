using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class UpdateRatingDto
    {
        public int AnnouncementId { get; set; }
        public string CommentText { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public double Rate { get; set; }
    }
}
