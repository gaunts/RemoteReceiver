using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WindowsInput;
using WindowsInput.Native;

namespace WebServerNetFramework
{
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public partial class _Default : Page
    {
        public static IPAddress GetLocalIp()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress localAddress = ipHostInfo.AddressList.Where(addr => addr.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();

            if (localAddress == null)
                localAddress = new IPAddress(Encoding.ASCII.GetBytes("127.0.0.1"));

            return localAddress;
        }

        public static void StartClient()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = GetLocalIp();
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1337);
                Debug.WriteLine(ipAddress.ToString());
                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    Debug.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);
                    Debug.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Debug.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Debug.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void volUp_Click(object sender, EventArgs e)
        {
            StartClient();
            //InputSimulator sim = new InputSimulator();
            //sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        }

        protected void foo_OnClick(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        }

        protected void volDown_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
        }

        protected void leftKey_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
        }

        protected void rightKey_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
        }

        protected void space_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        }

        protected void shutdown_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0 /f") { CreateNoWindow = true, UseShellExecute = false });
        }

        [System.Web.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ActionName("Simple")]
        public HttpResponseMessage PostSimple([FromBody] string value)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
            var response = new HttpResponseMessage(HttpStatusCode.Accepted);
            return response;
        }
    }
}