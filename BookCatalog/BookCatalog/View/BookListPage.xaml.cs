using BookCatalog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookListPage : ContentPage
    {
        public BookListPage()
        {
            InitializeComponent();
            BindingContext = new BookListViewModel() { Navigation = this.Navigation };
        }
    }
}