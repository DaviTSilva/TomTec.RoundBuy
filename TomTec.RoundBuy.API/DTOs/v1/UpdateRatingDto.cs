using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class UpdateRatingDto
    {
        public string CommentText { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public double Rate { get; set; }

        public void UpdateModel(Rating rating)
        {
            rating.CommentText = this.CommentText;
            rating.Pros = this.Pros;
            rating.Cons = this.Cons;
            rating.Rate = this.Rate;
        }
    }
}
