using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Rating : BaseEntity
    {
        public User AuthorUser { get; set; }
        public int AuthorUserId { get; set; }
        public string CommentText { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public double Rate { get; set; }
    }
}
