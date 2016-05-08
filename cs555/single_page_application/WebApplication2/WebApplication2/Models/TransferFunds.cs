using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank_MVC.Models
{
    public class TransferFunds
    {
        public int ChkAcuntNmbr { get; set; }
        public string ChkAcntBal { get; set; }
        public string SavAcntBal { get; set; }
        public double TAmount { get; set; }
        public string sTatus { get; set; }
    }
}   