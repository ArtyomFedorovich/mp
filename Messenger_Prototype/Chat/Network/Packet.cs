using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat
{
    [Serializable]
    public class Packet
    {
        UdpClient a = new UdpClient();
        byte[] Content { get; }

        public byte[] ConvertPacket(string content)
        {
            return null;
        }
    }
}
