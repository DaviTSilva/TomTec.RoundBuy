using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomTec.RoundBuy.API
{
    public static class Global
    {
        private static IConfiguration _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }
    }
}
