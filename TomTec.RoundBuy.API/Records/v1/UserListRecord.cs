using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Records.v1
{
    public class UserListRecord
    {
        public UserListRecord(IEnumerable<User> users)
        {
            Users = users;
        }

        public IEnumerable<User> Users { get; set; }
    }
}
