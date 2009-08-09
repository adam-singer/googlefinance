using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Finance
{
    public class FinanceNamespace
    {
        public const string PREFIX_FINANCE = "gf";
        public const string NAMESPACE_FINANCE = "http://schemas.google.com/finance/2007";

        // TODO: rename this to PORTFOLIOFEEDS
        public const string PORTFOLIOS = "http://finance.google.com/finance/feeds/default/portfolios";

        // Terms 
        public const string PORTFOLIOTERM = NAMESPACE_FINANCE + "#portfolio";
        public const string POSITIONTERM = NAMESPACE_FINANCE + "#position";
        public const string TRANSACTIONTERM = NAMESPACE_FINANCE + "#transaction";

        // Attribute strings
        public const string CURRENCYCODE = "currencyCode";
        public const string GAINPERCENTAGE = "gainPercentage";
        public const string RETURN1W = "return1w";
        public const string RETURN4W = "return4w";
        public const string RETURN3M = "return3m";
        public const string RETURNYTD = "returnYTD";
        public const string RETURN1Y = "return1y";
        public const string RETURN3Y = "return3y";
        public const string RETURN5Y = "return5y";
        public const string RETURNOVERALL = "returnOverall";
        public const string SHARES = "shares";
        public const string FULLNAME = "fullName";
        public const string EXCHANGE = "exchange";
        public const string TYPE = "type";
        public const string DATE = "date";
        public const string NOTES = "notes";
        
        // Element strings
        public const string PORTFOLIODATA = "portfolioData";
        public const string POSITIONDATA = "positionData";
        public const string TRANSACTIONDATA = "transactionData";
        public const string COSTBASIS = "costBasis";
        public const string DAYSGAIN = "daysGain";
        public const string GAIN = "gain";
        public const string MARKETVALUE = "marketValue";
        public const string SYMBOL = "symbol";
        public const string PRICE = "price";
        public const string COMMISSION = "commission";

        public const string PREFIX_GDATA = "gd";
        public const string NAMESPACE_GDATA = "http://schemas.google.com/g/2005";
        public const string MONEY = "money";
        public const string AMOUNT = "amount";
        public const string FEEDLINK = "feedLink";

        // Uri construction strings.
        public const string POSITIONS = "positions";
        public const string POSITIONAPPENDQUERY = "/" + POSITIONS;
        public const string TRANSACTIONS = "transactions";
        public const string TRANSACTIONSAPPENDQUERY = "/" + TRANSACTIONS;

        public const string AMP = "&";

        // NOTE: double check all these values and there returns!

        /// <summary>
        /// Specify returns=true to request that information about total financial returns 
        /// and performance statistics be included in the feed entries.
        /// <remarks>Supported only in portfolio and position feeds.</remarks>
        /// <example>http://finance.google.com/finance/feeds/default/portfolios/1/positions?returns=true&transactions=true
        ///</example>
        /// </summary>
        public const string RETURNSDETAIL = "returns=true";
        /// <summary>
        /// Specify positions=true to request that the position feed for each portfolio 
        /// entry be embedded as an inline feed, contained in the feed link element.
        /// <remarks>Supported only in portfolio feeds. If the returns parameter is also set, 
        /// returns data is included in the inlined feed as well.</remarks>
        /// <example>
        /// http://finance.google.com/finance/feeds/default/portfolios/1/positions?returns=true&transactions=true&positions=true
        /// </example>
        /// </summary>
        public const string POSITIONDETAILS = "positions=true";
        
        /// <summary>
        /// Specify transactions=true to request that the transaction feed for each position 
        /// entry be embedded as an inline feed, contained in the feed link element.
        /// <remarks>Supported only in position feeds.</remarks>
        /// <example>
        /// http://finance.google.com/finance/feeds/default/portfolios/1/positions?returns=true&transactions=true&positions=true
        /// </example>
        /// </summary>
        public const string TRANSACTIONSDETAIL = "transactions=true";
       

    }
}
