using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYZEve05MVC.Models
{
    public class Order
    {
        public int OrderNo { set; get; }
        public string UserID { set; get; }
        public string OrderDate { set; get; }
        public int TotalQty { set; get; }
        public double TotalCost { set; get; }
    }
}