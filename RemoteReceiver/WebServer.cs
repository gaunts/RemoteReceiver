using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteReceiver
{
    class WebServer
    {
        public static void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...", Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("A client connected.");
        }
    }
}
