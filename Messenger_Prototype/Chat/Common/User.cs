using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class User
    {
        public UserInfo UserInfo { get; }
        public IPEndPoint EndPoint { get; }

        public User(UserInfo userInfo, IPEndPoint endPoint)
        {
            UserInfo = userInfo;
            EndPoint = endPoint;
        }
    }
}
