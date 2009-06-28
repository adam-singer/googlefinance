using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class PortfolioQuery : DocumentQuery
    {
        /// <summary>
        /// A subclass of DocumentQuery, to create a portfolio query URI.
        /// Provides public properties that describe the different aspects
        /// of the URI, as well as a composite URI.
        /// </summary>
        public PortfolioQuery()
            : base(FinanceNamespace.PORTFOLIOS + "?returns=true&positions=true")
        { }

        public PortfolioQuery(string baseUri)
            : base(baseUri)
        {
        }
    }
}
