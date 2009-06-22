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
    public class Commission : SimpleElement
    {

          public Commission()
            : base("commission", "gf", "http://schemas.google.com/finance/2007")
        {
            
        }

          public Commission(string initValue)
             : base("commission", "gf", "http://schemas.google.com/finance/2007", initValue)
        {
            
        }

         public Money Money
         {
             get;
             set;
         }

         public override IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
         {
             Commission e = base.CreateInstance(node, parser) as Commission;
             e.Money = new Money(node["gd:money"]);
             return e;
         }

    }
}
