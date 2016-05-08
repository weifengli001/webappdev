using MyBank.App_Code;
using MyBank_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace MyBank_MVC.Controllers
{
    public class TransferFundsController : Controller
    {
        // GET: TransferFunds

        public ActionResult Index(TransferFunds Tfs)
        {
            //login check 
            // authorize check 
            if (SessionFacade.USERNAME == null)
            {
                SessionFacade.PAGEREQUESTED = Request.Url.AbsolutePath;
                RedirectToAction("Login", "Home");
            }

            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
            string savAcctNum = chkAcctNum + "1";
           Tfs.ChkAcntBal = iba.GetCheckingBalance(chkAcctNum).ToString();
           Tfs.SavAcntBal =  iba.GetSavingBalance(savAcctNum).ToString();
            Tfs.ChkAcuntNmbr = Int32.Parse( chkAcctNum);
            return View(Tfs);
        }
        [HttpPost]
        public ActionResult Tfunds(TransferFunds tf)
        {

            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            try
            {
                string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
                string savAcctNum = chkAcctNum + "1";
                if (iba.TransferFromChkgToSav(chkAcctNum, savAcctNum,
                    tf.TAmount))
                {
                    tf.sTatus = "Transfer successful..";
                    tf.ChkAcntBal =iba.GetCheckingBalance(chkAcctNum).ToString();
                    tf.SavAcntBal =  iba.GetSavingBalance(savAcctNum).ToString();
                }
            }
            catch (Exception ex)
            {
              tf.sTatus = "my mistake try again !";
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult History(TransferHistoryModel tf)
        {
           List<TransferHistory> tlist = tf.TList;
            if (SessionFacade.USERNAME == null)
            {
                SessionFacade.PAGEREQUESTED = Request.Url.AbsolutePath;
                RedirectToAction("Index", "Home");
            }
            try
            {
                IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
                string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
              tf.TList = iba.GetTransferHistory(chkAcctNum);
               // gv1.DataSource = TList;
                //gv1.DataBind();
            }
            catch (Exception ex)
            {
                string status = ex.Message;
            }
            tlist = tf.TList;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            // you need to reference System.Web.Extensions
            

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(tf.TList);



            return View(tf);
        }
        [HttpPost]
        public ActionResult TrnsHist()
        {

            //from view transferhistory
            return View();

        }

        

        }
    }