using System;

namespace BackNice.Models
{
    public class SupplierModel
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DataCreate { get; set; }
        public DateTime DataEdit { get; set; }
    }
}