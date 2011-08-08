using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Add the below:
using System.Windows;

//From a Console Application to Windows application
namespace _1_Fundamentals
{
    class Program
    {
        [STAThread]//Without this Invalid Operation Exception for any WPF program
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            Window winHelloWorld = new Window();
            winHelloWorld.Title = "Hello, world";
            winHelloWorld.Content = "I am the Hello world screen";
            //winHelloWorld.Show();

            Application app = new Application();
            //Only One Application object is allowed per app domain. If not exception.
            //Application app2 = new Application();  //Uncommenting this will throw exception
            app.Run(winHelloWorld);
            Console.Read();
        }
    }
}
