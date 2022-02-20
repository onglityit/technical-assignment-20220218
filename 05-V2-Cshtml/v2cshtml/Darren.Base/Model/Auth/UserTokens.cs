using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darren.Base.Model.Auth
{
    public class UserTokens
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
