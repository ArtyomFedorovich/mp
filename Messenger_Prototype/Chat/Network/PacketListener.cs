using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat
{
    public class PacketListener
    {
        UdpClient UdpClient { get; } = new UdpClient();
        MainWindow view = MainWindow.current;

        [STAThread]
        public void Listen()
        {
            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                UdpClient.Connect(App.ActiveUsers[1].EndPoint);
                view.textBoxChatPane.Text = 
                   "\n-----------*******Общий чат*******-----------\n";

                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = UdpClient.Receive(
                       ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    view.textBoxChatPane.Text += " --> " + returnData.ToString() + "\n";
                }
            }
            catch (Exception ex)
            {
                view.textBoxChatPane.Text += "Возникло исключение: " + ex.ToString() + "\n  " + ex.Message;
            }
        }
    }
}
