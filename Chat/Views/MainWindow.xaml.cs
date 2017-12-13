using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Chat
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      Thread thread = new Thread(new UdpPacketListener().UdpListen);
      thread.IsBackground = true;
      InitializeComponent();
      thread.Start();
    }

    public delegate void UpdateContent(string message);
    private void UpdateTextBoxChatPane(string message)
    {
      textBoxChatPane.Text += message;
    }
    private void UpdateView(string message)
    {
      var op = textBoxChatPane.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        new UpdateContent(UpdateTextBoxChatPane), message);
    }

    #region FormEvents

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TextBoxEntryField_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Return || e.Key == Key.Enter)
      {
        UdpClient senderich = null;
        try
        {
          senderich = new UdpClient();
          IPEndPoint receiver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 48911);
          byte[] bytes = Encoding.Unicode.GetBytes(textBoxEntryField.Text);
          senderich.Send(bytes, bytes.Length, receiver);
        }
        finally
        {
          senderich.Close();
        }
        textBoxEntryField.Clear();
        //Thread.Sleep(TimeSpan.FromSeconds(5));
      }
    }
    #endregion
  }
}
