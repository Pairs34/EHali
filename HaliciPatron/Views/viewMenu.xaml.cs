using HaliciPatron.Views.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaliciPatron.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewMenu : ContentPage
    {
        public viewMenu()
        {
            InitializeComponent();
            Title = "Halıcım";
            BindingContext = new MenuViewModel();
        }
    }
}