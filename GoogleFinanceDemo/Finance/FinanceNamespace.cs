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
       

    }
}
