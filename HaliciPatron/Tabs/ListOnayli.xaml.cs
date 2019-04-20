using System;
using System.Collections.Generic;
using System.Linq;
using HaliciPatron.Helper;
using HaliciPatron.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaliciPatron.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOnayli : ContentPage
    {
        private readonly FirebaseHelper helper;
        private List<Order> orders;

        public ListOnayli()
        {
            InitializeComponent();
            helper = new FirebaseHelper();

            BindingContext = this;

            IsBusy = false;
        }

        private async void LoadData()
        {
            IsBusy = true;
            lstCustomers.ItemsSource = null;
            orders = await helper.GetAcceptedOrders();
            lstCustomers.ItemsSource = orders;
            IsBusy = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadData();

            lstCustomers.RefreshCommand = new Command(() =>
            {
                LoadData();
                lstCustomers.IsRefreshing = false;
            });
        }

        private void LstCustomers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lstCustomers.SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstCustomers.ItemsSource = string.IsNullOrEmpty(e.NewTextValue)
                ? orders
                : orders.Where(x =>
                    x.CustomerName.ToLower().Contains(e.NewTextValue.ToLower()) ||
                    x.Phone.Contains(e.NewTextValue.ToLower()) ||
                    x.Adress.ToLower().Contains(e.NewTextValue.ToLower()));
        }

        private async void BtnSil_OnClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
                try
                {
                    var order = menuitem.BindingContext as Order;

                    await helper.DeleteOrder(order.Key);

                    LoadData();

                    await DisplayAlert("Bilgi", $"{order.CustomerName} Silindi", "Kapat");
                }
                catch (Exception err)
                {
                    await DisplayAlert("Bilgi", err.Message, "Kapat");
                }
        }

        private async void BtnAddDagitim_OnClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
                try
                {
                    var order = menuitem.BindingContext as Order;
                    if (order != null)
                    {
                        order.Durumu = "KARGO";

                        await helper.UpdateOrder(order.Key, order);

                        LoadData();

                        await DisplayAlert("Bilgi", $"{order.CustomerName} Dağıtıma Aktarıldı", "Kapat");
                    }
                }
                catch (Exception err)
                {
                    await DisplayAlert("Bilgi", err.Message, "Kapat");
                }
        }
    }
}