﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank.App_Code
{
    public interface IBusinessAccount
    {
        bool TransferFromChkgToSav(string chkAcctNum, string savAcctNum, double amt);
        double GetCheckingBalance(string chkAcctNum);
        double GetSavingBalance(string savAcctNum);
        List<TransferHistory> GetTransferHistory(string chkAcctNum);
    }
}