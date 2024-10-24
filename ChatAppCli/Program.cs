using ChatAppCli.Net.IO;
using ChatAppCli;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;




string name;
string message;

Console.Write("Nimi: ");
name = Console.ReadLine();

Server server = new Server();
server.ConnectToServer(name);

Console.Write("Message: ");
message = Console.ReadLine();

server.SendMessageToServer(message);
Console.WriteLine(name + " sent message: " + message);

// If you want to keep the console open until the user presses a key
Console.WriteLine("Press any key to exit...");
Console.ReadKey();