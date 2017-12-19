using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat
{
  /// <summary>
  /// Object that do listen to incoming UDP packets.
  /// </summary>
  public class UdpPacketListener
  {
    private UdpClient listenerClient;
    private UdpPacketFormatter packetFormatter = new UdpPacketFormatter();

    public class MessageEventArgs : EventArgs
    {
      public string TargetMessage { get; private set; }
      public string ReceiverLogin { get; private set; }
      public MessageEventArgs(string message, string receiverLogin)
      {
        TargetMessage = message;
        ReceiverLogin = receiverLogin;
      }
    }
    public event EventHandler<MessageEventArgs> NewUserMessage;
    public void UdpListen()
    {
      listenerClient = new UdpClient();
      IPEndPoint socket = new IPEndPoint(IPAddress.Any, App.ServicePortValue);
      listenerClient.Client.Bind(socket);

      while (true)
      {
        lock (listenerClient)
        {
          byte[] pdata = listenerClient.Receive(ref socket);

          Debug.WriteLine(socket.Address.ToString());
          Debug.WriteLine(BitConverter.ToString(pdata));

          var packetType = packetFormatter.GetPacketTypeFromPacket(pdata);
          if (packetType == UdpPacketFormatter.PacketType.Echo)
          {
            App.ServiceUsers.AddConnectedUser(packetFormatter.GetUserInfoFromEchoPacket(pdata));
          }
          else
          {
            string message = packetFormatter.GetMessageContentFromMessagePacket(pdata);
            string receiver = packetFormatter.GetMessageReceiverFromMessagePacket(pdata).Login;
            if (!string.IsNullOrEmpty(message))
            {
              NewUserMessage(this, new MessageEventArgs(message, receiver));
            }
          }
          
          Thread.Sleep(20);
        }
      }

    }
  }
}
