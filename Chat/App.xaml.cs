using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace Chat
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {
    public const int ServicePortValue = 48911;
    public static Users ServiceUsers { get; private set; } = new Users();
    public const string LocalUserName = "Me";

    public static User GetUserLoginBySocket(IPEndPoint socket)
    {
      if (socket.Address.Equals(ServiceUsers.LocalUser.UserSocket.Address))
      {
        return ServiceUsers.LocalUser;
      }
      else
      {
        return ServiceUsers.ConnectedUsers.Find(x => x.UserSocket.Equals(socket));   
      }
    }

    public App()
    {
      UserRegister register = new UserRegister();
      Thread thread = new Thread(register.SendLocalUserInfo);
      thread.IsBackground = true;
      thread.Start();
    }
  }
}
