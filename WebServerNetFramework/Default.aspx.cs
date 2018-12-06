using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
                //await Task.Delay(2000);
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        }
    }
}