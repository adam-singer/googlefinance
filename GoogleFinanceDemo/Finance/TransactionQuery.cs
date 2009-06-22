using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class TransactionQuery : DocumentQuery
    {
        //public TransactionQuery()
        //    : base("")
        //{
        //    throw new Exception("TransactionQuery 
        //}

        public TransactionQuery(string baseUri)
            : base(baseUri)
        {
        }
    }
}
