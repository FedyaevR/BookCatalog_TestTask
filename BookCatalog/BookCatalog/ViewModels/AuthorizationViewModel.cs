using BookCatalog.Models;
using BookCatalog.View;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookCatalog.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User User;

        public ICommand EnterCommand { get; protected set; }
        public ICommand RegisterCommand { get; protected set; }

        public INavigation Navigation { get; set; }
        public AuthorizationViewModel()
        {
            User = new User();
            EnterCommand = new Command(LoginUser);
            RegisterCommand = new Command(Register);
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
        //??
        public bool IsValid
        {
            get
            {
                return ((!String.IsNullOrEmpty(Login.Trim())) ||
                    (!String.IsNullOrEmpty(Password.Trim())));
            }
        }
        public  void LoginUser()
        {
            if (App.UserRepository.LoginUser(User.Login, User.Password) == true)
            {
                 Navigation.PushAsync(new UserPage());
            }
        }
        public  void Register()
        {
             Navigation.PushAsync(new RegistrationPage());
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
