using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class CommentDto
    {
        public string CommentText { get; set; }
        public int AnnouncementId { get; set; }

        public Comment ToModel(int authorUserId)
        {
            return new Comment()
            {
                AuthorUserId = authorUserId,
                CommentText = this.CommentText,
                AnnouncementId = this.AnnouncementId,
            };
        }
    }
}
