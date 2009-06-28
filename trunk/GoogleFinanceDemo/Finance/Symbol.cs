using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Finance
{

    public class Symbol : SimpleElement
    {
        public Symbol() : base(FinanceNamespace.SYMBOL, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
            Attributes.Add(FinanceNamespace.FULLNAME, null);
            Attributes.Add(FinanceNamespace.EXCHANGE, null);
            Attributes.Add(FinanceNamespace.SYMBOL, null);
        }

        public Symbol(string initValue)
            : base(FinanceNamespace.SYMBOL, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE, initValue)
        {
            Attributes.Add(FinanceNamespace.FULLNAME, null);
            Attributes.Add(FinanceNamespace.EXCHANGE, null);
            Attributes.Add(FinanceNamespace.SYMBOL, null);
        }

        public string FullName
        {
            get
            {
                return Attributes[FinanceNamespace.FULLNAME] as string;
            }
            set
            {
                Attributes[FinanceNamespace.FULLNAME] = value;
            }
        }

        public string Exchange
        {
            get
            {
                return Attributes[FinanceNamespace.EXCHANGE] as string;
            }
            set
            {
                Attributes[FinanceNamespace.EXCHANGE] = value;
            }
        }
        public string StockSymbol
        {
            get
            {
                return Attributes[FinanceNamespace.SYMBOL] as string;
            }
            set
            {
                Attributes[FinanceNamespace.SYMBOL] = value;
            }
        }
    }
}
