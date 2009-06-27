using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Finance
{
    // TODO: Finish implementation of this.

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

        public TransactionTypes Type {
            
            get
            {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                string ttype = ((XmlExtension)i).Node.Attributes["type"].Value;
                // XXX: need to handle this in a more elegant way. This is nasty and the enum still has problems
                // converting back to selected string. Use Attributes on the enum to convert enum values back
                // to proper strings.
                if (ttype == "Sell Short")
                {
                    return TransactionTypes.SELLSHORT;
                }
                else if (ttype == "Buy to Cover")
                {
                    return TransactionTypes.BUYTOCONVERT;
                }
                else if (ttype == "Buy")
                {
                    return TransactionTypes.BUY;
                }
                else if (ttype == "Sell")
                {
                    return TransactionTypes.SELL;
                }

                throw new Exception("TransactionType Not Found");
            }
        }

        public DateTime Date { 
            get {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                DateTime dt = DateTime.Parse(((XmlExtension)i).Node.Attributes["date"].Value);
                return dt;
            } 
        }
        public double Shares
        {
            get
            {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                return double.Parse(((XmlExtension)i).Node.Attributes["shares"].Value);
            }
        }
        public string Notes
        {
            get
            {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                return ((XmlExtension)i).Node.Attributes["notes"].Value;
            }
        }
        public Commission Commissions
        {
            get {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                Commission co = new Commission();
                co = co.CreateInstance(((XmlExtension)i).Node["gf:commission"], new AtomFeedParser()) as Commission;
                return co; }
        }

        public Price Price
        {
            get {
                IExtensionElementFactory i = FindExtension("transactionData", "http://schemas.google.com/finance/2007");
                Price pr = new Price();
                pr = pr.CreateInstance(((XmlExtension)i).Node["gf:price"], new AtomFeedParser()) as Price;    
                return pr; 
            }
        }


    }
}
