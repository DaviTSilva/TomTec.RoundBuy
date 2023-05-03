using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business.Validations
{
    static public class UserValidator
    {
        static public void Validate(this User user)
        {

            //Profanity Detector
            if (string.IsNullOrEmpty(user.UserName))
                throw new InvalidOperationException($"[INVALID \"{nameof(User).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(User.UserName).ToUpper()}\"]: Username can not contain profane words!");
        }


    }
}
