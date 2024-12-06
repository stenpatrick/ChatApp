﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatAppCli.Net.IO;


namespace ChatAppCli
{
    class Server
    {
        TcpClient _client;
        public PacketReader PacketReader;
        public event Action connectedEvent;
        public event Action msgReceivedEvent;
        public event Action userDisconnectedEvent;
        public Server()
        {
            _client = new TcpClient();
        }

        public bool ConnectToServer(string username)
        {
            try
            {
                if (!_client.Connected)
                {
                    _client.Connect("chatserver", 7891);
                    PacketReader = new PacketReader(_client.GetStream());

                    ReadPackets();
                    if (!string.IsNullOrEmpty(username))
                    {
                        var connectPacket = new PacketBuilder();
                        connectPacket.WriteOpCode(0);
                        connectPacket.WriteMessage(username);
                        _client.Client.Send(connectPacket.GetPacketBytes());
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }

            return false;
        }


        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch (opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        case 5:
                            msgReceivedEvent?.Invoke();
                            break;
                        case 10:
                            userDisconnectedEvent?.Invoke();
                            break;


                    }
                }
            });
        }

        public void SendMessageToServer(string message)
        {
            var messagePacket = new PacketBuilder();
            messagePacket.WriteOpCode(5);
            messagePacket.WriteMessage(message);
            _client.Client.Send(messagePacket.GetPacketBytes());
        }
    }
}