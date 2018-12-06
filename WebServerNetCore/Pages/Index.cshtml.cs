using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WindowsInput;
using WindowsInput.Native;

namespace WebServerNetCore.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        }
    }
}
