using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public interface IBusinessShopping
    {
        List<Product> getProductsByCatID(int CatID);
        Product getPoductByProductId(int ProductId);
    }