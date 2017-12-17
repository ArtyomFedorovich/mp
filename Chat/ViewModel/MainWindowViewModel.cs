using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows.Input;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Chat
{
  public class MainWindowViewModel : INotifyPropertyChanged
  {
    private string textBoxPaneChatPaneContent = string.Empty;
    private UdpPacketListener packetListener = new UdpPacketListener();
    private UdpPacketSender packetSender = new UdpPacketSender();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private Dispatcher mainWindowDispatcher;
    public delegate void UpdateContent(string message);

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
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private void UpdateTextBoxChatPane(string message)
    {
      TextBoxChatPaneContent += ("\n" + message);
    }
    private void UpdateView(object sender, UdpPacketListener.MessageEventArgs messageArg)
    {
      mainWindowDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateContent(UpdateTextBoxChatPane),
        messageArg.TargetMessage);
    }

    public void SendUdpMessageToAllUsers(string message)
    {
      packetSender.SendToAllUsers(message);
    }
  }
}
