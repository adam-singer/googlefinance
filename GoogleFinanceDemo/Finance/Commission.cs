using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using Google.GData.Client;
using System.Xml;

namespace Finance
{
    public class Commission : MoneyContainer
    {
          public Commission()
            : base(FinanceNamespace.COMMISSION, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
        }
    }
}
