using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Messenger_Prototype
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class P2PService : IP2PService
    {
        private MainWindow hostReference;
        private string username;

        public P2PService(MainWindow hostReference, string username)
        {
            this.hostReference = hostReference;
            this.username = username;
        }

        public string GetName()
        {
            return username;
        }

        public void SendMessage(string message, string from)
        {
            hostReference.DisplayMessage(message, from);
        }
    }
}
