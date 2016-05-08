using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank.App_Code
{
    public interface IRepositoryDataAuthentication
    {
        string IsValidUser(string uname, string pwd);
        // returns Checking AccountNumber

        bool UpdatePassword(string uname, string oldPW, string newPW);
    }
}