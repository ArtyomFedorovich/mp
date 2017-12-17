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
    private MainWindowViewModel viewModel;
    //UdpPacketListener packetListener = new UdpPacketListener();
    public MainWindow()
    {
      viewModel = new MainWindowViewModel(Dispatcher.CurrentDispatcher);
      this.DataContext = viewModel;
      InitializeComponent();
    }
    /*
    public delegate void UpdateContent(string message);
    private void UpdateTextBoxChatPane(string message)
    {
      textBoxChatPane.Text += message;
    }
    private void UpdateView(object sender, UdpPacketListener.MessageEventArgs messageArg)
    {
      var updateTextBoxChatPaneResult = textBoxChatPane.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        new UpdateContent(UpdateTextBoxChatPane), messageArg.TargetMessage);
    }*/

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
        viewModel.SendUdpMessageToAllUsers(textBoxEntryField.Text);
        textBoxEntryField.Clear();
      }
    }
    #endregion
  }
}
