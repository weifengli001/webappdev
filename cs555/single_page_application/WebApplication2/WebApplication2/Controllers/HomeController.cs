using MyBank.App_Code;
using MyBank_MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Login");

        }
        [HttpPost]
        
        public JsonResult  Login (List<string> val)
        {
            LoginModel lm= new LoginModel();
            lm.UserName = val[0];
            lm.PassWord = val[1];
           var sTatus = "please login!";
            IBusinessAuthentication iau = GenericFactory<BusinessLayer, IBusinessAuthentication>.CreateInstance();
            try
            {
                string chkAcctNum = iau.IsValidUser(Utils.StripPunctuation(lm.UserName),
                    Utils.StripPunctuation(lm.PassWord));
                //  string chkAcctNum = iau.IsValidUser(txtUsername.Text,
                //      txtPassword.Text); 

                if (chkAcctNum != "")
                {
                    sTatus = "Welcome User";
                    SessionFacade.USERNAME = lm.UserName;
                    SessionFacade.CHECKINGACCTNUM = chkAcctNum;
                    if (SessionFacade.PAGEREQUESTED != null)
                        Response.Redirect(SessionFacade.PAGEREQUESTED);
                }

                else
                {
                    sTatus = "Invalid User..";
                    return Json(sTatus);
                }
            }
            catch (Exception ex)
            {
                sTatus = "My  mistake ! try again !";
                return Json(sTatus);
            }
            

            return Json(sTatus);
        }
        [HttpGet]
        public JsonResult TransferFunds (TransferFunds tfs)
        {

            tfs.sTatus = "not logged in";
            
            if (SessionFacade.USERNAME == null)
            {
                SessionFacade.PAGEREQUESTED = Request.Url.AbsolutePath;
                tfs.sTatus = "Not logged in!";
                RedirectToAction("Index", "Home");
            }

            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
            string savAcctNum = chkAcctNum + "1";
            tfs.ChkAcntBal = iba.GetCheckingBalance(chkAcctNum).ToString();
            tfs.SavAcntBal = iba.GetSavingBalance(savAcctNum).ToString();
            tfs.ChkAcuntNmbr = Int32.Parse(chkAcctNum);
            tfs.sTatus = "welcome";

            var data = tfs;
            var viewModel = tfs;
            var serializer = new JavaScriptSerializer();
            // viewModel = serializer.Serialize(data);

            //  return View("viewname", viewModel);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
           // return Json( viewModel);

        }
        [HttpPost]
        public JsonResult SavtoChk(List<string> val)
        {
            TransferFunds tf = new TransferFunds();
            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            try
            {
                tf.TAmount = Int32.Parse(val[0]);
                string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
                string savAcctNum = chkAcctNum + "1";
                if (iba.TransferFromChkgToSav(chkAcctNum, savAcctNum,
                    tf.TAmount))
                {
                    tf.sTatus = "Transfer successful..";
                    tf.ChkAcntBal = iba.GetCheckingBalance(chkAcctNum).ToString();
                    tf.SavAcntBal = iba.GetSavingBalance(savAcctNum).ToString();
                }
            }
            catch (Exception ex)
            {
                tf.sTatus = "my mistake try again !";
            }
            //return Json(tf.sTatus);
            return Json(tf, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult History(TransferHistoryModel tf)
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
            //return Json(json, JsonRequestBehavior.AllowGet);
            return Json(new { Result = "OK", Records = tf.TList});

        }

    }
}