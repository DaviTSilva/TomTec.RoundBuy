using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Comment : BaseEntity
    {
        public User AuthorUser { get; set; }
        public int AuthorUserId { get; set; }
        public Announcement Announcement { get; set; }
        public int AnnouncementId { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required]
        public string CommentText { get; set; }
    }
}
