using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class RatingDto
    {
        public int AnnouncementId { get; set; }
        public string CommentText { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public double Rate { get; set; }

        public Rating ToModel(int authorUserId)
        {
            return new Rating()
            {
                AnnouncementId = this.AnnouncementId,
                AuthorUserId = authorUserId,
                CommentText = this.CommentText,
                Pros = this.Pros,
                Cons = this.Cons,
                Rate = this.Rate,
            };
        }
    }
}
