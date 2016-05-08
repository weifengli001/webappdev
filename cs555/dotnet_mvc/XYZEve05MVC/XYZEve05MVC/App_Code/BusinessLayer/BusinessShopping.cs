using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class BusinessShopping : IBusinessShopping
    {
        IRepository rp = new Repository();

    public Product getPoductByProductId(int ProductId)
    {
        return rp.getPoductByProductId(ProductId);
    }

    public List<Product> getProductsByCatID(int CatID)
        {
            return rp.getProductsByCatID(CatID);
        }
    }
