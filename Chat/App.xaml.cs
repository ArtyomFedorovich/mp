using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace Chat
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {
    public const int ServiceSocketValue = 48911;
    public static Users ServiceUsers { get; private set; } = new Users();
    public const string LocalUserName = "Me";

    public static User GetUserLoginBySocket(IPEndPoint socket)
    {
      if (socket.Address.ToString() == "127.0.0.1")
      {
        return ServiceUsers.LocalUser;
      }
      else
      {
        return ServiceUsers.ConnectedUsers.Find(x => x.UserSocket == socket);   
      }
    }
    public App()
    {

    }
  }
}
