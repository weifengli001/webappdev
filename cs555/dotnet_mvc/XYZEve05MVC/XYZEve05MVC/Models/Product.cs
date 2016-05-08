using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Product
{
    public Product()
    {

    }

    public int ProductId { set; get; }
    public int CatID { set; get; }
    public string ProductSDesc { set; get; }
    public string ProdectLDesc { set; get; }
    public string ProductImage { set; get; }
    public decimal Price { set; get; }
    public Boolean Instock { set; get; }
    public int Inventory { set; get; }


    public void SetFields(System.Data.DataRow dr)
    {
        this.ProductId = (int)dr["ProductId"];
        this.CatID = (int)dr["CatID"];
        this.ProductSDesc = (string)dr["ProductSDesc"];
        this.ProdectLDesc = (string)dr["ProductLDesc"];
        this.ProductImage = (string)dr["ProductImage"];
        this.Price = (decimal)dr["Price"];
        this.Instock = (Boolean)dr["Instock"];
        this.Inventory = (int)dr["Inventory"];
    }
}
