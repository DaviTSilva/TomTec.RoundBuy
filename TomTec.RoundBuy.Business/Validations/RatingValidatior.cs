using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Lib.Utils;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    static public class RatingValidatior
    {
        static public void Validate(this Rating rating)
        {
            if (rating.Rate > 5 || rating.Rate < 0)
                throw new InvalidOperationException($"[INVALID \"{nameof(Rating)}\"! WRONG VALUE FOR FIELD \"{nameof(Rating.Rate)}\"]: Rate can not be lower than zero or higher than 5!");

            if (rating.CommentText.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Rating)}\"! WRONG VALUE FOR FIELD \"{nameof(Rating.CommentText)}\"]: Comments can not contain profane words!");

            if (rating.Pros.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Rating)}\"! WRONG VALUE FOR FIELD \"{nameof(Rating.Pros)}\"]: Comments can not contain profane words!");

            if (rating.Cons.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Rating)}\"! WRONG VALUE FOR FIELD \"{nameof(Rating.Cons)}\"]: Comments can not contain profane words!");
        }
    }
}
