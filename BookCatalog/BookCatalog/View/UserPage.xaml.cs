using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookCatalog.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : TabbedPage
    {
        public UserPage()
        {
            InitializeComponent();
            this.Children.Add(new BookListPage());
            this.Children.Add(new BookAddPage());
            
           
        }
    }
}