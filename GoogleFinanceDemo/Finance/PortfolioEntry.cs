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
    /// <summary>
    /// PortfolioEntry API class for defining entries in a portfolio feed.
    /// </summary>
    public class PortfolioEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain portfolio extension data.
        /// </summary>
        public static AtomCategory PORTFOLIO_CATEGORY
        = new AtomCategory(FinanceNamespace.PORTFOLIOTERM,
                           new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new PortfolioEntry instance with the appropriate category 
        /// and extensions to indicate that it is a portfolio.
        /// </summary>
        public PortfolioEntry()
            : base()
        {
            Categories.Add(PORTFOLIO_CATEGORY);
            this.AddExtension(new PortfolioData());
        }

        /// <summary>
        /// The portfolioData element in this entry. 
        /// </summary>
        public PortfolioData PortfolioData
        {
            get
            
            {
                // returns null if no extension is found.
                return FindExtension(FinanceNamespace.PORTFOLIODATA,
                                      FinanceNamespace.NAMESPACE) as PortfolioData;
            }
            set
            {
                // creates an extension if one doesnt exists
                // replaces the extension if one does exist.
                ReplaceExtension(FinanceNamespace.PORTFOLIODATA,
                                  FinanceNamespace.NAMESPACE,
                                  value);
            } 
        }
    }
}
