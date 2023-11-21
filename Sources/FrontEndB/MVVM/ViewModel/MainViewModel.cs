using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UserPanel.FrontEndB.MVVM.Core;
using UserPanel.User_Data_and_Structures;

namespace UserPanel.FrontEndB.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand ToolsViewCommand { get; set; }
        public RelayCommand ChangeAvatarCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public ToolsViewModel ToolsVM { get; set; }

        private object? _currentView;
        private object? _currentAvatar;

        public object? CurrentView 
        { 
            get { return _currentView; } 
            set { _currentView = value; OnPropertyChanged("CurrentView"); } 
        }
        public object? CurrentAvatar
        {
            get { return _currentAvatar; }
            set { _currentAvatar = value; OnPropertyChanged("CurrentAvatar"); }
        }  

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            ToolsVM = new ToolsViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            if (UserData.AvatarPath == string.Empty)
            {
                CurrentAvatar = Properties.Resources.avatar;
            }
            else if (File.Exists(UserData.AvatarPath)) { CurrentAvatar = UserData.AvatarPath; }
            else { CurrentAvatar = Properties.Resources.avatar; }

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            ToolsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ToolsVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            ChangeAvatarCommand = new RelayCommand(o =>
            {
                CurrentAvatar = UserData.AvatarPath;
            });
        }
    }
}
