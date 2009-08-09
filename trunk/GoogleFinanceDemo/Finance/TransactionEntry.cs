using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Finance
{
    public enum TransactionTypes
    {
        BUY,
        SELL,
        BUYTOCONVERT,
        SELLSHORT
    }

    public class TransactionEntry : AbstractEntry
    {
        public static AtomCategory TRANSACTION_CATEGORY
            = new AtomCategory(FinanceNamespace.TRANSACTIONTERM,
                new AtomUri(BaseNameTable.gKind));

        public TransactionEntry()
            : base()
        {
            Categories.Add(TRANSACTION_CATEGORY);
            this.AddExtension(new TransactionData());
        }

        public TransactionData TransactionData
        {
            get
            {
                // returns null if no extension is found.
                return FindExtension(FinanceNamespace.TRANSACTIONDATA,
                                      FinanceNamespace.NAMESPACE_FINANCE) as TransactionData;
            }
            set
            {
                // creates an extension if one doesnt exists
                // replaces the extension if one does exist.
                ReplaceExtension(FinanceNamespace.TRANSACTIONDATA,
                                  FinanceNamespace.NAMESPACE_FINANCE,
                                  value);
            }
        }

        public TransactionTypes Type 
        {
            get { return TransactionData.Type; }
        }
        public string Date 
        {
            get { return TransactionData.Date; }
        }
        public double Shares
        {
            get { return TransactionData.Shares; }
        }
        public string Notes
        {
            get { return TransactionData.Notes; }
        }
        public Commission Commissions
        {
            get { return TransactionData.Commission; }
        }
        public Price Price
        {
            get { return TransactionData.Price; }
        }
    }
}
