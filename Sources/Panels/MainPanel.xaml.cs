namespace UserPanel.Panels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using UserPanel.Classes;
    using UserPanel.User_Data_and_Structures;

    /// <summary>
    /// Логика взаимодействия для MainPanel.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        private static ObservableCollection<UserInfo> users = new ObservableCollection<UserInfo>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => 
            {
                while (true)
                {
                    users = Server.getUsers();
                    Thread.Sleep(50);
                }
            });

            userDataGrid.ItemsSource = users;
            //for (int i = 0; i < 15; i++)
            //{
            //    Random random = new Random();
            //    users.Add(new UserInfo
            //    {
            //        IP = random.NextDouble().ToString(),
            //        OS = "windows 11",
            //        UserName = "Yato",
            //        AV = "Windows Defender"
            //    });
            //}
        }

        private void userConnect_Click(object sender, RoutedEventArgs e)
        {
            UserInfo userInfo = (UserInfo)userDataGrid.SelectedItem;
            SentMessage.Show($"Connect to {userInfo.IP} ?");
            //MessageBox.Show($"Connect to {userInfo.IP} ?");
        }

        private void fileExplorer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void userTools_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer SV = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                SV.LineUp();
            }
            else
            {  
                SV.LineDown();
            }
            e.Handled = true;
        }
    }
}
