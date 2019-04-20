using System;

namespace HaliciPatron.Model
{
    public class Order : Customer
    {
        public int? Adet { get; set; }
        public int? MKare { get; set; }
        public double? Fiyat { get; set; }
        public DateTime? MusteridenAlisTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public string Durumu { get; set; }
        public string Description { get; set; }
    }
}