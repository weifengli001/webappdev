using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBankMVC15.Models;

namespace MyBankMVC15.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]  //
        public ActionResult News()
        {
            ViewBag.Username = HttpContext.User.Identity.Name;
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Transfer()
        {
           
            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            String UserName = System.Web.HttpContext.Current.User.Identity.Name;
            string chkAcctNum = iba.GetChkAcctNum(UserName);
            string savAcctNum = chkAcctNum + "1";
            CheckingAccount ca = new CheckingAccount();
            ca.CheckingAccountNumber = chkAcctNum;
            ca.Balance = iba.GetCheckingBalance(chkAcctNum);
            SavingAccount sa = new SavingAccount();
            sa.SavingAccountNumber = savAcctNum;
            sa.Balance = iba.GetSavingBalance(savAcctNum);
            ViewData["CheckingAccount"] = ca;
            ViewData["SavingAccount"] = sa;
            return View(new CheckingAccount());
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ViewResult Transfer(CheckingAccount ca)
        {
            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            try
            {
                string chkAcctNum = ca.CheckingAccountNumber;
                string savAcctNum = chkAcctNum + "1";
                if (iba.TransferFromChkgToSav(chkAcctNum, savAcctNum,
                    ca.Balance))
                {
                    ViewBag.msg = "Transfer successful..";

                    ca.Balance = iba.GetCheckingBalance(chkAcctNum);

                    SavingAccount sa = new SavingAccount();
                    sa.SavingAccountNumber = savAcctNum;
                    sa.Balance = iba.GetSavingBalance(savAcctNum);
                    ViewData["CheckingAccount"] = ca;
                    ViewData["SavingAccount"] = sa;
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View(ca);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult History()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public JsonResult getTransHistoryByPage(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
                String UserName = System.Web.HttpContext.Current.User.Identity.Name;
                string chkAcctNum = iba.GetChkAcctNum(UserName);
                int historyCount = iba.GetTransferHistoryCount(chkAcctNum);
                List<TransferHistory> Histories = iba.
                    GetTransferHistory(chkAcctNum, jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = Histories, TotalRecordCount = historyCount });
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}