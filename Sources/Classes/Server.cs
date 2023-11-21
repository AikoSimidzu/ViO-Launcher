using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using UserPanel.User_Data_and_Structures;

namespace UserPanel.Classes
{
    class Server
    {
        static readonly IPAddress adr = IPAddress.Parse(Encoding.UTF8.GetString(Convert.FromBase64String("There IP")));
        
		//public static bool AuthV2(string Login, string Password)
        //{
        //    try
        //    {
        //        using (UdpClient auth = new UdpClient(1868))
        //        {
        //            byte[] data = Encoding.UTF8.GetBytes($"AUTH;{Login};{Password}");
        //            IPEndPoint remotePoint = new IPEndPoint(adr, 1868);
        //            auth.Send(data, remotePoint);

        //            var result = auth.Receive(ref remotePoint);
        //            return bool.Parse(Encoding.UTF8.GetString(result));
        //        }
        //    }
        //    catch(Exception ex) 
        //    {
        //        SentMessage.Show(ex.Message);
        //        return false;
        //    }
        //}
		
        public static bool Auth(string Login, string Password)
        {
            try
            {
                using (TcpClient auth = new TcpClient())
                {
                    auth.Connect(new IPEndPoint(adr, 1868));
                    NetworkStream stream = auth.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes($"AUTH;{Login};{Password};");
                    stream.Write(data, 0, data.Length);

                    var responseData = new byte[10];
                    stream.Read(responseData, 0, responseData.Length);
                    
                    return bool.Parse(Encoding.UTF8.GetString(responseData).Split(';')[0]);
                }
            }
            catch (Exception ex)
            {
                SentMessage.Show(ex.Message);
                return false;
            }
        }

        public static bool Register(string Login, string Password)
        {
            try
            {
                using (TcpClient auth = new TcpClient())
                {
                    auth.Connect(new IPEndPoint(adr, 1868));
                    NetworkStream stream = auth.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes($"REG;{Login};{Password};{Helper.GetHWID()};");
                    stream.Write(data, 0, data.Length);

                    var responseData = new byte[10];
                    stream.Read(responseData, 0, responseData.Length);
                    return bool.Parse(Encoding.UTF8.GetString(responseData).Split(';')[0]);
                }
            }
            catch (Exception ex)
            {
                SentMessage.Show(ex.Message);
                return false;
            }
        }

        public static string CheckLicense()
        {
            using (TcpClient auth = new TcpClient())
            {
                auth.Connect(new IPEndPoint(adr, 1868));
                NetworkStream stream = auth.GetStream();
                byte[] data = Encoding.UTF8.GetBytes($"ED;{UserData.Login};{Helper.GetHWID()};");
                stream.Write(data, 0, data.Length);

                var responseData = new byte[50];
                stream.Read(responseData, 0, responseData.Length);
                return Encoding.UTF8.GetString(responseData).Split(';')[0];
            }
        }

        public static bool ActivateLicense(string Key)
        {
            try
            {
                using (TcpClient auth = new TcpClient())
                {
                    auth.Connect(new IPEndPoint(adr, 1868));
                    NetworkStream stream = auth.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes($"DL;{UserData.Login};{Helper.pwdEncrypt(UserData.Password)};{Helper.GetHWID()};{Key};");
                    stream.Write(data, 0, data.Length);

                    var responseData = new byte[10];
                    stream.Read(responseData, 0, responseData.Length);
                    return bool.Parse(Encoding.UTF8.GetString(responseData).Split(';')[0]);
                }
            }
            catch (Exception ex)
            {
                SentMessage.Show(ex.Message);
                return false;
            }
        }

        public static DateTime getServerTime()
        {
            using (TcpClient auth = new TcpClient())
            {
                auth.Connect(new IPEndPoint(adr, 1868));
                NetworkStream stream = auth.GetStream();
                byte[] data = Encoding.UTF8.GetBytes("ST;");
                stream.Write(data, 0, data.Length);

                var responseData = new byte[50];
                stream.Read(responseData, 0, responseData.Length);
                
                return DateTime.MinValue.AddSeconds(double.Parse(Encoding.UTF8.GetString(responseData).Split(';')[0]));
            }
        }

        public static ObservableCollection<UserInfo> getUsers()
        {
            ObservableCollection <UserInfo> users = new();

            using (TcpClient tcClient = new())
            {
                tcClient.Connect(new IPEndPoint(adr, 1869));
                NetworkStream stream = tcClient.GetStream();
                byte[] data = Encoding.UTF8.GetBytes($"GD;{UserData.Login};");
                stream.Write(data, 0, data.Length);

                // буфер для получения данных
                var responseData = new byte[1048];
                // StringBuilder для склеивания полученных данных в одну строку
                var response = new StringBuilder();
                int bytes;  // количество полученных байтов
                do
                {
                    // получаем данные
                    bytes = stream.Read(responseData);
                    // преобразуем в строку и добавляем ее в StringBuilder
                    response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                }
                while (bytes > 0); // пока данные есть в потоке 

                users = JsonSerializer.Deserialize<ObservableCollection<UserInfo>>(response.ToString());
            }

            return users;
        }
    }
}
