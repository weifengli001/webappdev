using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RepositoryAbstraction
/// </summary>
public class RepositoryAbstraction : IRepositoryDataAccount, IRepositoryDataAuthentication
    
{
    IRepositoryDataAccount _irepdacc = null;
    IRepositoryDataAuthentication _irepdauth = null;
	public RepositoryAbstraction()
    {
       // _irepdacc = GenericFactory<RepositoryMySql, IRepositoryDataAccount>.CreateInstance();
       // _irepdauth = GenericFactory<RepositoryMySql, IRepositoryDataAuthentication>.CreateInstance();
         _irepdacc = GenericFactory<Repository, IRepositoryDataAccount>.CreateInstance();
         _irepdauth = GenericFactory<Repository, IRepositoryDataAuthentication>.CreateInstance();
        

    }

    public T CreateInstance<T>(T trep)
        where T : IRepositoryDataAccount,IRepositoryDataAuthentication, new()
    {
        trep = new T();
        _irepdacc = (IRepositoryDataAccount) trep;
        _irepdauth = (IRepositoryDataAuthentication)trep;
        return trep;
    }

    public bool TransferChkToSav(string chkAcctNum, string savAcctNum, double amt)
    {
        return _irepdacc.TransferChkToSav(chkAcctNum,savAcctNum,amt);
    }

    public bool TransferChkToSavViaSP(string chkAcctNum, string savAcctNum, double amt)
    {
        throw new NotImplementedException();
    }

    public double GetCheckingBalance(string chkAcctNum)
    {
        return _irepdacc.GetCheckingBalance(chkAcctNum);
    }

    public double GetSavingBalance(string savAcctNum)
    {
        return _irepdacc.GetSavingBalance(savAcctNum);
    }

    public List<TransferHistory> GetTransferHistory(string chkAcctNum)
    {
        return _irepdacc.GetTransferHistory(chkAcctNum);
    }

    public string IsValidUser(string uname, string pwd)
    {
        return _irepdauth.IsValidUser(uname,pwd);
    }

    public bool UpdatePassword(string uname, string oldPW, string newPW)
    {
        throw new NotImplementedException();
    }
}