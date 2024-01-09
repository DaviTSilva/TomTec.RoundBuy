using System.Collections.Generic;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public interface ILikesRepository
    {
        public void UserLikeAnnouncements(int orderId, int announcementId);
        public void UserUnlikeAnnouncements(int orderId, int announcementId);
        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikes();
        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikesByUserId(int userId);
        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikesById(int announcementId);
        public void UserLikeComment(int orderId, int commentId);
        public void UserUnlikeComment(int orderId, int commentId);
        public IEnumerable<UserLikesOnComment> GetCommentsLikes();
        public IEnumerable<UserLikesOnComment> GetCommentsLikesByUserId(int userId);
        public IEnumerable<UserLikesOnComment> GetCommentsLikesById(int commentId);
        public void UserLikeRating(int orderId, int ratingId);
        public void UserUnlikeRating(int orderId, int ratingId);
        public IEnumerable<UserLikesOnRating> GetRatingsLikes();
        public IEnumerable<UserLikesOnRating> GetRatingsLikesByUserId(int userId);
        public IEnumerable<UserLikesOnRating> GetRatingsLikesById(int RatingId);
    }
}