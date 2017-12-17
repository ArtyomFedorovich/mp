using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
    public const string LocalUsersName = "Me";

    public App()
    {

    }
  }
}
