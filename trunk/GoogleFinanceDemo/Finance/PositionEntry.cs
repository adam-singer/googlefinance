using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Xml;

namespace Finance
{
    public class PositionEntry : AbstractEntry
    {
        public static AtomCategory POSITION_CATEGORY
                = new AtomCategory("http://schemas.google.com/finance/2007#position",
                           new AtomUri(BaseNameTable.gKind));

        public PositionEntry()
            : base()
        {
            Categories.Add(POSITION_CATEGORY);
        }

        FinanceService service;
        public PositionEntry(FinanceService iService)
            : base()
        {
            service = iService; // Have a copy of IService to retrive the transactions list.
            Categories.Add(POSITION_CATEGORY);
        }

        public List<TransactionEntry> Transactions
        {
            get
            {
                string absId = this.Id.AbsoluteUri;
                TransactionQuery transactionQuery = new TransactionQuery(this.FeedLink.Href);
                TransactionFeed transactionFeed = service.Query(transactionQuery);

                List<TransactionEntry> transactions = new List<TransactionEntry>();
                foreach (TransactionEntry transaction in transactionFeed.Entries)
                {
                    transactions.Add(transaction);
                }

                return transactions;
            }
        }

        public FeedLink FeedLink
        {
            get
            {
                // NOTE: this is the correct way to find extensions and cast them.
                // good example of how to correcly parse is to check out
                // FeedLink Parser, Google.GData.Extensions.FeedLink
                // FeedLink doesnt use the parser you pass, it creates its own static parser..
                IExtensionElementFactory tmpFeedLinkData = FindExtension("feedLink", "http://schemas.google.com/g/2005");
                FeedLink tmpFeedLink = new FeedLink();
                tmpFeedLink = tmpFeedLink.CreateInstance(((XmlExtension)tmpFeedLinkData).Node, new AtomFeedParser()) as FeedLink;
                return tmpFeedLink;
            }
        }

        public Symbol Symbol
        {
            get
            {
                IExtensionElementFactory tmpSymbolData = FindExtension("symbol", "http://schemas.google.com/finance/2007");
                Symbol tmpSymbol = new Symbol();
                tmpSymbol = tmpSymbol.CreateInstance(((XmlExtension)tmpSymbolData).Node, new AtomFeedParser()) as Symbol;
                return tmpSymbol;
            }
        }


        public CostBasis CostBasis
        {
            get
            {
                // Sucky way to extract the correct xml elements.
                IExtensionElementFactory tmpPositionData = FindExtension("positionData", "http://schemas.google.com/finance/2007");
                CostBasis tmpCostBasis = new CostBasis();
                tmpCostBasis = tmpCostBasis.CreateInstance(((XmlExtension)tmpPositionData).Node["gf:costBasis"], new AtomFeedParser()) as CostBasis;
                return tmpCostBasis;
            }
        }

        public DaysGain DaysGain
        {
            get
            {
                IExtensionElementFactory tmpPositionData = FindExtension("positionData", "http://schemas.google.com/finance/2007");
                DaysGain tmpDaysGain = new DaysGain();
                tmpDaysGain = tmpDaysGain.CreateInstance(((XmlExtension)tmpPositionData).Node["gf:daysGain"], new AtomFeedParser()) as DaysGain;
                return tmpDaysGain;
            }
        }

        public Gain Gain
        {
            get
            {
                IExtensionElementFactory tmpPositionData = FindExtension("positionData", "http://schemas.google.com/finance/2007");
                Gain tmpGain = new Gain();
                tmpGain = tmpGain.CreateInstance(((XmlExtension)tmpPositionData).Node["gf:gain"], new AtomFeedParser()) as Gain;
                return tmpGain;
            }
        }

        public MarketValue MarketValue
        {
            get
            {
                IExtensionElementFactory tmpPositionData = FindExtension("positionData", "http://schemas.google.com/finance/2007");
                MarketValue tmpMarketValue = new MarketValue();
                tmpMarketValue = tmpMarketValue.CreateInstance(((XmlExtension)tmpPositionData).Node["gf:marketValue"], new AtomFeedParser()) as MarketValue;
                return tmpMarketValue;
            }
        }
        
        public double GainPercentage
        {
            get
            {
                return ReturnDoubleAttribute("gainPercentage");
            }
        }

        public double Return1Week
        {
            get
            {
                return ReturnDoubleAttribute("return1w");
            }
        }

        public double Return4Week
        {
            get
            {
                return ReturnDoubleAttribute("return4w");
            }
        }

        public double Return3Month
        {
            get
            {
                return ReturnDoubleAttribute("return3m");
            }
        }

        public double ReturnYTD
        {
            get
            {
                return ReturnDoubleAttribute("returnYTD");
            }
        }

        public double Return1Year
        {
            get
            {
                return ReturnDoubleAttribute("return1y");
            }

        }

        public double Return3Year
        {

            get {
                return ReturnDoubleAttribute("return3y");
            }
        }

        public double Return5Year
        {

            get {
                return ReturnDoubleAttribute("return5y");
            }
        }

        public double ReturnOverall
        {
            get { 
                return ReturnDoubleAttribute("returnOverall");
            }
        }

        public double Shares
        {
            get { 
                return ReturnDoubleAttribute("shares");
            }
        }

        public double ReturnDoubleAttribute(string attributeName)
        {
            IExtensionElementFactory i = FindExtension("positionData", "http://schemas.google.com/finance/2007");
            return double.Parse(((XmlExtension)i).Node.Attributes[attributeName].Value);
        }
    }
}
