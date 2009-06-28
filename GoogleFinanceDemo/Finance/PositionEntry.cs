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
                = new AtomCategory(FinanceNamespace.POSITIONTERM,
                           new AtomUri(BaseNameTable.gKind));

        public PositionEntry()
            : base()
        {
            Categories.Add(POSITION_CATEGORY);

            this.AddExtension(new PositionData());
            this.AddExtension(new FeedLink());
            this.AddExtension(new Symbol());
        }

        // NOTE: this is how to get transactions, but we want to keep the PositionEntry object as clean as we can
        // we can always make a query from the application driver instead of the object.
        //public List<TransactionEntry> Transactions
        //{
        //    get
        //    {
        //        string absId = this.Id.AbsoluteUri;
        //        TransactionQuery transactionQuery = new TransactionQuery(this.FeedLink.Href);
        //        TransactionFeed transactionFeed = service.Query(transactionQuery);

        //        List<TransactionEntry> transactions = new List<TransactionEntry>();
        //        foreach (TransactionEntry transaction in transactionFeed.Entries)
        //        {
        //            transactions.Add(transaction);
        //        }

        //        return transactions;
        //    }
        //}

        // NOTE: we can get the transaction HERF from this property.
        public FeedLink FeedLink
        {
            get
            {
                return FindExtension(FinanceNamespace.FEEDLINK,
                    FinanceNamespace.NAMESPACE_GDATA) as FeedLink;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.FEEDLINK,
                    FinanceNamespace.NAMESPACE_GDATA, value);
            }
        }

        // NOTE: do we really want to be able to set this variable?
        // it might make sense to just create a new FeedLink
        public string TransactionHerf
        {
            get
            {
                return FeedLink.Href;
            }
            set
            {
                FeedLink.Href = value;
            }
        }

        public Symbol Symbol
        {
            get
            {
                return FindExtension(FinanceNamespace.SYMBOL,
                    FinanceNamespace.NAMESPACE_FINANCE) as Symbol;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.SYMBOL,
                    FinanceNamespace.NAMESPACE_FINANCE, value);
            }
        }

        public PositionData PositionData
        {
            get
            {
                // returns null if no extension is found.
                return FindExtension(FinanceNamespace.POSITIONDATA,
                                      FinanceNamespace.NAMESPACE_FINANCE) as PositionData;
            }
            set
            {
                // creates an extension if one doesnt exists
                // replaces the extension if one does exist.
                ReplaceExtension(FinanceNamespace.POSITIONDATA,
                                  FinanceNamespace.NAMESPACE_FINANCE,
                                  value);
            }
        }

        
        // NOTE: The typed objects of PositionData will return null if ?returns=true is not in the query parameter. 
        // This is considered extra information... 
        public CostBasis CostBasis
        {
            get { return PositionData.CostBasis; }
        }

        public DaysGain DaysGain
        {
            get { return PositionData.DaysGain; }
        }

        public Gain Gain
        {
            get { return PositionData.Gain; }
        }

        public MarketValue MarketValue
        {
            get { return PositionData.MarketValue; }
        }
        
        public double GainPercentage
        {
            get { return PositionData.GainPercentage; }
        }

        public double Return1Week
        {
            get { return PositionData.Return1Week; }
        }
        public double Return4Week
        {
            get { return PositionData.Return4Week; }
        }
        public double Return3Month
        {
            get { return PositionData.Return3Month; }
        }
        public double ReturnYTD
        {
            get { return PositionData.ReturnYTD; }
        }
        public double Return1Year
        {
            get { return PositionData.Return1Year; }
        }
        public double Return3Year
        {
            get { return PositionData.Return3Year; }
        }
        public double Return5Year
        {
            get { return PositionData.Return5Year; }
        }
        public double ReturnOverall
        {
            get { return PositionData.ReturnOverall; }
        }

        public double Shares
        {
            get { return PositionData.Shares; }
        }
    }
}
