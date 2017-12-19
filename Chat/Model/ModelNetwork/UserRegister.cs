using System;
using System.Threading;

namespace Chat
{
  public class UserRegister
  {
    public void SendLocalUserInfo()
    {
      while(true)
      {
        UdpPacketSender packetSender;
        lock (packetSender = new UdpPacketSender())
        {
          packetSender.SendLocalUserInfoToAllUsers();
        }

        Thread.Sleep(TimeSpan.FromSeconds(5));
      }
    }
  }
}
