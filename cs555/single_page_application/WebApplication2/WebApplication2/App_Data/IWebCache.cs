using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank.App_Code
{
    public interface IWebCache
    {
        void Remove(string key);
        void Store(string key, object obj);
        T Retrieve<T>(string key);
    }
}