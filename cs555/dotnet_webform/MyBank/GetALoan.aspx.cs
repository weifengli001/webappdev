using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetALoan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionFacade.USERNAME == null)
        {
            SessionFacade.PAGEREQUESTED = Request.ServerVariables["SCRIPT_NAME"];
            Response.Redirect("Login.aspx");
        }

        IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
        string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
        string savAcctNum = chkAcctNum + "1";
        lblCheckingBalance.Text = iba.GetCheckingBalance(chkAcctNum).ToString();
        lblSavingBalance.Text = iba.GetSavingBalance(savAcctNum).ToString();
    }
    protected void btnGetALoan_Click(object sender, EventArgs e)
    {
        IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
        try
        {
            string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
            string savAcctNum = chkAcctNum + "1";
            double loanAmount = double.Parse(txtAmount.Text);
            double chkBalance = iba.GetCheckingBalance(chkAcctNum);
            double savBalance = iba.GetSavingBalance(savAcctNum);
            Stretegy stretegy = Stretegy.testLoan(chkBalance, savBalance, loanAmount);
            lblStatus.Text = stretegy.msg;
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }
}