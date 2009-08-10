using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance
{
    public class TransactionDataArgs
    {
        public TransactionTypes TransactionType { set; get; }
        public float Price { set; get; }
        public float Commission { set; get; }
        public double Shares { set; get; }
        public string Notes { set; get; }
        public DateTime Date;
        public string CurrencyCode { set; get; }
    }
}
