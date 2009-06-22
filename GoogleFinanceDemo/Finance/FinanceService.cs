using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class FinanceService : Service
    {
        public const string ServiceName = "finance";

        public FinanceService(string applicationName) : base(ServiceName, applicationName)
        {
            NewFeed += new ServiceEventHandler(FinanceService_NewFeed);
        }

        void FinanceService_NewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Portfolio Feed");

            int i = e.Uri.AbsoluteUri.IndexOf("/" + "portfolios");
            int p = e.Uri.AbsoluteUri.IndexOf("/" + "positions");
            int t = e.Uri.AbsoluteUri.IndexOf("/" + "transactions");

            // TODO: This sucks, it needs to be changed to parse out the Feeds correcly.
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            else if (e.Uri.AbsoluteUri.IndexOf("/" + "transactions") != -1)
            {
                e.Feed = new TransactionFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsoluteUri.IndexOf("/" + "positions") != -1)
            {
                e.Feed = new PositionFeed(e.Uri, e.Service);
            }
            else if (e.Uri.AbsoluteUri.IndexOf("/" + "portfolios") != -1)
            {
                e.Feed = new PortfolioFeed(e.Uri, e.Service);
            }
        }

        public PortfolioFeed Query(PortfolioQuery feedQuery)
        {
            return base.Query(feedQuery) as PortfolioFeed;
        }

        public PositionFeed Query(PositionQuery feedQuery)
        {
            return base.Query(feedQuery) as PositionFeed;
        }

        public TransactionFeed Query(TransactionQuery feedQuery)
        {
            return base.Query(feedQuery) as TransactionFeed;
        }
    }
}
