using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Xml;
using System.ComponentModel;
using Google.GData.GoogleBase;

namespace Finance
{

    public class PortfolioEntry : AbstractEntry
    {
        public static AtomCategory PORTFOLIO_CATEGORY
        = new AtomCategory("http://schemas.google.com/finance/2007#portfolio",
                           new AtomUri(BaseNameTable.gKind));

        public PortfolioEntry()
            : base()
        {
            Categories.Add(PORTFOLIO_CATEGORY);
        }

        public string CurrencyCode
        {
            get
            {
                IExtensionElementFactory i = FindExtension("portfolioData", "http://schemas.google.com/finance/2007");
                return ((XmlExtension)i).Node.Attributes["currencyCode"].Value; 
            }
        }

        public double GainPercentage
        {
            get
            {
                return ReturnDoubleAttribute("gainPercentage");
            }
        }

        public double Return1Week
        {
            get
            {
                return ReturnDoubleAttribute("return1w");
            }
        }

        public double Return4Week
        {
            get
            {
                return ReturnDoubleAttribute("return4w");
            }
        }

        public double Return3Month
        {
            get
            {
                return ReturnDoubleAttribute("return3m");
            }
        }

        public double ReturnYTD
        {
            get
            {
                return ReturnDoubleAttribute("returnYTD");
            }
        }

        public double Return1Year
        {
            get
            {
                return ReturnDoubleAttribute("return1y");
            }

        }

        public double Return3Year
        {

            get
            {
                return ReturnDoubleAttribute("return3y");
            }
        }

        public double Return5Year
        {

            get
            {
                return ReturnDoubleAttribute("return5y");
            }
        }

        public double ReturnOverall
        {
            get
            {
                return ReturnDoubleAttribute("returnOverall");
            }
        }

        public double ReturnDoubleAttribute(string attributeName)
        {
            IExtensionElementFactory i = FindExtension("portfolioData", "http://schemas.google.com/finance/2007");
            return double.Parse(((XmlExtension)i).Node.Attributes[attributeName].Value);
        }

    }
}
