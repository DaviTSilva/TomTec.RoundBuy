using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Lib.Utils;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    static public class CommentValidator
    {
        static public void Validate(this Comment comment)
        {
            if (string.IsNullOrEmpty(comment.CommentText))
                throw new InvalidOperationException($"[INVALID \"{nameof(Comment)}\"! WRONG VALUE FOR FIELD \"{nameof(Comment.CommentText)}\"]: Comments can not be empty!");
            
            if (comment.CommentText.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Comment)}\"! WRONG VALUE FOR FIELD \"{nameof(Comment.CommentText)}\"]: Comments can not contain profane words!");
        }
    }
}
