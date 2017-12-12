using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chat
{
  public class PacketListener
  {
    UdpClient UdpClient { get; }
    public Thread thread;
    public Queue<string> MessageBuffer;
    public MainWindow _view;

    public PacketListener(MainWindow view)
    {
      MessageBuffer = new Queue<string>();
      _view = view;
      UdpClient = new UdpClient();
    }

    private void Listen()
    {
      IPEndPoint RemoteIpEndPoint = App.ActiveUsers[2].EndPoint;

      try
      {
        UdpClient.Connect(App.ActiveUsers[2].EndPoint);

        //_view.textBoxChatPane.Text =
        //   "\n-----------*******Общий чат*******-----------\n";

        while (true)
        {

          byte[] receiveBytes = null;
          while (UdpClient.Available > 0)
          {
            // Ожидание дейтаграммы
            receiveBytes = UdpClient.Receive(
                ref RemoteIpEndPoint);
          }

          // Преобразуем и отображаем данные
          string returnData = Encoding.UTF8.GetString(receiveBytes);
          lock (MessageBuffer)
          {
            MessageBuffer.Enqueue(returnData);
            // _view.textBoxChatPane.Text += " --> " + returnData.ToString() + "\n";
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Send, (ThreadStart)delegate()
            {
              _view.textBoxChatPane.Text += " --> " + MessageBuffer.Dequeue() + "\n";

            });
          }
          Thread.Sleep(TimeSpan.FromSeconds(2));
        }
      }
      catch (Exception ex)
      {
        //_view.textBoxChatPane.Text += "Возникло исключение: " + ex.ToString() + "\n  " + ex.Message;
      }
    }

    public void Start()
    {
      thread = new Thread(Listen);
      thread.IsBackground = true;
      thread.Start();
    }
  }
}
