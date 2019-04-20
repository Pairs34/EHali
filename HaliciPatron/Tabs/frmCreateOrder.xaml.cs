using System;
using HaliciPatron.Helper;
using HaliciPatron.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaliciPatron.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmCreateOrder : ContentPage
    {
        public frmCreateOrder(Order nOrder)
        {
            InitializeComponent();
            Title = "Sipariş Kayıt";
            _order = nOrder;
            txtCustomerName.Text = nOrder.CustomerName;
            txtAdress.Text = nOrder.Adress;
            txtPhone.Text = nOrder.Phone;
        }

        public Order _order { get; set; }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomerName.Text) ||
                    string.IsNullOrEmpty(txtAdress.Text) ||
                    string.IsNullOrEmpty(txtFiyat.Text) ||
                    string.IsNullOrEmpty(txtMKare.Text) ||
                    string.IsNullOrEmpty(txtAdet.Text))
                {
                    await DisplayAlert("Bilgi", "Boş alanları doldurunuz", "Kapat");
                    return;
                }

                var helper = new FirebaseHelper();

                try
                {
                    if (_order.Key != null || string.IsNullOrEmpty(_order.Key))
                        await helper.UpdateOrder(_order.Key,
                            new Order
                            {
                                CustomerName = txtCustomerName.Text,
                                Adress = txtAdress.Text,
                                Phone = txtPhone.Text,
                                Fiyat = Convert.ToDouble(txtFiyat.Text),
                                Adet = Convert.ToInt32(txtAdet.Text),
                                Description = txtAciklama.Text,
                                MKare = int.Parse(txtMKare.Text),
                                MusteridenAlisTarihi = _order.MusteridenAlisTarihi,
                                TeslimTarihi = null,
                                Durumu = "ONAYLI"
                            });
                }
                catch (Exception err)
                {
                    await DisplayAlert("Bilgi # Create Order", err.Message, "Kapat");
                }

                await Navigation.PopAsync(true);
            }
            catch (Exception exception)
            {
                await DisplayAlert("Bilgi # Order Record", exception.Message, "");
            }
        }
    }
}