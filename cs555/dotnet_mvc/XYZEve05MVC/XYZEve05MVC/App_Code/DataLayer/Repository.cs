using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class Repository : IRepository
{
    public Product getPoductByProductId(int ProductId)
    {
        Product product = new Product();
        try
        {
            string sql = "select * from Products where ProductId=" + ProductId;
            DataSet ds = DBFunctions.GetDataSetDB(sql);
            product = RepositoryHelper.ConvertDataSetToList(ds).First();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return product;
    }

    public List<Product> getProductsByCatID(int CatID)
    {
        List<Product> TList = null;
        try
        {
            string sql = "SELECT * FROM  Products WHERE CATID=" + CatID;
            DataSet ds = DBFunctions.GetDataSetDB(sql);
            TList = RepositoryHelper.ConvertDataSetToList(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        };
        return TList;

    }



}