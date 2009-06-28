using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;

namespace Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class DaysGain : MoneyContainer
    {
        public DaysGain()
            : base(FinanceNamespace.DAYSGAIN, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
        }
    }


}
