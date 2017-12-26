using System.Collections.Generic;
using System.Net;
using System.Linq;
using System;

namespace Chat
{
  public class Users
  {
    public User LocalUser = new User(new IPEndPoint(IPAddress.Parse("192.168.10.3"), App.ServicePortValue), 
      App.LocalUserName, string.Empty);
    public List<User> ConnectedUsers { get; private set; }

    public Users()
    {
      ConnectedUsers = new List<User>();
    }

    public void AddConnectedUser(User user)
    {
      if ((ConnectedUsers.FirstOrDefault(u => user.Login == u.Login) == null)
        && ConnectedUsers.FirstOrDefault(u => user.Login == App.ServiceUsers.LocalUser.Login) == null)
      {
        ConnectedUsers.Add(user);
      }
    }
  }
}
