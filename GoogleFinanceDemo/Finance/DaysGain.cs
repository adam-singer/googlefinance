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
    public class DaysGain : SimpleElement
    {
        //XXX: Need to support more then one node of money... for instance,
        // when a security's default currency differs from the portfolio's
        // , then a second money element is included.. we need to check for that.

         public DaysGain()
            : base("daysGain", "gf", "http://schemas.google.com/finance/2007")
        {
        }

         public DaysGain(string initValue)
            : base("daysGain", "gf", "http://schemas.google.com/finance/2007", initValue)
        {   
        }

        public Money Money
        {
            get;
            set;
        }

        public override Google.GData.Client.IExtensionElementFactory CreateInstance(System.Xml.XmlNode node, Google.GData.Client.AtomFeedParser parser)
        {
            DaysGain e = base.CreateInstance(node, parser) as DaysGain;
            e.Money = new Money(node["gd:money"]);
            return e;
        }

    }
}
