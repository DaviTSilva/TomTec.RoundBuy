using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Comment : BaseEntity
    {
        public User AuthorUser { get; set; }
        public int AuthorUserId { get; set; }
        public string CommentText { get; set; }
    }
}
