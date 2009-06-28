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
            //this.AddExtension(new CostBasis());
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
                                      FinanceNamespace.NAMESPACE_FINANCE) as PortfolioData;
            }
            set
            {
                // creates an extension if one doesnt exists
                // replaces the extension if one does exist.
                ReplaceExtension(FinanceNamespace.PORTFOLIODATA,
                                  FinanceNamespace.NAMESPACE_FINANCE,
                                  value);
            }
        }

        #region Public Properties for PortfolioData Properties
        public string CurrencyCode
        {
            get { return PortfolioData.CurrencyCode; }
        }
        public double GainPercentage
        {
            get { return PortfolioData.GainPercentage; }
        }
        public double Return1Week
        {
            get { return PortfolioData.Return1Week; }
        }
        public double Return4Week
        {
            get { return PortfolioData.Return4Week; }
        }
        public double Return3Month
        {
            get { return PortfolioData.Return3Month; }
        }
        public double ReturnYTD
        {
            get { return PortfolioData.ReturnYTD; }
        }
        public double Return1Year
        {
            get { return PortfolioData.Return1Year; }
        }
        public double Return3Year
        {
            get { return PortfolioData.Return3Year; }
        }
        public double Return5Year
        {
            get { return PortfolioData.Return5Year; }
        }
        public double ReturnOverall
        {
            get { return PortfolioData.ReturnOverall; }
        }
        public CostBasis CostBasis
        {
            get { return PortfolioData.CostBasis; }
        }
        public DaysGain DaysGain
        {
            get { return PortfolioData.DaysGain; }
        }
        public Gain Gain
        {
            get { return PortfolioData.Gain; }
        }
        public MarketValue MarketValue
        {
            get { return PortfolioData.MarketValue; }
        }
        #endregion 
    }
}
