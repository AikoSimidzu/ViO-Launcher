namespace UserPanel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using UserPanel.Classes;
    using UserPanel.FrontEndB;
    using UserPanel.User_Data_and_Structures;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string log = "1";
            Task.Run(() => 
            {
                string myS = string.Empty;
                if (AllocConsole())
                {
                    while (log != "stop")
                    {
                        if (myS != log)
                        {
                            Console.WriteLine(log);
                            myS = log;
                        }
                    }
                    FreeConsole();
                }
            });
            
            DataContext = new Blureffect(this, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND);
            log = "Blur effect activated...";

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            if (UserData.LoadData())
            {
                log = "User Data loaded...";

                log = "Connect to server...";
                if (Server.Auth(UserData.Login, Helper.pwdEncrypt(UserData.Password)))
                {
                    log = "True";
                    WorkPanel wp = new();
                    wp.Show();
                    this.Hide();
                    //log = "stop";
                }
                else
                {
                    SentMessage.Show("Unsuccessful authorization attempt!");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (new Regex("[;@%=+_ ]").Matches(username.Text).Count == 0 && username.Text.Length > 3)
            {
                if (new Regex("[;@%=+_ ]").Matches(password.Password).Count == 0 && password.Password.Length > 5)
                {
                    if (Server.Auth(username.Text, Helper.pwdEncrypt(password.Password)))
                    {
                        UserData.Login = username.Text; UserData.Password = password.Password;
                        UserData.SaveData();

                        WorkPanel wp = new();
                        wp.Show();
                        this.Hide();
                    }
                    else
                    {
                        SentMessage.Show("Unsuccessful authorization attempt!");
                    }
                }
                else
                {
                    SentMessage.Show("The password must not have special characters, spaces, and its minimum length is 6 characters.");
                }
            }
            else
            {
                SentMessage.Show("The login must not have special characters, spaces, and its minimum length is 4 characters.");
            }
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            if (new Regex("[;@%=+_ ]").Matches(username.Text).Count == 0 && username.Text.Length > 3)
            {
                if (new Regex("[;@%=+_ ]").Matches(password.Password).Count == 0 && password.Password.Length > 5)
                {
                    if (Server.Register(username.Text, Helper.pwdEncrypt(password.Password)))
                    {
                        UserData.Login = username.Text; UserData.Password = password.Password;
                        UserData.SaveData();

                        WorkPanel wp = new();
                        wp.Show();
                        this.Hide();
                    }
                    else
                    {
                        SentMessage.Show("This login is busy or you already have an account!");
                    }
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                }
                else
                {
                    SentMessage.Show("The password must not have special characters, spaces, and its minimum length is 6 characters.");
                }
            }
            else
            {
                SentMessage.Show("The login must not have special characters, spaces, and its minimum length is 4 characters.");
            }
        }
    }
}
