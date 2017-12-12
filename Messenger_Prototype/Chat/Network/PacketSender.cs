using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat
{
  public class PacketSender
  {
    public void Send(string message)
    {
      UdpClient udpClient = new UdpClient();
      udpClient.Connect(App.ActiveUsers[2].EndPoint);

      byte[] bytes = Encoding.UTF8.GetBytes(message);
      // Отправляем данные
      udpClient.Send(bytes, bytes.Length);
      /*
      for (int i = 0; i < 10; i++)
      {
          //byte[] packet = new Packet().ConvertPacket("Hello");
          //udpClient.Send(packet, packet.Length);

          byte[] bytes = Encoding.UTF8.GetBytes(@"Hello {i++}");
          // Отправляем данные
          udpClient.Send(bytes, bytes.Length);
          Thread.Sleep(10000);
      }*/

      udpClient.Close();
    }
  }
}
