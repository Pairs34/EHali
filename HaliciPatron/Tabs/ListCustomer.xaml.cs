using System;
using System.Collections.Generic;
using System.Linq;
using HaliciPatron.Forms;
using HaliciPatron.Helper;
using HaliciPatron.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaliciPatron.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListCustomer : ContentPage
    {
        private List<Customer> customers;
        private readonly FirebaseHelper helper;

        public ListCustomer()
        {
            InitializeComponent();
            helper = new FirebaseHelper();
            BindingContext = this;

            IsBusy = false;
        }

        private async void LoadData()
        {
            IsBusy = true;
            LstCustomers.ItemsSource = null;
            customers = await helper.GetCustomers();
            LstCustomers.ItemsSource = customers;
            IsBusy = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private async void BtnEdit_OnClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var customer = menuitem.BindingContext as Customer;
                var frmEdit = new frmCustomerEdit(customer);

                frmEdit.Disappearing += (s, o) =>
                {
                    DisplayAlert("Bilgi", "Güncellendi", "Kapat");

                    LoadData();
                };

                await Navigation.PushAsync(frmEdit, true);
            }
        }

        private async void BtnCreateOrder_OnClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var customer = menuitem.BindingContext as Customer;
                var frmCreateOrder = new MainPage(customer);

                await Navigation.PushAsync(frmCreateOrder, true);

                frmCreateOrder.Disappearing += (s, o) => { LoadData(); };
            }
        }

        private async void BtnDelCustomer_OnClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
                try
                {
                    var customer = menuitem.BindingContext as Customer;

                    await helper.DeleteCustomer(customer.Key);

                    LoadData();

                    await DisplayAlert("Bilgi", $"{customer.CustomerName} Silindi", "Kapat");
                }
                catch (Exception err)
                {
                    await DisplayAlert("Bilgi", err.Message, "Kapat");
                }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            LstCustomers.ItemsSource = string.IsNullOrEmpty(e.NewTextValue)
                ? customers
                : customers.Where(x =>
                    x.CustomerName.ToLower().Contains(e.NewTextValue.ToLower()) ||
                    x.Phone.Contains(e.NewTextValue.ToLower()) ||
                    x.Adress.ToLower().Contains(e.NewTextValue.ToLower()));
        }
    }
}