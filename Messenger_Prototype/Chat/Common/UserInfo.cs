using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class UserInfo
    {
        public string Nickname { get; }
        public int ID { get; }

        public UserInfo(string nickname, int ID)
        {
            Nickname = nickname;
            this.ID = ID;
        }
    }
}
