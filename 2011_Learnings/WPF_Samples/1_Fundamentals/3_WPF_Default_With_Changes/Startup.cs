using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_WPF_Default_With_Changes
{
    public class Startup
    {
        [STAThread]
        public static void Main()
        {
            MainWindow wnd = null;
            try
            {
                wnd = new MainWindow();
            }
            catch { }

            App app = new App();

            app.Run(wnd);
        }

    }
}
