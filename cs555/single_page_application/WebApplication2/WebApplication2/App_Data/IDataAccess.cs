using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyBank.App_Code
{
    public interface IDataAccess
    {
        object GetSingleAnswer(string sql, List<SqlParameter> PList);
        DataTable GetDataTable(string sql, List<SqlParameter> PList);
        int InsOrUpdOrDel(string sql, List<SqlParameter> PList);
    }
}