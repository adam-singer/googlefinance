using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    // TODO: Finish implementation of this.

    public enum TransactionTypes
    {
        BUY,
        SELL
    }

    public class TransactionEntry : AbstractEntry
    {
        public static AtomCategory TRANSACTION_CATEGORY
            = new AtomCategory("http://schemas.google.com/finance/2007#transaction",
                new AtomUri(BaseNameTable.gKind));

        public TransactionEntry()
            : base()
        {
            Categories.Add(TRANSACTION_CATEGORY);

            //Example to turn the enum back into a string for the creation of the xml object.
            //TransactionTypes t = TransactionTypes.BUY;
            //string str = Enum.GetName(typeof(TransactionTypes), t);

        }

        // TODO: Create real types, the strings are temporary. 

        public TransactionTypes Type { get; set; }
        public DateTime Date { get; set; }
        public double Shares { get; set; }
        public string Notes { get; set; }
        public Commission Commissions
        {
            get { return null; }
        }

        public Price Price
        {
            get { return null; }
        }


    }
}
