using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class UserLikesOnRating
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int RatingId { get; set; }

        public Rating Rating { get; set; }

        public UserLikesOnRating(int userId, int ratingId)
        {
            UserId = userId;
            RatingId = ratingId;
        }
    }
}
