using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public class LikesRepository : ILikesRepository
    {
        private string[] IncludesAnnouncement
        {
            get
            {
                return new string[]{
                    $"{nameof(UserLikesOnAnnouncement.User)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.Images)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.OrderProducts)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.Ratings)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.Ratings)}.{nameof(Rating.AuthorUser)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.Comments)}",
                    $"{nameof(UserLikesOnAnnouncement.Announcement)}.{nameof(Announcement.Comments)}.{nameof(Comment.AuthorUser)}",                   
                };
            }
        }

        private readonly RoundBuyDbContext _dbContext;
        public LikesRepository(RoundBuyDbContext context)
        {
            _dbContext = context;
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }

        #region Announcement
        public void UserLikeAnnouncements(int orderId, int announcementId)
        {
            var userLikesOnAnnouncement = new UserLikesOnAnnouncement(orderId, announcementId);
            _dbContext.UserLikesOnAnnouncements.Add(userLikesOnAnnouncement);
            _dbContext.SaveChanges();
        }

        public void UserUnlikeAnnouncements(int orderId, int announcementId)
        {
            var userLikesOnAnnouncement = new UserLikesOnAnnouncement(orderId, announcementId);
            _dbContext.UserLikesOnAnnouncements.Remove(userLikesOnAnnouncement);
            _dbContext.SaveChanges();
        }

        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikes()
        {
            var userLikesOnAnnouncement = (IEnumerable<UserLikesOnAnnouncement>)_dbContext.Set<UserLikesOnAnnouncement>().IncludeMultiple(IncludesAnnouncement);
            if (userLikesOnAnnouncement == null || userLikesOnAnnouncement.Count() <= 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnAnnouncement)}'");

            return userLikesOnAnnouncement;
        }

        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikesByUserId(int userId)
        {
            var entities = (IEnumerable<UserLikesOnAnnouncement>)_dbContext.Set<UserLikesOnAnnouncement>().Where(e => e.UserId == userId).IncludeMultiple(IncludesAnnouncement);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnAnnouncement)}'");
            return entities;
        }

        public IEnumerable<UserLikesOnAnnouncement> GetAnnouncementsLikesById(int announcementId)
        {
            var entities = (IEnumerable<UserLikesOnAnnouncement>)_dbContext.Set<UserLikesOnAnnouncement>().Where(e => e.AnnouncementId == announcementId);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnAnnouncement)}'");
            return entities;
        }
        #endregion

        #region Comment
        public void UserLikeComment(int orderId, int commentId)
        {
            var userLikesOnComment = new UserLikesOnComment(orderId, commentId);
            _dbContext.UserLikesOnComments.Add(userLikesOnComment);
            _dbContext.SaveChanges();
        }

        public void UserUnlikeComment(int orderId, int commentId)
        {
            var userLikesOnComment = new UserLikesOnComment(orderId, commentId);
            _dbContext.UserLikesOnComments.Remove(userLikesOnComment);
            _dbContext.SaveChanges();
        }

        public IEnumerable<UserLikesOnComment> GetCommentsLikes()
        {
            var entities = (IEnumerable<UserLikesOnComment>)_dbContext.Set<UserLikesOnComment>();
            if (entities == null || entities.Count() <= 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnComment)}'");

            return entities;
        }

        public IEnumerable<UserLikesOnComment> GetCommentsLikesByUserId(int userId)
        {
            var entities = (IEnumerable<UserLikesOnComment>)_dbContext.Set<UserLikesOnComment>().Where(e => e.UserId == userId);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnComment)}'");
            return entities;
        }

        public IEnumerable<UserLikesOnComment> GetCommentsLikesById(int commentId)
        {
            var entities = (IEnumerable<UserLikesOnComment>)_dbContext.Set<UserLikesOnComment>().Where(e => e.CommentId == commentId);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnComment)}'");
            return entities;
        }
        #endregion

        #region Rating
        public void UserLikeRating(int orderId, int ratingId)
        {
            var userLikesOnRating = new UserLikesOnRating(orderId, ratingId);
            _dbContext.UserLikesOnRatings.Add(userLikesOnRating);
            _dbContext.SaveChanges();
        }

        public void UserUnlikeRating(int orderId, int ratingId)
        {
            var userLikesOnRating = new UserLikesOnRating(orderId, ratingId);
            _dbContext.UserLikesOnRatings.Remove(userLikesOnRating);
            _dbContext.SaveChanges();
        }

        public IEnumerable<UserLikesOnRating> GetRatingsLikes()
        {
            var entities = (IEnumerable<UserLikesOnRating>)_dbContext.Set<UserLikesOnRating>();
            if (entities == null || entities.Count() <= 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnRating)}'");

            return entities;
        }

        public IEnumerable<UserLikesOnRating> GetRatingsLikesByUserId(int userId)
        {
            var entities = (IEnumerable<UserLikesOnRating>)_dbContext.Set<UserLikesOnRating>().Where(e => e.UserId == userId);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnRating)}'");
            return entities;
        }

        public IEnumerable<UserLikesOnRating> GetRatingsLikesById(int RatingId)
        {
            var entities = (IEnumerable<UserLikesOnRating>)_dbContext.Set<UserLikesOnRating>().Where(e => e.RatingId == RatingId);
            if (entities.Count() == 0)
                throw new KeyNotFoundException($"found no matches for a list of entity type '{typeof(UserLikesOnRating)}'");
            return entities;
        }
        #endregion
    }
}
