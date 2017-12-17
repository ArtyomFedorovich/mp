using System.Net;

namespace Chat
{
  public class User
  {
    public IPEndPoint UserSocket { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }

    public User(IPEndPoint socket, string login, string password)
    {
      UserSocket = socket;
      Login = login;
      Password = password;
    }
  }
}
