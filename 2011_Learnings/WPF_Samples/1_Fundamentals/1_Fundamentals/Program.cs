using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Add the below:
using System.Windows;

namespace _1_Fundamentals
{
    class Program
    {
        [STAThread]//Without this Invalid Operation Exception for any WPF program
        static void Main(string[] args)
        {
            
            Window winHelloWorld = new Window();
            winHelloWorld.Title = "Hello, world";
            winHelloWorld.Content = "I am the Hello world screen";
            //winHelloWorld.Show();

            Application app = new Application();
            Application app2 = new Application();
            app.Run(winHelloWorld);
            app2.Run(winHelloWorld);
        }
    }
}
