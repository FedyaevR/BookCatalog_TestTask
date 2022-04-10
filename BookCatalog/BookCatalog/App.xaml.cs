using BookCatalog.Data;
using BookCatalog.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog
{
    public partial class App : Application
    {
        string dbPath_User => FileAccesHelper.GetLocalFilePath("user.db3");
        string dbPath_Book => FileAccesHelper.GetLocalFilePath("book.db3");
        public static UserRepository UserRepository { get; private set; }
        public static BookRepository BookRepository { get; private set; }
        public App()
        {
            InitializeComponent();

            UserRepository = new UserRepository(dbPath_User);
            BookRepository = new BookRepository(dbPath_Book);
            MainPage = new NavigationPage(new AuthorizationPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
