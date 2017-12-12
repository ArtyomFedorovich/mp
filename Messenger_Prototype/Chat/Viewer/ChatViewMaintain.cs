using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Chat
{
  public class ChatViewMaintain
  {
    //public Thread thread;
    public static MainWindow _view;
    public PacketListener listener;

    public ChatViewMaintain(PacketListener listener)
    {
      this.listener = listener;
      _view = new MainWindow();
      _view.Show();
    }

    public void Display()
    {
      //Thread.Sleep(10000);
      _view.textBoxChatPane.Text = "\n-----------*******Общий чат*******-----------\n";
      try
      {
        while (true)
        {
          lock (listener.MessageBuffer)
          {
            while (listener.MessageBuffer.Count != 0)
            {
              _view.textBoxChatPane.Text += listener.MessageBuffer.Dequeue();
            }
          }
          Thread.Sleep(100);
        }
      }
      catch (Exception ex)
      {

      }
    }

    /*public void Start()
    {
        thread = new Thread(Display);
        thread.Start();
    }*/
  }
}
