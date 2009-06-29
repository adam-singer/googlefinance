using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using System.Xml;

namespace Finance
{
    public class Money : SimpleElement
    {
        public Money()
            : base(FinanceNamespace.MONEY, FinanceNamespace.PREFIX_GDATA, FinanceNamespace.NAMESPACE_GDATA)
        {
            Attributes.Add(FinanceNamespace.AMOUNT, null);
            Attributes.Add(FinanceNamespace.CURRENCYCODE, null);

        }

        public Money(string initValue)
            : base(FinanceNamespace.MONEY, FinanceNamespace.PREFIX_GDATA, FinanceNamespace.NAMESPACE_GDATA, initValue)
        {
            Attributes.Add(FinanceNamespace.AMOUNT, null);
            Attributes.Add(FinanceNamespace.CURRENCYCODE, null);
        }

        public float Amount
        {
            get
            {
                return float.Parse(Attributes[FinanceNamespace.AMOUNT] as string);
            }
            set
            {
                Attributes[FinanceNamespace.AMOUNT] = value.ToString();
            }
        }

        public string CurrencyCode
        {
            get
            {
                return Attributes[FinanceNamespace.CURRENCYCODE] as string;
            }
            set
            {
                Attributes[FinanceNamespace.CURRENCYCODE] = value;
            }
        }

    }
}
