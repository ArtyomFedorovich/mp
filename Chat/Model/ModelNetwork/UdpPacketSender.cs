using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat
{
  public class UdpPacketSender
  {
    private UdpClient senderClient;
    private UdpPacketFormatter packetFormatter = new UdpPacketFormatter();

    public void SendMessageToAllUsers(string content)
    {
      lock(senderClient = new UdpClient())
      {        
        byte[] bytes = packetFormatter.CreateMessagePacket(content, App.ServiceUsers.LocalUser);
        senderClient.Send(bytes, bytes.Length, App.ServiceUsers.LocalUser.UserSocket);
        foreach (var user in App.ServiceUsers.ConnectedUsers)
        {
          senderClient.Send(bytes, bytes.Length, user.UserSocket);
        }       
      }
    }
    public void SendLocalUserInfoToAllUsers()
    {
      lock (senderClient = new UdpClient())
      {
        byte[] bytes = packetFormatter.CreateLocalUserInfoPacket();
        foreach (var user in App.ServiceUsers.ConnectedUsers)
        {
          senderClient.Send(bytes, bytes.Length, user.UserSocket);
        }
      }
    }
  }
}
