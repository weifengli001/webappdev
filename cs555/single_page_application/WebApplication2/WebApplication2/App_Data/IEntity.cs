using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MyBank.App_Code
{
    public interface IEntity
    {
        void SetFields(DataRow dr);
    }
}