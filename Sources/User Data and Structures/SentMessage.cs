using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPanel.User_Data_and_Structures
{
    class SentMessage
    {
        public static string? MessageText { get; private set; }
        public static bool? MessageAnswer { get; set; }
        public static void Show(string Message)
        {
            MessageText = Message;
            Panels.MessageMenu.Message msg = new();
            msg.Cancle.Visibility = System.Windows.Visibility.Hidden;
            msg.ShowDialog();
        }

        public static bool? ShowA(string Message)
        {
            MessageText = Message;
            Panels.MessageMenu.Message msg = new();
            msg.ShowDialog();
            return MessageAnswer;
        }
    }
}
