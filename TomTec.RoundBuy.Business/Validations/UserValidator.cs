using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Lib.Utils;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business.Validations
{
    static public class UserValidator
    {
        static public void Validate(this User user)
        {
            if (!user.Email.IsEmail())
                throw new InvalidOperationException($"[INVALID \"{nameof(User)}\"! WRONG VALUE FOR FIELD \"{nameof(User.Email)}\"]: given email is not in valid format!");

            if (!IsOfficialIdentification(user.OfficialIdentification, user.OfficialIdentificationTypeId))
                throw new InvalidOperationException($"[INVALID \"{nameof(User)}\"! WRONG VALUE FOR FIELD \"{nameof(User.OfficialIdentification)}\"]: given {(OfficialIdentificationTypeEnum)user.OfficialIdentificationTypeId} is not in valid format!");  

            if (string.IsNullOrEmpty(user.UserName))
                throw new InvalidOperationException($"[INVALID \"{nameof(User)}\"! WRONG VALUE FOR FIELD \"{nameof(User.UserName)}\"]: Username can not contain profane words!");
        }

        private static bool IsOfficialIdentification(string identification, int identificationTypeId)
        {
            if (identificationTypeId == (short)OfficialIdentificationTypeEnum.CPF)
                return identification.IsCPF();

            if (identificationTypeId == (short)OfficialIdentificationTypeEnum.CPNJ)
                return identification.IsCnpj();

            return false;
        }
    }
}
