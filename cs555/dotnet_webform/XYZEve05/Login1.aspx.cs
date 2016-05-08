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
using System.Data.SqlClient;

public partial class Login1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        string uid = txtName.Text;
        string pwd = txtPwd.Text;
        string accessLevel = CheckUser(uid, pwd);
        if (accessLevel != "")
        {
            lblMsg.Text = "Login OK ";
            Session["UNAME"] = accessLevel;
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Invalid login for Requested Page";
        }
    }

    private string CheckUser(string uid, string pwd)
    {
         string sql = "select UserName from Users where Username='" +
            uid + "' and Password='" + pwd + "'";
        Object obj = DBFunctions.GetScalarDB(sql);
        if (obj != null)
            return obj.ToString();  // Username column
        else
            return "";  // means user not found
     }
  

}
