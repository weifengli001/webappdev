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

public partial class Admin_ViewProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UNAME"] != null)
        {
            string user = (string)Session["UNAME"];
            if (user != "admin")
                Response.Redirect("../Login1.aspx");

        }
        else
            Response.Redirect("../Login1.aspx");

 
        if (!IsPostBack)
            BindToGrid();
    }

    private void BindToGrid()
    {
        try
        {
            string sql = "SELECT * FROM  Products";
            DataSet ds = DBFunctions.GetDataSetDB(sql);
            ds.Tables[0].Columns.Remove("CatID");
            gv1.DataSource = ds.Tables[0];
            DataBind();
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }
    protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv1.PageIndex = e.NewPageIndex;
        BindToGrid();
    }
    protected void btnLogoff_Click(object sender, EventArgs e)
    {
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("http://localhost/xyzeve05/default.aspx");
    }
}
