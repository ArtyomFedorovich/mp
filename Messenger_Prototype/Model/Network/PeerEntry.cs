using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype
{
    public class PeerEntry
    {
        public PeerName PeerName { get; set; }
        public IP2PService ServiceProxy { get; set; }
        public string DisplayString { get; set; }
        public bool ButtonsEnabled { get; set; }
    }
}
