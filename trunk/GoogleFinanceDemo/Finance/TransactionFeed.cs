using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class TransactionFeed : AbstractFeed
    {
        public TransactionFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        public override AtomEntry CreateFeedEntry()
        {
            return new TransactionEntry();
        }
    }
}
