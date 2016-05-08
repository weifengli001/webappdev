using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBank.App_Code
{
    public class NullObjectCache : IWebCache
    {  // to allow disabling of caching

        #region IWebCache Members

        public void Remove(string key)
        {
        }

        public void Store(string key, object obj)
        {
        }

        public T Retrieve<T>(string key)
        {
            return default(T);
        }

        #endregion
    }
}