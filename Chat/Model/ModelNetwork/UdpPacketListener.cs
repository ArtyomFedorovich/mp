using System;
using System.Collections.Generic;
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
      public string TargetMessage { get; protected set; }
      public MessageEventArgs(string message)
      {
        TargetMessage = message;
      }
    }
    public event EventHandler<MessageEventArgs> NewUserMessage;
    public void UdpListen()
    {
      IPEndPoint socket = new IPEndPoint(IPAddress.Any, App.ServiceSocketValue);
      listenerClient.Client.Bind(socket);

      while (true)
      {
        lock (listenerClient = new UdpClient())
        {
          byte[] pdata = listenerClient.Receive(ref socket);

          var packetType = packetFormatter.GetPacketTypeFromPacket(pdata);
          if (packetType == UdpPacketFormatter.PacketType.Echo)
          {
            App.ServiceUsers.ConnectedUsers.Add(packetFormatter.GetUserInfoFromEchoPacket(pdata));
          }
          else
          {
            string message = Encoding.Unicode.GetString(pdata);
            if (!string.IsNullOrEmpty(message))
            {
              NewUserMessage(this, new MessageEventArgs(message));
            }
          }
          
          Thread.Sleep(TimeSpan.FromSeconds(2));
        }
      }

    }
  }
}
