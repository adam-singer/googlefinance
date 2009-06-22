using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PortfolioQuery : DocumentQuery
    {
        //public string PortfolioFeeds = "http://finance.google.com/finance/feeds/default/portfolios";

        public PortfolioQuery()
            : base("http://finance.google.com/finance/feeds/default/portfolios" + "?returns=true&positions=true")
        { }

        public PortfolioQuery(string baseUri)
            : base(baseUri)
        {
        }
    }
}
