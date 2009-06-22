using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using System.Xml;

namespace Finance
{
    // XXX: Move this class into its own file.
    public class Money : SimpleElement
    {
        public Money()
            : base("money", "gd", "http://schemas.google.com/finance/2007")
        {
            Attributes.Add("amount", null);
            Attributes.Add("currencyCode", null);

        }

        public Money(string initValue)
            : base("money", "gd", "http://schemas.google.com/finance/2007", initValue)
        {
            Attributes.Add("amount", null);
            Attributes.Add("currencyCode", null);
        }

        public Money(XmlNode node)
            : base("money", "gd", "http://schemas.google.com/finance/2007")
        {
            Attributes.Add("amount", null);
            Attributes.Add("currencyCode", null);
            Amount = float.Parse(node.Attributes["amount"].Value);
            CurrencyCode = node.Attributes["currencyCode"].Value as string;
        }

        public float Amount
        {
            get
            {
                return float.Parse(Attributes["amount"] as string);
            }
            set
            {
                Attributes["amount"] = value.ToString();
            }
        }

        public string CurrencyCode
        {
            get
            {
                return Attributes["currencyCode"] as string;
            }
            set
            {
                Attributes["currencyCode"] = value;
            }
        }

    }
}
