using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _4_Style
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Height = 75; win.Width = 300;
            win.Title = "ThreeDBorderWindow";

            win.WindowStyle = WindowStyle.ThreeDBorderWindow;
            win.Content = win.WindowStyle.ToString();

            win.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Height = 75; win.Width = 300;
            win.Title = "SingleBorderWindow";

            win.WindowStyle = WindowStyle.SingleBorderWindow;
            win.Content = win.WindowStyle.ToString();

            win.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Height = 75; win.Width = 300;
            win.Title = "ToolWindow";

            win.WindowStyle = WindowStyle.ToolWindow;
            win.Content = win.WindowStyle.ToString();

            win.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Height = 75; win.Width = 300;
            win.Title = "None";

            win.WindowStyle = WindowStyle.None;
            win.Content = win.WindowStyle.ToString();

            win.Show();

        }
    }
}
