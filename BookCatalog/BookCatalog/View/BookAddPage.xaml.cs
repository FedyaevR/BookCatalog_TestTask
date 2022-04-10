using BookCatalog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookAddPage : ContentPage
    {
        public BookAddPage()
        {
            InitializeComponent();
            BindingContext = new BookAddViewModel() { Navigation = this.Navigation };
        }

     
    }
}