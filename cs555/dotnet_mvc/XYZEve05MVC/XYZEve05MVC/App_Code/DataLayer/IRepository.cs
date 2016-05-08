using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XYZEve05MVC.Models;

    public interface IRepository
    {
        List<Product> getProductsByCatID(int CatID);
        Product getPoductByProductId(int ProductId);
    }