using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;
using Google.GData.Client;
using System.Xml;

namespace Finance
{
    //XXX: Need to support more then one node of money... for instance,
    // when a security's default currency differs from the portfolio's
    // , then a second money element is included.. we need to check for that.

    public class MarketValue : SimpleElement
    {
          public MarketValue()
            : base("marketValue", "gf", "http://schemas.google.com/finance/2007")
        {
            
        }

          public MarketValue(string initValue)
              : base("marketValue", "gf", "http://schemas.google.com/finance/2007", initValue)
        {
            
        }

          public Money Money
          {
              get;
              set;
          }

          public override IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
          {
              MarketValue e = base.CreateInstance(node, parser) as MarketValue;
              e.Money = new Money(node["gd:money"]);
              return e;
          }
    }
}
