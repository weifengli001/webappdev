using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XYZEve05MVC.Models;

/// <summary>
/// Summary description for SessionFacade
/// </summary>
public class SessionFacade
{   // Facade is often implemented as a Singleton
    // but here due to static fields, we do not need Singleton
    //static readonly SessionFacade instance = new SessionFacade();
    
    //SessionFacade()
    //{
    //}
    //public static SessionFacade Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //}

    static readonly string _USERNAME = "USERNAME";
    public static string USERNAME
    {
        get
        {
            string res = null;
            if (HttpContext.Current.Session[_USERNAME] != null)
                res = (string)HttpContext.Current.Session[_USERNAME];
            return res;
        }
        set
        {
            HttpContext.Current.Session[_USERNAME] = value;
        }
    }

    static readonly string _PrinterFriendly = "PrinterFriendly";
    public static string PrinterFriendly
    {
        get
        {
            string res = null;
            if (HttpContext.Current.Session[_PrinterFriendly] != null)
                res = (string)HttpContext.Current.Session[_PrinterFriendly];
            return res;
        }
        set
        {
            HttpContext.Current.Session[_PrinterFriendly] = value;
        }
    }

    static readonly string _PAGEREQUESTED = "PAGEREQUESTED";
    public static string PAGEREQUESTED
    {
        get
        {
            string res = null;
            if (HttpContext.Current.Session[_PAGEREQUESTED] != null)
                res = (string)HttpContext.Current.Session[_PAGEREQUESTED];
            return res;
        }
        set
        {
            HttpContext.Current.Session[_PAGEREQUESTED] = value;
        }
    }

    static readonly string _CATID = "CATID";
    public static string CATID
    {
        get
        {
            string res = null;
            if (HttpContext.Current.Session[_CATID] != null)
                res = (string)HttpContext.Current.Session[_CATID];
            return res;
        }
        set
        {
            HttpContext.Current.Session[_CATID] = value;
        }
    }

    static readonly string _MYCART = "MYCART";
    public static ShoppingCart MyCart
    {
        get
        {
            ShoppingCart ret = null;
            if (HttpContext.Current.Session[_MYCART] != null)
                ret = (ShoppingCart)HttpContext.Current.Session[_MYCART];
            return ret;
        }
        set
        {
            HttpContext.Current.Session[_MYCART] = value;
        }
    }

    public static void Logout()
    {
        HttpContext.Current.Session.Abandon();
    }
}