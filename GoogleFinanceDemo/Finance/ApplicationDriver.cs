using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;

namespace Finance
{
    public class ApplicationDriver
    {
        /// <summary>
        /// The name of this application.
        /// </summary>
        public static string Name = ".NETSDK Finance";

        /// <summary>
        /// Main service provider. 
        /// </summary>
        private static FinanceService service;

        /// <summary>
        /// Feed of portfolios.
        /// </summary>
        private static PortfolioFeed portfolioFeed;

        private static PortfolioEntry portfolioEntry;

        private static PositionFeed portfolioPositionFeed;

        /// <summary>
        /// Default constructor for client login
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="password">Password</param>
        public ApplicationDriver(string user, string password)
        {
            ApplicationDriver.service = new FinanceService(ApplicationDriver.Name);
            ApplicationDriver.service.setUserCredentials(user, password);
#if DEBUG
            // Debugging requests and inserts.
            GDataLoggingRequestFactory factoryLogging = new GDataLoggingRequestFactory("finance", ApplicationDriver.Name);
            factoryLogging.MethodOverride = true;
            factoryLogging.CombinedLogFileName = @"c:\xmllog.log";
            ApplicationDriver.service.RequestFactory = factoryLogging;
#endif 
        }

        /// <summary>
        /// Constructor for webapplications. Obtain the token using the authsub helper methods.
        /// </summary>
        /// <param name="token">Your authentication token</param>
        public ApplicationDriver(string token)
        {
            GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory("finance", ApplicationDriver.Name);
            authFactory.Token = token;
            ApplicationDriver.service = new FinanceService(authFactory.ApplicationName);
            ApplicationDriver.service.RequestFactory = authFactory;
        }

        /// <summary>
        /// Reset objects to null so they can be reloaded on events.
        /// </summary>
        public void Refresh()
        {
            ApplicationDriver.portfolioFeed = null;
            ApplicationDriver.portfolioEntry = null;
            ApplicationDriver.portfolioPositionFeed = null;
        }

        /// <summary>
        /// Gets a list of Portfolio Names
        /// </summary>
        /// <returns>Returns Portfolio Names as a List object.</returns>
        public List<string> Portfolios
        {
            // TODO: Should return List of Portfolio objects instead of just the Title of the Portfolio.
            get
            {
                List<string> results = new List<string>();

                // Load or reload portfolios feed if needed.
                EnsurePortfoliosFeed();

                foreach (PortfolioEntry entry in ApplicationDriver.portfolioFeed.Entries)
                {
                    results.Add(entry.Title.Text);
                }

                return results;
            }
        }

        /// <summary>
        /// Reload Portfolio Feed if null.
        /// </summary>
        private void EnsurePortfoliosFeed()
        {
            if (ApplicationDriver.portfolioFeed == null)
            {
                PortfolioQuery query = new PortfolioQuery();
                ApplicationDriver.portfolioFeed = service.Query(query);
            }
        }

        /// <summary>
        /// Get the current PortfolioEntry object
        /// </summary>
        public PortfolioEntry CurrentPortfolioEntry
        {
            get
            {
                if (ApplicationDriver.portfolioEntry != null)
                {
                    //return ApplicationDriver.portfolioEntry.Title.Text;
                    return ApplicationDriver.portfolioEntry;
                }
                return null;
            }
        }

        /// <summary>
        /// Get or Set the current portfolio.
        /// </summary>
        public string CurrentPortfolioName
        {
            // TODO: Should return List of Portfolio objects instead of just the Title of the Portfolio.
            get
            {
                if (ApplicationDriver.portfolioEntry != null)
                {
                    return ApplicationDriver.portfolioEntry.Title.Text;
                    //return ApplicationDriver.portfolioEntry;
                }
                return null;
            }

            set
            {
                ApplicationDriver.portfolioEntry = null;
                if (value == null)
                {
                    return;
                }

                EnsurePortfoliosFeed();

                foreach (PortfolioEntry entry in ApplicationDriver.portfolioFeed.Entries)
                {
                    if (value.Equals(entry.Title.Text))
                    {
                        ApplicationDriver.portfolioEntry = entry;
                        break;
                    }

                    if (ApplicationDriver.portfolioEntry == null)
                    {
                        throw new ArgumentException(value + " was not a valid portfolio name");
                    }
                }
            }
        }

        //public List<TransactionEntry> CurrentTransactions
        //{
        //    get { return null; }
        //}

        /// <summary>
        /// Getter of the current positions in the view of the current portfolio. 
        /// </summary>
        public List<PositionEntry> CurrentPositions
        {
            // TODO: Should return List of Position objects instead of just the Title of the Positions.
            get {
                EnsurePortfoliosFeed();
                if (ApplicationDriver.portfolioEntry != null) {
                    // returns=true specifies to request that information about total financial returns and performance
                    // statistics be included in the feed entries.
                    PositionQuery positionQuery = new PositionQuery(ApplicationDriver.portfolioEntry.SelfUri.Content + "/positions" + "?returns=true");
                    ApplicationDriver.portfolioPositionFeed = service.Query(positionQuery);

                    List<PositionEntry> positions = new List<PositionEntry>();

                    //List<PositionEntry> positionsWithData = new List<PositionEntry>();
                    
                    foreach (PositionEntry entry in ApplicationDriver.portfolioPositionFeed.Entries)
                    {
                        positions.Add(entry);
//#if DEBUG
//                        string str = entry.Id.AbsoluteUri;
//                        // XXX: This is for testing, please remove
//                        PositionQuery tmpPositionQuery = new PositionQuery(str);
//                        PositionFeed tmp = service.Query(tmpPositionQuery);
//                        foreach (PositionEntry en in tmp.Entries)
//                        {
//                            Console.WriteLine(en.Return1Week);
//                        }
//#endif                
                    }

                    return positions;
                }
                return null; 
            }
        }

       
        /// <summary>
        /// Create a portfolio 
        /// </summary>
        /// <param name="title">Name of the portfolio</param>
        /// <param name="portfolioData">Settings for the portfolio</param>
        /// <returns>Returns the response from the server</returns>
        public PortfolioEntry CreatePortfolio(string title, PortfolioData portfolioData)
        {
            // Create a new entry.
            PortfolioEntry entry = new PortfolioEntry();
            // Set the name of the portfolio
            entry.Title.Text = title;
            // Set the portfolio data
            entry.PortfolioData = portfolioData;

            // Default to USD
            if (entry.PortfolioData.CurrencyCode.Length == 0) entry.PortfolioData.CurrencyCode = "USD";

            // Return the response from the server.
            return service.Insert(new Uri(FinanceNamespace.PORTFOLIOS), entry);
        }

        /// <summary>
        /// Create a portfolio
        /// </summary>
        /// <param name="title">Name of the portfolio</param>
        /// <returns>Returns the response from the server</returns>
        public PortfolioEntry CreatePortfolio(string title)
        {
            return CreatePortfolio(title, new PortfolioData() { CurrencyCode = "USD" });
        }

        /// <summary>
        /// Create a portfolio
        /// </summary>
        /// <param name="title">Name of the portfolio</param>
        /// <param name="currencyCode">Currency code of the portfolio</param>
        /// <returns></returns>
        public PortfolioEntry CreatePortfolio(string title, string currencyCode)
        {
            return CreatePortfolio(title, new PortfolioData() { CurrencyCode = currencyCode });
        }
    }
}
