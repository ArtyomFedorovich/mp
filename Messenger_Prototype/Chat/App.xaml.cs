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
        public static List<User> ActiveUsers;
        public User CurrentUser { get; }
        public App()
        {
            ActiveUsers = new List<User>();
            // костыль
            byte[] address = new byte[4] { 192, 168, 10, 3 };

            CurrentUser = new User(new UserInfo("Me", 1), new IPEndPoint(new IPAddress(address), 49002));
            address = new byte[4] { 10, 160, 0, 53 };
            var DistanceUser = new User(new UserInfo("Constantinopolich", 2), new IPEndPoint(new IPAddress(address), 49001));
            ActiveUsers.Add(CurrentUser);
            ActiveUsers.Add(DistanceUser);
        }
    }
}
