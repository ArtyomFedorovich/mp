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
  }
}
