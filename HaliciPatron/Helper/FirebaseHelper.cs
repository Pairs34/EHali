using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using HaliciPatron.Model;

namespace HaliciPatron.Helper
{
    internal class FirebaseHelper
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://extsoft-c28bf.firebaseio.com/");

        public FirebaseClient GetClient()
        {
            return firebase;
        }

        public async Task<FirebaseObject<Customer>> AddCustomer(Customer nCustomer)
        {
            var nRecord = await firebase.Child("Customers").PostAsync(nCustomer);

            return nRecord;
        }

        public async Task<FirebaseObject<Order>> AddOrder(Order nOrder)
        {
            var nRecord = await firebase.Child("Orders").PostAsync(nOrder);

            return nRecord;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return (await firebase.Child("Customers").OnceAsync<Customer>()).Select(item =>
                    new Customer
                    {
                        Key = item.Key,
                        CustomerName = item.Object.CustomerName,
                        Adress = item.Object.Adress,
                        Phone = item.Object.Phone
                    })
                .ToList();
        }

        public async Task<List<Order>> GetOrders()
        {
            return (await firebase.Child("Orders").OnceAsync<Order>())
                .Where(x => x.Object.Durumu == "TESLIMEDILDI")
                .Select(item =>
                    new Order
                    {
                        Key = item.Key,
                        CustomerName = item.Object.CustomerName,
                        Adress = item.Object.Adress,
                        Durumu = item.Object.Durumu,
                        Fiyat = item.Object.Fiyat,
                        MKare = item.Object.MKare,
                        Description = item.Object.Description,
                        MusteridenAlisTarihi = item.Object.MusteridenAlisTarihi,
                        TeslimTarihi = item.Object.TeslimTarihi,
                        Phone = item.Object.Phone
                    })
                .ToList();
        }

        public async Task<List<Order>> GetAcceptedOrders()
        {
            return (await firebase.Child("Orders").OnceAsync<Order>()).Where(x => x.Object.Durumu == "ONAYLI")
                .Select(item => new Order
                {
                    Key = item.Key,
                    CustomerName = item.Object.CustomerName,
                    Adress = item.Object.Adress,
                    Durumu = item.Object.Durumu,
                    Fiyat = item.Object.Fiyat,
                    MKare = item.Object.MKare,
                    Description = item.Object.Description,
                    MusteridenAlisTarihi = item.Object.MusteridenAlisTarihi,
                    Phone = item.Object.Phone
                })
                .ToList();
        }

        public async Task<List<Order>> GetCargoOrders()
        {
            return (await firebase.Child("Orders").OnceAsync<Order>()).Where(x => x.Object.Durumu == "KARGO")
                .Select(item => new Order
                {
                    Key = item.Key,
                    CustomerName = item.Object.CustomerName,
                    Adress = item.Object.Adress,
                    Durumu = item.Object.Durumu,
                    Fiyat = item.Object.Fiyat,
                    MKare = item.Object.MKare,
                    Description = item.Object.Description,
                    MusteridenAlisTarihi = item.Object.MusteridenAlisTarihi,
                    Phone = item.Object.Phone
                })
                .ToList();
        }

        public async Task<List<Order>> GetWaitingOrders()
        {
            return (await firebase.Child("Orders").OnceAsync<Order>()).Where(x => x.Object.Durumu == "ONAYSIZ")
                .Select(item => new Order
                {
                    Key = item.Key,
                    CustomerName = item.Object.CustomerName,
                    Adress = item.Object.Adress,
                    Durumu = item.Object.Durumu,
                    Fiyat = item.Object.Fiyat,
                    MKare = item.Object.MKare,
                    Phone = item.Object.Phone
                })
                .ToList();
        }

        public async Task UpdateOrder(string nKey, Order nOrder)
        {
            var toUpdateOrder = (await firebase.Child("Orders").OnceAsync<Order>())
                .FirstOrDefault(a => a.Key == nKey);

            if (toUpdateOrder != null)
                await firebase.Child("Orders")
                    .Child(toUpdateOrder.Key)
                    .PutAsync(new Order
                    {
                        CustomerName = nOrder.CustomerName,
                        Adress = nOrder.Adress,
                        Phone = nOrder.Phone,
                        Fiyat = nOrder.Fiyat,
                        MKare = nOrder.MKare,
                        Adet = nOrder.Adet,
                        Description = nOrder.Description,
                        MusteridenAlisTarihi = nOrder.MusteridenAlisTarihi,
                        TeslimTarihi = nOrder.TeslimTarihi,
                        Durumu = nOrder.Durumu
                    });
        }

        public async Task UpdateCustomer(string nKey, Customer nCustomer)
        {
            var toUpdateCustomer = (await firebase.Child("Customers").OnceAsync<Customer>())
                .FirstOrDefault(a => a.Key == nKey);

            if (toUpdateCustomer != null)
                await firebase.Child("Customers")
                    .Child(toUpdateCustomer.Key)
                    .PutAsync(new Customer
                    {
                        CustomerName = nCustomer.CustomerName,
                        Adress = nCustomer.Adress,
                        Phone = nCustomer.Phone
                    });
        }

        public async Task DeleteOrder(string nKey)
        {
            var toDeleteOrder = (await firebase.Child("Orders").OnceAsync<Order>())
                .FirstOrDefault(a => a.Key == nKey);

            if (toDeleteOrder != null)
                await firebase.Child("Orders")
                    .Child(toDeleteOrder.Key)
                    .DeleteAsync();
        }

        public async Task DeleteCustomer(string nKey)
        {
            var toDeleteCustomer = (await firebase.Child("Customers").OnceAsync<Order>())
                .FirstOrDefault(a => a.Key == nKey);

            if (toDeleteCustomer != null)
                await firebase.Child("Customers")
                    .Child(toDeleteCustomer.Key)
                    .DeleteAsync();
        }
    }
}