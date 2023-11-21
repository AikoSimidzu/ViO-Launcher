namespace UserPanel.Panels
{
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.Win32;
    using UserPanel.Classes;
    using UserPanel.User_Data_and_Structures;

    /// <summary>
    /// Логика взаимодействия для SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        public SettingsPanel()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string expDate = Server.CheckLicense();
            string dateResult = string.Empty;
            DateTime serverExpDate = DateTime.MinValue;
            DateTime serverTime = Server.getServerTime();

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (expDate.ToLower() == "true") 
            { 
                dateResult = "LifeTime";
                serverExpDate = DateTime.MaxValue; 
            } 
            else if (expDate.ToLower() == "false") 
            { 
                dateResult = "1.07.1970"; 
            } 
            else if (dateTime.AddSeconds(double.Parse(expDate)) >= serverTime) 
            { 
                serverExpDate = dateTime.AddSeconds(double.Parse(expDate));
                dateResult = dateTime.AddSeconds(double.Parse(expDate)).ToString(); 
            } 
            else { dateResult = "1.07.1970"; }

            licenseDate.Text = "License expired: " + dateResult;            

            if (serverExpDate >= serverTime)
            {
                licenseIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                UserData.License = true;
            }
            else
            {                
                licenseIcon.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                UserData.License = false;
            }
            avatarPath.Text = UserData.AvatarPath;
        }

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG files(*.png)|*.png|JPG files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (ofd.ShowDialog().Value)
            {
                avatarPath.Text = ofd.FileName;
                UserData.AvatarPath = ofd.FileName;
            }
            UserData.SaveData();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            avatarPath.Text = string.Empty;
            UserData.AvatarPath = string.Empty;
            UserData.SaveData();
        }

        private void activateLKey_Click(object sender, RoutedEventArgs e)
        {
            if (Server.ActivateLicense(licenseKey.Text))
            {
                MessageBox.Show("You have activated the key!");
            }
            {
                MessageBox.Show("Failed to activate key. Perhaps you already have a subscription or the key is entered incorrectly.");
            }
        }        
    }
}
