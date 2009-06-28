using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PortfolioFeed : AbstractFeed
    {
        /// <summary>
        /// Construct the portfolio feed.
        /// </summary>
        /// <param name="uriBase">Base URI of the feed entry</param>
        /// <param name="iService">The service to use</param>
        public PortfolioFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        /// <summary>
        /// Create the portfolio entry.
        /// </summary>
        /// <returns>Portfolio Entry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new PortfolioEntry();
        }

        /// <summary>
        /// Get's called after we already handled the custom entry, to handle all other potential parsing tasks.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">The atom feed parser.</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            Tracing.TraceMsg("\t HandleExtensionElements for Portfolio feed called");
        }
    }
}
