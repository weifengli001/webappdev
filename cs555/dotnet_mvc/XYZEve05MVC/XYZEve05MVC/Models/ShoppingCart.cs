using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace XYZEve05MVC.Models
{
    public class ShoppingCart : ICloneable 
    {
        public int TotalQty { set; get; }
        public double TotalCost { set; get; }
        public ArrayList list = new ArrayList(20);  // maximum of 20 items

        public List<CartItem> items = new List<CartItem>(20);

        public object Clone()   // needed in implementing shopping
                                // "Cancel Last Change" functionality.
        {
            ShoppingCart clone = new ShoppingCart();
            clone.list = (ArrayList)this.list.Clone();
            return clone;
        }
    }
}