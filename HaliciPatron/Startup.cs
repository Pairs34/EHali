using System;
using HaliciPatron.Views;
using HaliciPatron.Views.Model;
using Xamarin.Forms;

namespace HaliciPatron
{
    internal class Startup : MasterDetailPage
    {
        private readonly viewMenu menuPage;

        public Startup()
        {
            MasterBehavior = MasterBehavior.Popover;

            var detailPage = new MainPage(null);

            menuPage = new viewMenu();

            Master = menuPage;
            Detail = new NavigationPage(detailPage);

            menuPage.lstMenu.ItemSelected += LstMenu_ItemSelected;
        }

        private void LstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuModel;
            if (item != null)
            {
                var page = (Page) Activator.CreateInstance(item.TargetType);
                page.Title = item.Name;
                Detail = new NavigationPage(page);
                menuPage.lstMenu.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}