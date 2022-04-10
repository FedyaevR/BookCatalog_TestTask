using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;
using BookCatalog.Models;

namespace BookCatalog.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User User;
        public ICommand RegisterCommand { get; protected set; }
        public ICommand CancelCommand { get;protected set; }
        public INavigation Navigation { get; set; }

        public RegistrationViewModel()
        {
            User = new User();
            RegisterCommand = new Command(Register);
            CancelCommand = new Command(Cancel);
            
        }
        public string Login
        {
            get { return User.Login; }
            set
            {
                if (User.Login != value)
                {
                    User.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Password
        {
            get { return User.Password; }
            set
            {
                if (User.Password != value)
                {
                    User.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        private async void Register()
        {
            await App .UserRepository.AddNewUserAsync(User.Login, User.Password);
            await Navigation .PopAsync();
        }
        private async void Cancel()
        {
            await Navigation.PopAsync();
        }
        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
