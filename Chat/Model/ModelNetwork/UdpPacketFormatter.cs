using System;
using System.Collections.Generic;
using System.Text;

namespace Chat
{
  public class UdpPacketFormatter
  {
    public enum PacketType : short
    {
      Message,
      Echo
    }

    public string GetMessageContentFromMessagePacket(byte[] packet)
    {

    }

    public User GetUserInfoFromEchoPacket(byte[] packet)
    {
      var userIp = new byte[4] { packet[2], packet[3], packet[4], packet[5] };
      var userPort = new byte[4] { packet[6], packet[7], packet[8], packet[9] };
      var userLogin = new byte[packet.Length - 10];
      for (int i = 0; i < userLogin.Length; i++)
      {
        userLogin[i] = packet[10 + i];
      }
      return new User(new System.Net.IPEndPoint(BitConverter.ToInt64(userIp, 0), BitConverter.ToInt32(userPort, 0)),
        Encoding.Unicode.GetString(userLogin), string.Empty);
    }
    public PacketType GetPacketTypeFromPacket(byte[] packet)
    {
      const int bytesInShort = 2;
      return (PacketType)BitConverter.ToInt16(new byte[bytesInShort] { packet[0], packet[1] }, 0); 
    }

    public byte[] CreateMessagePacket(string content, User receiver)
    {
      List<byte> packetContent = new List<byte>();
      byte[] header = BitConverter.GetBytes((short)PacketType.Message);
      packetContent.AddRange(header);

      byte[] receiverIp = receiver.UserSocket.Address.GetAddressBytes();
      byte[] receiverPort = BitConverter.GetBytes(receiver.UserSocket.Port);
      packetContent.AddRange(receiverIp);
      packetContent.AddRange(receiverPort);

      packetContent.AddRange(Encoding.Unicode.GetBytes(content));

      return packetContent.ToArray();
    }

    public byte[] CreateLocalUserInfoPacket()
    {
      List<byte> packetContent = new List<byte>();
      byte[] header = BitConverter.GetBytes((short)PacketType.Echo);
      packetContent.AddRange(header);

      byte[] userIp = App.ServiceUsers.LocalUser.UserSocket.Address.GetAddressBytes();
      byte[] userPort = BitConverter.GetBytes(App.ServiceUsers.LocalUser.UserSocket.Port);
      packetContent.AddRange(userIp);
      packetContent.AddRange(userPort);

      byte[] userLogin = Encoding.Unicode.GetBytes(App.ServiceUsers.LocalUser.Login);
      packetContent.AddRange(userLogin);

      return packetContent.ToArray();
    }
  }
}
