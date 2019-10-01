using RemoteInterface;
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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void VolUp_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.VolumeUp);
        }

        protected void VolDown_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.VolumeDown);
        }

        protected void LeftKey_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.LeftArrow);
        }

        protected void RightKey_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.RightArrow);
        }

        protected void Space_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.Space);
        }

        protected void Previous_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.Previous);
        }

        protected void PlayPause_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.PlayPause);
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.Next);
        }


        protected void Shutdown_Click(object sender, EventArgs e)
        {
            RemoteSender.Send(RemoteCommand.Shutdown);
        }

        protected void LaunchServer_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\simon\Desktop\RemoteReceiver.exe");
        }
    }
}