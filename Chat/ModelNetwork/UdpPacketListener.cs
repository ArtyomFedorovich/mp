using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
  /// <summary>
  /// Object that do listen to incoming UDP packets.
  /// </summary>
  public class UdpPacketListener
  {
    public void UdpListen()
    {
      UdpClient listener = new UdpClient();
      IPEndPoint socket = new IPEndPoint(IPAddress.Any, 48911);
      listener.Client.Bind(socket);

      while (true)
      {
        try
        {
          byte[] pdata = listener.Receive(ref socket);
          string message = Encoding.Unicode.GetString(pdata);
          if (!string.IsNullOrEmpty(message))
          {
            UpdateView(message);
          }
          Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        finally
        {
          //listener.Close();
        }
      }
    }
  }
}
