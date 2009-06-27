using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PortfolioFeed : AbstractFeed
    {
        public PortfolioFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        public override AtomEntry CreateFeedEntry()
        {
            return new PortfolioEntry();
            //return new PortfolioEntryTest();
        }

        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            Tracing.TraceMsg("\t HandleExtensionElements for Portfolio feed called");
        }
    }
}
