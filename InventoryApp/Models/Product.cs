using System;

namespace InventoryApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Expired { get; set; }


        //public string Key { get; set; }
        //public string Task { get; set; }
        //public DateTime Deadline { get; set; }
        //public string MoreDetails { get; set; }
        //public bool IsComplete { get; set; }



    }
}