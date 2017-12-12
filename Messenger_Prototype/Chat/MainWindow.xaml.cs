using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;

namespace Chat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PacketSender packetSender;
        public MainWindow()
        {
            InitializeComponent();
            packetSender = new PacketSender();
            //DataContext = new GeneralViewModel();           
        }
        /*public void DisplayMessage(CompositeType composite)
        {
            string username = composite.Username == null ? "" : composite.Username;
            string message = composite.Message == null ? "" : composite.Message;
            textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);
        }*/

        private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                packetSender.Send(textBoxEntryField.Text);
                //((PacketSender)sender).Send((textBoxEntryField.Text));
                textBoxEntryField.Clear();
            }
        }
    }
}
