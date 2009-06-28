using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using Google.GData.Client;
using System.Xml;

namespace Finance
{
    public class MarketValue : MoneyContainer
    {
          public MarketValue()
            : base(FinanceNamespace.MARKETVALUE, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
            
        }
    }
}
