using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat
{
  public class UdpPacketSender
  {
    private UdpPacketFormatter packetFormatter = new UdpPacketFormatter();

    public void SendToAllUsers(string content)
    {
      UdpClient senderClient = null;
      try
      {
        senderClient = new UdpClient();
        IPEndPoint receiver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 48911);
        
        byte[] bytes = packetFormatter.CreateMessagePacket(content, App.ServiceUsers.LocalUser);
        foreach (var user in App.ServiceUsers.ConnectedUsers)
        {
          senderClient.Send(bytes, bytes.Length, user.UserSocket);
        }       
      }
      finally
      {
        senderClient.Close();
      }
    }
  }
}
