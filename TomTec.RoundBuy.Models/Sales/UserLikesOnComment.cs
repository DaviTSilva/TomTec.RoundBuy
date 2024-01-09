using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class UserLikesOnComment
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        public UserLikesOnComment(int userId, int commentId)
        {
            UserId = userId;
            CommentId = commentId;
        }
    }
}
