﻿using ChatApp.Net;
using ChatClient.MVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.MVVM.ViewModel
{
    class MainViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }
        
        private Server _server;
        public MainViewModel() 
        {
            _server = new Server();
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer());
        
        }
    }
}