using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserPanel.User_Data_and_Structures;

namespace UserPanel.Panels.MessageMenu
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Message()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBlock.Text = SentMessage.MessageText;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            SentMessage.MessageAnswer = false;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            SentMessage.MessageAnswer = true;
            this.Close();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            SentMessage.MessageAnswer = false;
            this.Close();
        }
    }
}
