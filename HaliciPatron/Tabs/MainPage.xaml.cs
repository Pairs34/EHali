using System;
using System.ComponentModel;
using System.Linq;
using HaliciPatron.Helper;
using HaliciPatron.Model;
using Xamarin.Forms;

namespace HaliciPatron
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private readonly Customer _customer;

        public MainPage()
        {
            InitializeComponent();
            Title = "Kayıt Ekle";
        }

        public MainPage(Customer nCustomer)
        {
            InitializeComponent();
            Title = "Kayıt Ekle";
            if (nCustomer != null)
            {
                _customer = nCustomer;
                txtCustomerName.Text = _customer.CustomerName;
                txtAdress.Text = _customer.Adress;
                txtPhone.Text = _customer.Phone;
            }
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text) ||
                string.IsNullOrEmpty(txtAdress.Text))
            {
                await DisplayAlert("Bilgi", "Boş alanları doldurunuz", "Kapat");
                return;
            }

            var helper = new FirebaseHelper();
            var order = await helper.AddOrder(new Order
            {
                CustomerName = txtCustomerName.Text,
                Adress = txtAdress.Text,
                Phone = txtPhone.Text,
                Fiyat = null,
                MKare = null,
                Adet = null,
                Description = "",
                MusteridenAlisTarihi = null,
                TeslimTarihi = null,
                Durumu = "ONAYSIZ"
            });

            var IsExist =
                (await helper.GetClient().Child("Customers").OnceAsync<Customer>()).Select(x =>
                    x.Object.Phone.Contains(order.Object.Phone)).FirstOrDefault();
            if (!IsExist)
            {
                var customer = await helper.AddCustomer(new Customer
                {
                    CustomerName = txtCustomerName.Text,
                    Adress = txtAdress.Text,
                    Phone = txtPhone.Text
                });
            }

            if (order.Object != null)
                await DisplayAlert("Bilgi", $"Veri Kaydedildi # {order.Object.CustomerName}", "Kapat");
            else
                await DisplayAlert("Bilgi", "Veri Kaydedilemedi", "Kapat");

            Clear();
            await Navigation.PopAsync(true);
        }

        private void Clear()
        {
            txtAdress.Text = "";
            txtCustomerName.Text = "";
            txtPhone.Text = "";
        }
    }
}