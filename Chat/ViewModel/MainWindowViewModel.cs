using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Input;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System;

namespace Chat
{
  public class MainWindowViewModel : INotifyPropertyChanged
  {
    private string textBoxPaneChatPaneContent = string.Empty;
    private List<User> connectedUsersList = new List<User>() { App.ServiceUsers.LocalUser };
  private UdpPacketListener packetListener = new UdpPacketListener();
    private UdpPacketSender packetSender = new UdpPacketSender();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private Dispatcher mainWindowDispatcher;
    public delegate void UpdateContent(string message, string receiverLogin);

    public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

    public string TextBoxChatPaneContent
    {
      get
      {
        return textBoxPaneChatPaneContent;
      }
      private set
      {
        this.textBoxPaneChatPaneContent = value;
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(TextBoxChatPaneContent)));
      }
    }
    public string TextBoxEntryFieldContent { get; private set; }
    public List<User> ConnectedUsersList
    {
      get
      {
        return connectedUsersList;
      }
      private set
      {
        connectedUsersList = value;
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(ConnectedUsersList)));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public MainWindowViewModel(Dispatcher mainDispatcher)
    {
      mainWindowDispatcher = mainDispatcher;
      Thread thread = new Thread(packetListener.UdpListen);
      packetListener.NewUserMessage += UpdateView;
      thread.IsBackground = true;
      thread.Start();

      // Thread for users list renderer.
      Thread renderThread = new Thread(UpdateConnectedUserList);
      renderThread.IsBackground = true;
      renderThread.Start();
    }

    public void UpdateConnectedUserList()
    {
      while (true)
      {
        ConnectedUsersList = new List<User>() { App.ServiceUsers.LocalUser };
        ConnectedUsersList.AddRange(App.ServiceUsers.ConnectedUsers);

        Thread.Sleep(TimeSpan.FromSeconds(5));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private void UpdateTextBoxChatPane(string message, string receiverLogin)
    {
      TextBoxChatPaneContent += (receiverLogin + "> " + message + "\n");
    }
    private void UpdateView(object sender, UdpPacketListener.MessageEventArgs messageArg)
    {
      mainWindowDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateContent(UpdateTextBoxChatPane),
        messageArg.TargetMessage, messageArg.ReceiverLogin);
    }

    public void SendUdpMessageToAllUsers(string message)
    {
      //UpdateTextBoxChatPane(message, App.ServiceUsers.LocalUser.Login);
      packetSender.SendMessageToAllUsers(message);
    }
  }
}
