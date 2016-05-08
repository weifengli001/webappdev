using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYZEve05MVC.Models
{
    public class CartItem
    {
        public int ProductId { set; get; }
        public string ProductSDesc { set; get; }
        public double Price { set; get; }
        public int Quantity {
            set;
            get; }
        public double TotalAmount { set; get; }
    }
}