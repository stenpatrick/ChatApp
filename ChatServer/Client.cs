﻿using System.Net.Sockets;
using ChatServer.Net.IO;

namespace ChatServer;

public class Client
{
    public string Username { get; set; }
    public Guid UID { get; set; }
    public TcpClient ClientSocket { get; set; }
    PacketReader _packetReader;
    public Client(TcpClient client)
    {
        ClientSocket = client;
        UID = Guid.NewGuid();
        _packetReader = new PacketReader(ClientSocket.GetStream());

        var opcode = _packetReader.ReadByte();
        Username = _packetReader.ReadMessage();
        Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {Username}");
        Task.Run(() => Process());
    }

    void Process()
    {
        while (true)
        {
            try
            {
                var opcode = _packetReader.ReadByte();
                switch (opcode)
                {
                    case 5:
                        var msg = _packetReader.ReadMessage();
                        Console.WriteLine($"$[{DateTime.Now}]: Message received! {msg}");
                        Program.BroadcastMessage($"[{DateTime.Now}]: [{Username}]: {msg}");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"[{UID}]: Disonnected!");
                Program.BroadcastDisconnect(UID.ToString());
                ClientSocket.Close();
                break;
            }
            
        }
    }
}