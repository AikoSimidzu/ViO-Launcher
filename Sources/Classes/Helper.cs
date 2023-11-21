using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;

namespace UserPanel.Classes
{
    class Helper
    {
        public static string? GetHWID()
        {
            string? str = string.Empty;
            try
            {
                string str2 = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                using (ManagementObject obj1 = new ManagementObject("win32_logicaldisk.deviceid=\"" + str2 + ":\""))
                {
                    obj1.Get();
                    str = obj1["VolumeSerialNumber"]?.ToString();
                }
            }
            catch { }
            return str;
        }

        public static string? pwdEncrypt(string password) 
        {
            char[] key = GetHWID().ToCharArray();
            char[] output = new char[password.Length];

            for (int i = 0; i < password.Length; i++)
            {
                output[i] = (char)(password[i] ^ key[i % key.Length]);
            }
            return new string(Convert.ToBase64String(Encoding.UTF8.GetBytes(output)));
        }
    }
}
