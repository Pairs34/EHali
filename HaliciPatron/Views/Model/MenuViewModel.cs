using System.Collections.ObjectModel;
using System.ComponentModel;
using HaliciPatron.Views.Data;

namespace HaliciPatron.Views.Model
{
    internal class MenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenuModel> _menu;

        public MenuViewModel()
        {
            Menu = MenuDataBinding.BindMenu();
        }

        public ObservableCollection<MenuModel> Menu
        {
            get => _menu;

            set => _menu = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}