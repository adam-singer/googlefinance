using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;

namespace Finance
{
    public class TransactionData : SimpleContainer
    {
        public TransactionData() :
            base(FinanceNamespace.TRANSACTIONDATA, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
            this.ExtensionFactories.Add(new Price());
            this.ExtensionFactories.Add(new Commission());

            Attributes.Add(FinanceNamespace.TYPE, null);
            Attributes.Add(FinanceNamespace.DATE, null);
            Attributes.Add(FinanceNamespace.SHARES, null);
            Attributes.Add(FinanceNamespace.NOTES, null);
        }

        public Price Price
        {
            get
            {
                return FindExtension(FinanceNamespace.PRICE,
                                         FinanceNamespace.NAMESPACE_FINANCE) as Price;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.PRICE,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        public Commission Commission
        {
            get
            {
                return FindExtension(FinanceNamespace.COMMISSION,
                                         FinanceNamespace.NAMESPACE_FINANCE) as Commission;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.COMMISSION,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        public TransactionTypes Type
        {
            get
            {
                string t = Attributes[FinanceNamespace.TYPE] as string;

                if (t == "Sell Short")
                {
                    return TransactionTypes.SELLSHORT;
                }
                else if (t == "Buy to Cover")
                {
                    return TransactionTypes.BUYTOCONVERT;
                }
                else if (t == "Buy")
                {
                    return TransactionTypes.BUY;
                }
                else if (t == "Sell")
                {
                    return TransactionTypes.SELL;
                }

                throw new Exception("TransactionType Not Found");
            }

            set
            {
                if (value == TransactionTypes.BUY)
                {
                    Attributes[FinanceNamespace.TYPE] = "Buy";
                }
                else if (value == TransactionTypes.BUYTOCONVERT)
                {
                    Attributes[FinanceNamespace.TYPE] = "Buy to Cover";
                }
                else if (value == TransactionTypes.SELL)
                {
                    Attributes[FinanceNamespace.TYPE] = "Sell";
                }
                else if (value == TransactionTypes.SELLSHORT)
                {
                    Attributes[FinanceNamespace.TYPE] = "Sell Short";
                }
            }
        }

        public DateTime Date
        {
            get
            {
                DateTime dt = DateTime.Parse(Attributes[FinanceNamespace.DATE] as string);
                return dt;
            }
            set
            {
                Attributes[FinanceNamespace.DATE] = value.ToString();
            }
        }

        public double Shares
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.SHARES] as string);
            }
            set
            {
                Attributes[FinanceNamespace.SHARES] = value.ToString();
            }
        }

        public string Notes
        {
            get
            {
                return Attributes[FinanceNamespace.NOTES] as string;
            }
            set
            {
                Attributes[FinanceNamespace.NOTES] = value;
            }
        }
    }
}
