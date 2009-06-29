using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using Google.GData.Client;
using System.Xml;

namespace Finance
{
    public class Price : SimpleElement
    {
         public Price()
            : base(FinanceNamespace.PRICE, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {   
        }
    }
}
