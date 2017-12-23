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
    public List<User> connectedUsersList = App.ServiceUsers.ConnectedUsers;
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
      App.ServiceUsers.NewConnectedUserEvent += UpdateConnectedUsersList;

      mainWindowDispatcher = mainDispatcher;
      Thread thread = new Thread(packetListener.UdpListen);
      packetListener.NewUserMessage += UpdateView;
      thread.IsBackground = true;
      thread.Start();
    }
    public void UpdateConnectedUsersList(object sender, EventArgs e)
    {

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
