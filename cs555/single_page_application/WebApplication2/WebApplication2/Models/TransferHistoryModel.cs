using MyBank.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank_MVC.Models
{
    public class TransferHistoryModel
    {
        public int ChkAcntNumbr;
   public      List<TransferHistory> TList { get; set; }


    }
}