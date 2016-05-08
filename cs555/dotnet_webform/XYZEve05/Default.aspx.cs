using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    void Page_PreInit(object sender, EventArgs e)
    {
        // programmatically set Master page
        if (Session["PrinterFriendly"] != null)
        {
            this.MasterPageFile = "MasterPage.master";
            //Session["PrinterFriendly"] = null;
        }
    }
    protected void lnkPrinterFriendly_Click(object sender, EventArgs e)
    {
        Session["PrinterFriendly"] = 1;
        Response.Redirect("Default.aspx");
    }
}
