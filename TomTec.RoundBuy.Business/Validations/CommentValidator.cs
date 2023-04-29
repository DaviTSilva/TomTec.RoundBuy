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
            if (comment.CommentText.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Comment).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Comment.CommentText).ToUpper()}\"]: Comments can not contain profane words!");
        }
    }
}
