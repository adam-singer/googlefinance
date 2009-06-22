using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Xml;

namespace Finance
{
    public class CostBasis : SimpleElement
    {
        //XXX: Need to support more then one node of money... for instance,
        // when a security's default currency differs from the portfolio's
        // , then a second money element is included.. we need to check for that.
        public CostBasis()
            : base("costBasis", "gf", "http://schemas.google.com/finance/2007")
        {
            
        }

        public CostBasis(string initValue)
            : base("costBasis", "gf", "http://schemas.google.com/finance/2007", initValue)
        {
            
        }

        public Money Money
        {
            get;
            set;
        }

        public override IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            CostBasis e = base.CreateInstance(node, parser) as CostBasis;
            e.Money = new Money(node["gd:money"]);
            return e;
        }

    }

  
}
