using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UdpSendingWpf
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      Thread thread = new Thread(UdpListen);
      thread.IsBackground = true;
      InitializeComponent();
      thread.Start();
    }

    private void UdpListen()
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
    public delegate string CustomDelegate(string message);
    private string M(string message)
    {
      textBoxChatPane.Text += message;
      return textBoxChatPane.Text;
    }
    private void UpdateView(string message)
    {
      var disp = Dispatcher.CurrentDispatcher;
      CustomDelegate sdkad = M;
      var op = textBoxChatPane.Dispatcher.BeginInvoke(DispatcherPriority.Normal, sdkad,message);
      if (op.Result!= null)
      {

      }
    }

    private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
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
  }
}
