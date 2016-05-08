using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for IBusinessAccount
/// </summary>
public interface IBusinessAccount
{
	bool TransferFromChkgToSav(string chkAcctNum, string savAcctNum, double amt);
    double GetCheckingBalance(string chkAcctNum);
    double GetSavingBalance(string savAcctNum);
    int GetTransferHistoryCount(string chkAcctNum);
    List<TransferHistory> GetTransferHistory(string chkAcctNum);
    List<TransferHistory>  GetTransferHistory(string chkAcctNum, int jtStartIndex, int jtPageSize, string jtSorting);
    string GetChkAcctNum(string UserName);
}