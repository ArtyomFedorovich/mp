using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace Chat
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  public partial class App : Application
  {
    public PacketListener listener;
    public MainWindow view;

    public static List<User> ActiveUsers;
    public User CurrentUser { get; }
    public App()
    {
      ActiveUsers = new List<User>();
      // костыль
      CurrentUser = new User(new UserInfo("Me", 1),
          new IPEndPoint(IPAddress.Parse("192.168.10.3"), 49002));

      var DistanceUser = new User(new UserInfo("Constantinopolich", 2),
          new IPEndPoint(IPAddress.Parse("10.160.0.53"), 49001));

      var LocalDistanceUser = new User(new UserInfo("CurrentMashine", 3),
          new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49001));

      ActiveUsers.Add(CurrentUser);
      ActiveUsers.Add(DistanceUser);
      ActiveUsers.Add(LocalDistanceUser);

      MainWindow mainWindow = new MainWindow();

      listener = new PacketListener(mainWindow);
      listener.Start();
      //
      //
      mainWindow.Show();
      //Thread.Sleep(100);
      //
    }
  }
}
