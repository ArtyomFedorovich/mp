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

namespace Chat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow current;
        PacketListener listener = new PacketListener();
        PacketSender sender = new PacketSender();
        List<User> users = App.ActiveUsers;

        public MainWindow()
        {
            InitializeComponent();
            current = this;
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
                this.sender.Send(textBoxEntryField.Text);
                //((PacketSender)sender).Send((textBoxEntryField.Text));
                textBoxEntryField.Clear();
            }
        }
    }
}
