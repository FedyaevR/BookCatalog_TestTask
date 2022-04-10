using BookCatalog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel() { Navigation = this.Navigation};
        }
    }
}