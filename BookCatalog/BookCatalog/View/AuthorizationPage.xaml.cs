using BookCatalog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            BindingContext = new AuthorizationViewModel() { Navigation = this.Navigation };
            
        }
    }
}