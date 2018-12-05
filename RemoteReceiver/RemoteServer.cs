using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteReceiver
{
    public class RemoteServer
    {
        static Socket newConnectionSocket;

        public static void StartListening()
        {
            IPAddress localAddress = GetLocalIp();
            IPEndPoint localEndPoint = new IPEndPoint(localAddress, 1337);
            newConnectionSocket = new Socket(localAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Debug.WriteLine("Returning IP address : {0}", localAddress.ToString());
            SysTray.DisplayString(localAddress.ToString());

            try
            {
                newConnectionSocket.Bind(localEndPoint);
                newConnectionSocket.Listen(100);
                WaitForConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void WaitForConnection()
        {
            Console.WriteLine("Waiting for a connection...");
            newConnectionSocket.BeginAccept(new AsyncCallback(AcceptCallback), newConnectionSocket);
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            WaitForConnection();
            throw new NotImplementedException();
        }

        public static IPAddress GetLocalIp()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress localAddress = ipHostInfo.AddressList.Where(addr => addr.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();

            if (localAddress == null)
                localAddress = new IPAddress(Encoding.ASCII.GetBytes("127.0.0.1"));

            return localAddress;
        }
    }
}
