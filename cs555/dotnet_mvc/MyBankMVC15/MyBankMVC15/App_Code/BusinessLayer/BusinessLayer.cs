using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BusinessLayer
/// </summary>
public class BusinessLayer : IBusinessAuthentication, IBusinessAccount
{
    //IRepositoryDataAuthentication idau = null;
    //IRepositoryDataAccount idac = null;
    RepositoryAbstraction _rap = new RepositoryAbstraction();

    //public BusinessLayer(IRepositoryDataAuthentication idauth, IRepositoryDataAccount idacc)
    //{
    //    idau = idauth;
    //    idac = idacc;
    //}
    
    //public BusinessLayer():
    //    this(GenericFactory<Repository, IRepositoryDataAuthentication>.CreateInstance(),
    //GenericFactory<Repository, IRepositoryDataAccount>.CreateInstance())
    //{
    //}

    #region IAuthentication Members

    public string IsValidUser(string uname, string pwd)
    {
        string res = "";
        try
        {
            //res = idau.IsValidUser(uname, pwd);
            res = _rap.IsValidUser(uname, pwd);
        }
        catch (Exception)
        {
            throw;
        }
        return res;
    }

    public string GetChkAcctNum(string UserName)
    {
        string res = "";
        try
        {
            res = _rap.GetChkAcctNum(UserName);
        }
        catch (Exception)
        {
            throw;
        }
        return res;
    }

    public bool ChangePassword(string uname, string oldpwd, string newpwd)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region IBusinessAccount Members

    public bool TransferFromChkgToSav(string chkAcctNum, string savAcctNum, double amt)
    {
        // return idac.TransferChkToSavViaSP(chkAcctNum,savAcctNum,amt);
        // return _rap.TransferChkToSavViaSP(chkAcctNum, savAcctNum, amt);
        return _rap.TransferChkToSav(chkAcctNum, savAcctNum, amt);  // mysql does not have the sp
    }

    #endregion

    #region IBusinessAccount Members


    public double GetCheckingBalance(string chkAcctNum)
    {
        double res = 0;
        try
        {
            // res = idac.GetCheckingBalance(chkAcctNum);
            res = _rap.GetCheckingBalance(chkAcctNum);
        }
        catch (Exception)
        {
            throw;
        }
        return res;
    }

    #endregion

    #region IBusinessAccount Members


    public double GetSavingBalance(string savAcctNum)
    {
        double res = 0;
        try
        {
            // res = idac.GetSavingBalance(savAcctNum);
            res = _rap.GetSavingBalance(savAcctNum);
        }
        catch (Exception)
        {
            throw;
        }
        return res;
    }

    #endregion

    #region IBusinessAccount Members


    public List<TransferHistory> GetTransferHistory(string chkAcctNum)
    {
        List<TransferHistory> TList = null;
        try
        {
            // TList = idac.GetTransferHistory(chkAcctNum);
            TList = _rap.GetTransferHistory(chkAcctNum);
        }
        catch (Exception)
        {
            throw;
        }
        return TList;
    }

    public List<TransferHistory> GetTransferHistory(string chkAcctNum, int jtStartIndex, int jtPageSize, string jtSorting)
    {
        List<TransferHistory> TList = null;
        try
        {
            // TList = idac.GetTransferHistory(chkAcctNum);
            TList = _rap.GetTransferHistory(chkAcctNum, jtStartIndex, jtPageSize, jtSorting);
        }
        catch (Exception)
        {
            throw;
        }
        return TList;
    }

    public int GetTransferHistoryCount(string chkAcctNum)
    {
        int res = 0;
        try
        {
            res = _rap.GetTransferHistoryCount(chkAcctNum);
        }
        catch (Exception)
        {
            throw;
        }
        return res;
    }

    #endregion
}