using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Finance
{
    public class FinanceNamespace
    {
        public const string PREFIX = "gf";
        public const string NAMESPACE = "http://schemas.google.com/finance/2007";

        public const string PORTFOLIOS = "http://finance.google.com/finance/feeds/default/portfolios";

        public const string PORTFOLIODATA = "portfolioData";
        public const string PORTFOLIOTERM = NAMESPACE + "#portfolio";
 
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
      
     
    }
}
