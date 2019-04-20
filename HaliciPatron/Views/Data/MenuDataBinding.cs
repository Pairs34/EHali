using System.Collections.ObjectModel;
using HaliciPatron.Tabs;
using HaliciPatron.Views.Model;

namespace HaliciPatron.Views.Data
{
    internal class MenuDataBinding
    {
        public static ObservableCollection<MenuModel> BindMenu()
        {
            return new ObservableCollection<MenuModel>
            {
                new MenuModel {Icon = "working.png", Name = "Alınan Siparişler", TargetType = typeof(ListOnayli)},
                new MenuModel {Icon = "waiting.png", Name = "Bekleyen Siparişler", TargetType = typeof(ListOnaysiz)},
                new MenuModel {Icon = "cargo.png", Name = "Dağıtım Listesi", TargetType = typeof(ListDagitim)},
                new MenuModel {Icon = "done.png", Name = "Teslim Edilenler", TargetType = typeof(ListAllPage)},
                new MenuModel {Icon = "add_customer.png", Name = "Kayıt Ekle", TargetType = typeof(MainPage)},
                new MenuModel {Icon = "customer.png", Name = "Müşteriler", TargetType = typeof(ListCustomer)}
                //new MenuModel {Icon = "money.png", Name = "Maliyet" , TargetType = typeof(frmCalculate)}
            };
        }
    }
}