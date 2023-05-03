using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Business
{
    public interface IRatingService
    {
        public void CheckIfUserCanRate(int userId, int announcementId);
    }
}
