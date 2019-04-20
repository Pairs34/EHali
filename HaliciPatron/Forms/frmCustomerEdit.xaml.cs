using System;
using HaliciPatron.Helper;
using HaliciPatron.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaliciPatron.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmCustomerEdit : ContentPage
    {
        public Customer eCustomer;
        private readonly FirebaseHelper helper;

        public frmCustomerEdit(Customer _customer)
        {
            eCustomer = _customer;
            InitializeComponent();
            helper = new FirebaseHelper();
            TxtCustomerName.Text = eCustomer.CustomerName;
            TxtAdress.Text = eCustomer.Adress;
            TxtPhone.Text = eCustomer.Phone;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                await helper.UpdateCustomer(eCustomer.Key, new Customer
                {
                    CustomerName = TxtCustomerName.Text,
                    Adress = TxtAdress.Text,
                    Phone = TxtPhone.Text
                });

                await Navigation.PopAsync();
            }
            catch (Exception exception)
            {
                await DisplayAlert("Bilgi", exception.Message, "Kapat");
            }
        }
    }
}