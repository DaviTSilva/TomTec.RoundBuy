using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TomTec.RoundBuy.Models
{
    public class UserLikesOnAnnouncement
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }

        public UserLikesOnAnnouncement(int userId, int announcementId)
        {
            UserId = userId;
            AnnouncementId = announcementId;
        }
    }
}
