using ChatAppCli.Net.IO;
using ChatAppCli;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using ChatAppCli.ViewModel;
using ChatAppCli.Model;

class Program
{


    static void Main()
    {
        Console.WriteLine("Enter name to connect");
        var name = Console.ReadLine();
        var message = string.Empty;
        var input = new MainViewModel(name);


        while (true)
        {
            message = Console.ReadLine();
            input.SendMessage(message);
        }
    }
}
