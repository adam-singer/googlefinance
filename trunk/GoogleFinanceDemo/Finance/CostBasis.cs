using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Xml;

namespace Finance
{
    public class CostBasis : MoneyContainer
    {
        public CostBasis()
            : base(FinanceNamespace.COSTBASIS, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
        }
    }

}
