using ChatAppCli;
using ChatAppCli.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppCli.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        private Server _server;

        public MainViewModel(string name)
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.userDisconnectedEvent += RemoveUser;
            _server.ConnectToServer(name);
        }

        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Messages.Add(msg);
            Console.WriteLine(msg);
        }

        public void SendMessage(string message)
        {
            _server.SendMessageToServer(message);
            Messages.Add(message);
        }


        private void UserConnected()
        {
            var user = new UserModel
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()
            };

            if (!Users.Any(x => x.UID == user.UID))
            {
                Users.Add(user);
            }
            Console.WriteLine("Connected user list " + user.Username);
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Users.Remove(user);
        }

    }
}