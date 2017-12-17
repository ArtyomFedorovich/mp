using System.Collections.Generic;
using System.Net;

namespace Chat
{
  public class Users
  {
    public User LocalUser = new User(new IPEndPoint(IPAddress.Parse("127.0.0.1"), App.ServiceSocketValue), 
      App.LocalUsersName, string.Empty);
    public List<User> ConnectedUsers { get; private set; }

    public Users()
    {
      ConnectedUsers = new List<User>();
    }

    public void AddConnectedUser(User user)
    {
      ConnectedUsers.Add(user);
    }
  }
}
