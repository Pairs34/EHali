using Xamarin.Forms;

namespace HaliciPatron.Tabs
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            Children.Add(new MainPage(null)
            {
                Title = "Kayıt Ekle"
            });
            Children.Add(new NavigationPage(new ListCustomer())
            {
                Title = "Müşteriler"
            });
            Children.Add(new ListOnayli
            {
                Title = "Teslim Alınan"
            });
            Children.Add(new ListOnaysiz
            {
                Title = "Bekleyen"
            });
            Children.Add(
                new ListAllPage
                {
                    Title = "Tümü"
                }
            );
        }
    }
}