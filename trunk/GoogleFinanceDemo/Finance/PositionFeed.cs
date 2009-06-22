using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PositionFeed : AbstractFeed
    {
        public PositionFeed(Uri uriBase, IService iService)
            : base(uriBase, iService)
        {
        }

        public override AtomEntry CreateFeedEntry()
        {
            return new PositionEntry();
        }
    }
}
