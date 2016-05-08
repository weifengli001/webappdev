using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank.App_Code
{
    public interface IRepositoryDataAccount
    {
        bool TransferChkToSav(string chkAcctNum, string savAcctNum, double amt);
        bool TransferChkToSavViaSP(string chkAcctNum,
                   string savAcctNum, double amt);
        double GetCheckingBalance(string chkAcctNum);
        double GetSavingBalance(string savAcctNum);
        List<TransferHistory> GetTransferHistory(string chkAcctNum);
    }
}