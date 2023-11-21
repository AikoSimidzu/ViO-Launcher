namespace UserPanel.User_Data_and_Structures
{
    using System;
    using System.IO;
    using UserPanel.FrontEndB.MVVM.Core;

    class UserData : ObservableObject
    {
        public static string Login { get; set; } = string.Empty;
        public static string Password { get;set;} = string.Empty;
        public static bool License { get;set;}
        public static string AvatarPath { get; set; } = string.Empty;

        private readonly static string UserDataPath = Path.Combine(Environment.CurrentDirectory, "UserData");
        public static void SaveData()
        {
            string udFormat = $"{Login};{Password};{AvatarPath}";
            File.WriteAllText(UserDataPath, udFormat);
        }
        public static bool LoadData()
        {
            if (File.Exists(UserDataPath))
            {
                string uData = File.ReadAllText(UserDataPath);
                Login = uData.Split(';')[0];
                Password = uData.Split(";")[1];
                AvatarPath = uData.Split(";")[2];
                return true;
            }
            else { return false; }
        }
    }
}
