using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XYZEve05MVC.Models
{
    public class OrderDetail
    {
        public Int64 OrderDetailID { set; get; }
        public int OrderNo { set; get; }
        public int ItemNo { set; get; }
        public string ItemDesc { set; get; }
        public int Qty { set; get; }
        public double Price { set; get; }
    }
}