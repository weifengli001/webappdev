using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Transactions;



/// <summary>
/// Summary description for DataLayer
/// </summary>
public class Repository : IRepositoryDataAuthentication, IRepositoryDataAccount
{
    //IDataAccess _idataAccess = null;
    DataAbstraction _dac = null;
    CacheAbstraction webCache = null;

   // public Repository(IDataAccess ida, CacheAbstraction webc)
    public Repository(DataAbstraction dac, CacheAbstraction webc)
    {
        //_idataAccess = ida;
        _dac = dac;
        webCache = webc;
    }

    //public Repository()
    //    : this(GenericFactory<DataAccess,IDataAccess>.CreateInstance(),new CacheAbstraction())
    public Repository() : this(new DataAbstraction(), new CacheAbstraction())    
    {
    }


    #region IDataAuthentication Members

    public string IsValidUser(string uname, string pwd)
    {
        string res = "";
        try
        {
            string sql = "select CheckingAccountNum from Users where " +
                "Username=@uname and Password=@pwd";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@uname", SqlDbType.VarChar, 50);
            p1.Value = uname;
            PList.Add(p1);
            SqlParameter p2 = new SqlParameter("@pwd", SqlDbType.VarChar, 50);
            p2.Value = pwd;
            PList.Add(p2);
            //object obj = _idataAccess.GetSingleAnswer(sql, PList);
            object obj = _dac.GetSingleAnswer(sql, PList);
            if (obj != null)
                res = obj.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;

    }

    public string GetChkAcctNum(string UserName)
    {
        string res = "";
        try
        {
            string sql = "select CheckingAccountNum from Users where " +
                "Username = @uname";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@uname", SqlDbType.VarChar, 50);
            p1.Value = UserName;
            PList.Add(p1);
            object obj = _dac.GetSingleAnswer(sql, PList);
            if (obj != null)
                res = obj.ToString();
        }
        catch(Exception ex)
        {
            throw ex;
        }
        return res;
    }

    #endregion

    #region IDataAccount Members

    public bool TransferChkToSav(string chkAcctNum, string savAcctNum, double amt)
    {
        // to do
        //string CONNSTR = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
        //bool res = false;
        //SqlConnection conn = new SqlConnection(CONNSTR);
        //using (IDbTransaction tran = conn.BeginTransaction())
        //{
        //    try
        //    {
        //        // your code
        //        tran.Commit();
        //    }
        //    catch
        //    {
        //        tran.Rollback();
        //        throw;
        //    }
        //}
        try
        {
            using (TransactionScope tran = new TransactionScope())
            {
                if (amt <= 0) throw new Exception("Transfer amt should be bigger than 0.");
                double chkbalance = GetCheckingBalance(chkAcctNum);
                if(chkbalance >= amt)
                {
                    UpdateAccountingBalance(chkAcctNum, chkbalance - amt);
                    double savbalance = GetSavingBalance(savAcctNum);
                    UpdateSavingBalance(savAcctNum, savbalance + amt);
                    TransferHistory history = new TransferHistory();
                    history.FromAccountNum = chkAcctNum;
                    history.ToAccountNum = savAcctNum;
                    history.Amount = (decimal)amt;
                    history.CheckingAccountNumber = chkAcctNum;
                    WriteTransferHistory(history);

                }
                else
                {
                    throw new Exception("no enogh balance.");
                }
                
                tran.Complete();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

       
        return true;
    }

    public double GetCheckingBalance(string chkAcctNum)
    {
        double res = 0;
        try
        {
            string sql = "select Balance from CheckingAccounts where " +
                "CheckingAccountNumber=@chkAcctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@chkAcctNum", SqlDbType.VarChar, 50);
            p1.Value = chkAcctNum;
            PList.Add(p1);
            //object obj = _idataAccess.GetSingleAnswer(sql,PList);
            object obj = _dac.GetSingleAnswer(sql, PList);
            if (obj != null)
                res = double.Parse(obj.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        };
        return res;
    }

    public bool TransferChkToSavViaSP(string chkAcctNum, string savAcctNum,
           double amt)
    {
        string CONNSTR = ConfigurationManager.ConnectionStrings["BANKDBCONN"].ConnectionString;
        bool res = false;
        SqlConnection conn = new SqlConnection(CONNSTR);
        try
        {
            conn.Open();
            string sql = "SPXferChkToSav"; // name of SP
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter p1 = new SqlParameter("@ChkAcctNum",
                                System.Data.SqlDbType.VarChar, 50);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            p1.Value = chkAcctNum;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@SavAcctNum",
                                System.Data.SqlDbType.VarChar, 50);
            p2.Value = savAcctNum;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@amt",
                                System.Data.SqlDbType.Money);
            p3.Value = amt;
            cmd.Parameters.Add(p3);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 3)
                res = true;
            
            // clear cache for TransferHistory
            string key = String.Format("TransferHistory_{0}",
    chkAcctNum);
            webCache.Remove(key);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }

    public double GetSavingBalance(string savAcctNum)
    {
        double res = 0;
        try
        {
            string sql = "select Balance from SavingAccounts where " +
                "SavingAccountNumber=@savAcctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@savAcctNum", SqlDbType.VarChar, 50);
            p1.Value = savAcctNum;
            PList.Add(p1);
            //object obj = _idataAccess.GetSingleAnswer(sql,PList);
            object obj = _dac.GetSingleAnswer(sql, PList);
            if (obj != null)
                res = double.Parse(obj.ToString());
        }
        catch (Exception)
        {
            throw;
        };
        return res;
    }

    #endregion

    #region IDataAccount Members
    public List<TransferHistory> GetTransferHistory(string chkAcctNum)
    {
        List<TransferHistory> TList = null;
        try
        {
            string key = String.Format("TransferHistory_{0}",
                chkAcctNum);
            TList = webCache.Retrieve<List<TransferHistory>>(key);
            if (TList == null)  
            {
                //TList = new List<TransferHistory>();
                DataTable dt = GetTransferHistoryDB(chkAcctNum);
                TList = RepositoryHelper.ConvertDataTableToList<TransferHistory>(dt);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    TransferHistory the = new TransferHistory();
                //    the.SetFields(dr);
                //    TList.Add(the);
                //}
                webCache.Insert(key, TList);
            }
         }
        catch (Exception ex)
        {
            throw ex;
        };
        return TList;
    }

    public List<TransferHistory> GetTransferHistory(string chkAcctNum, int jtStartIndex, int jtPageSize, string jtSorting)
    {
        List<TransferHistory> TList = null;
        try
        {
            
                DataTable dt = GetTransferHistoryDB(chkAcctNum, jtStartIndex, jtPageSize, jtSorting);
                TList = RepositoryHelper.ConvertDataTableToList<TransferHistory>(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        };
        return TList;
    }


    #endregion

    public System.Data.DataTable GetTransferHistoryDB(string chkAcctNum)
    {
        DataTable dt = null;
        try
        {
            string sql = "select * from TransferHistory where " +
                "CheckingAccountNumber=@chkAcctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@chkAcctNum", SqlDbType.VarChar, 50);
            p1.Value = chkAcctNum;
            PList.Add(p1);
            // dt = _idataAccess.GetDataTable(sql,PList);
            dt = _dac.GetDataTable(sql, PList);
        }
        catch (Exception ex)
        {
            throw ex;
        };
        return dt;
    }

    public System.Data.DataTable GetTransferHistoryDB(string chkAcctNum, int jtStartIndex, int jtPageSize, string jtSorting)
    {
        DataTable dt = null;
        if (jtSorting == "TransferDate DESC")
        {
            jtSorting = "Date DESC";
        }
        try
        {
            string sqlpageing = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + jtSorting +
                ") AS Row, * FROM TransferHistory) " +
                "AS HistoryWithRowNumbers where CheckingAccountNumber=@chkAcctNum " + 
                " and Row > @jtStartIndex and Row <= @jtEndIndex ORDER BY " + jtSorting ;
            List<DbParameter> PList = new List<DbParameter>();


            SqlParameter p1 = new SqlParameter("@chkAcctNum", SqlDbType.VarChar, 50);
            p1.Value = chkAcctNum;
            PList.Add(p1);

            SqlParameter p2 = new SqlParameter("@jtStartIndex", SqlDbType.Int, 50);
            p2.Value = jtStartIndex;
            PList.Add(p2);

            SqlParameter p3 = new SqlParameter("@jtEndIndex", SqlDbType.Int, 50);
            p3.Value = jtStartIndex + jtPageSize;
            PList.Add(p3);

            // dt = _idataAccess.GetDataTable(sql,PList);
            dt = _dac.GetDataTable(sqlpageing, PList);
        }
        catch (Exception ex)
        {
            throw ex;
        };
        return dt;
    }

    public bool UpdatePassword(string uname, string oldPW, string newPW)
    {
        throw new NotImplementedException();
    }

    public bool UpdateAccountingBalance(string acctNum, double amt)
    {
        bool res = false;
        try
        {
            string sql = "update CheckingAccounts set Balance = @amt where CheckingAccountNumber = @acctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@amt", SqlDbType.Decimal);
            SqlParameter p2 = new SqlParameter("@acctNum", SqlDbType.VarChar, 50);
            p1.Value = amt;
            p2.Value = acctNum;
            PList.Add(p1);
            PList.Add(p2);
            int ret = _dac.InsOrUpdOrDel(sql, PList);
            if (ret > 0) res = true;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        return res;
    }

    public bool UpdateSavingBalance(string acctNum, double amt)
    {
        bool res = false;
        try
        {
            string sql = "update SavingAccounts set Balance = @amt where SavingAccountNumber = @acctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@amt", SqlDbType.Decimal);
            SqlParameter p2 = new SqlParameter("@acctNum", SqlDbType.VarChar, 50);
            p1.Value = amt;
            p2.Value = acctNum;
            PList.Add(p1);
            PList.Add(p2);
            int ret = _dac.InsOrUpdOrDel(sql, PList);
            if (ret > 0) res = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return res;
    }

    public int WriteTransferHistory(TransferHistory history)
    {
        int ret = 0;
        try
        {
            string sql = "insert into TransferHistory values(@FromAccountNum, @ToAccountNum, GETDATE(), @Amount, @CheckingAccountNumber)";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@FromAccountNum", SqlDbType.VarChar, 50);
            SqlParameter p2 = new SqlParameter("@ToAccountNum", SqlDbType.VarChar, 50);
            //SqlParameter p3 = new SqlParameter("@Date", SqlDbType.DateTime);
            SqlParameter p4 = new SqlParameter("@Amount", SqlDbType.Decimal);
            SqlParameter p5 = new SqlParameter("@CheckingAccountNumber", SqlDbType.VarChar, 50);
            p1.Value = history.FromAccountNum;
            p2.Value = history.ToAccountNum;
            //p3.Value = history.TransferDate;
            p4.Value = history.Amount;
            p5.Value = history.CheckingAccountNumber;
            PList.Add(p1);
            PList.Add(p2);
            //PList.Add(p3);
            PList.Add(p4);
            PList.Add(p5);
            ret = _dac.InsOrUpdOrDel(sql, PList);

        }
        catch(Exception ex)
        {
            throw ex;
        }
        return ret;
    }

    public int GetTransferHistoryCount(string chkAcctNum)
    {
        int res = 0;
        try
        {
            string sql = "select count(*) from TransferHistory where " +
                "CheckingAccountNumber=@chkAcctNum";
            List<DbParameter> PList = new List<DbParameter>();
            SqlParameter p1 = new SqlParameter("@chkAcctNum", SqlDbType.VarChar, 50);
            p1.Value = chkAcctNum;
            PList.Add(p1);
            object obj = _dac.GetSingleAnswer(sql, PList);
            if (obj != null)
                res = int.Parse(obj.ToString());
        }
        catch (Exception)
        {
            throw;
        };
        return res;
    }
}