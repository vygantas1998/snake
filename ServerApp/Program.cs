﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApp
{
    class Program
    {

        public static void Main(string[] args)
        {
            SnakeServer server = new SnakeServer();
            server.ExecuteServer();
        }
    }
}
