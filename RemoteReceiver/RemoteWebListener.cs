using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RemoteInterface;

namespace RemoteReceiver
{
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class RemoteWebListener
    {
        static Socket newConnectionSocket;

        public static void StartListening()
        {
            IPAddress localAddress = GetLocalIp();
            IPEndPoint localEndPoint = new IPEndPoint(localAddress, 1337);
            newConnectionSocket = new Socket(localAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

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
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject {
                workSocket = handler
            };
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            String content = String.Empty;

            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = 0;
            try
            {
                bytesRead = handler.EndReceive(ar);
            }
            catch(SocketException e)
            {
                return;
            }

            Debug.WriteLine("receive : {0}", bytesRead);
            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if (content.IndexOf("<EOF>") > -1)
                {
                    content = content.Substring(0, content.Length - "<EOF>".Length);
                    Debug.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    if (Enum.TryParse(content, out RemoteCommand command))
                        CommandExecution.ExecuteCommand(command);
                    state.sb = new StringBuilder();
                }

                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
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
