using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PositionQuery : DocumentQuery
    {
        public PositionQuery()
            : base("http://finance.google.com/finance/feeds/default/portfolios/1/positions")
        {

        }

        public PositionQuery(string baseUri) : base(baseUri)
        {

        }
    }
}
