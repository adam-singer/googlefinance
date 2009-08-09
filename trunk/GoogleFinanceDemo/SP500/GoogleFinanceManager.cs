using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Finance;
using Google.GData.Client;
namespace SP500
{
    /// <summary>
    /// This object is going to do all the work for us. This should be moved into Finance and then have an interface for it.
    /// </summary>
    public class GoogleFinanceManager : IGoogleFinanceManager
    {
        public FinanceService FinanceService { private set; get; }

        public bool PortfolioDetails { set; get; }
        public bool PositionDetails { set; get; }
        public bool TransactionDetails { set; get; }

        /// <summary>
        /// Enablese the query to provide more details. <see cref="http://code.google.com/apis/finance/reference.html#Parameters"/>
        /// </summary>
        /// <returns>string that enables more parameters to contain more details from the queries.</returns>
        public string Details()
        {
            string detailsParameters = "";
            List<string> d = new List<string>();

            if (PortfolioDetails == true) d.Add(FinanceNamespace.RETURNSDETAIL);

            if (PositionDetails == true) d.Add(FinanceNamespace.POSITIONDETAILS);
            
            if (TransactionDetails == true) d.Add(FinanceNamespace.TRANSACTIONSDETAILS);
            
            switch (d.Count)
            {
                case 0:
                    detailsParameters = "";
                    break;

                case 1:
                    detailsParameters = FinanceNamespace.QUESTIONMARK + d[0];
                    break;

                case 2:
                    detailsParameters = FinanceNamespace.QUESTIONMARK + d[0] + FinanceNamespace.AMP + d[1];
                    break;

                case 3:
                    detailsParameters = FinanceNamespace.QUESTIONMARK + d[0] + FinanceNamespace.AMP + d[1] + FinanceNamespace.AMP + d[2];
                    break;
            }

            return detailsParameters;
        }

        public GoogleFinanceManager(string username, string passsword)
        {
            FinanceService = new FinanceService("GoogleFinanceManager");
            FinanceService.setUserCredentials(username, passsword);

#if DEBUG
            // Debugging requests and inserts.
            GDataLoggingRequestFactory factoryLogging = new GDataLoggingRequestFactory("finance", ApplicationDriver.Name);
            factoryLogging.MethodOverride = true;
            factoryLogging.CombinedLogFileName = @"c:\xmllog.log";
            FinanceService.RequestFactory = factoryLogging;
#endif 
        }

        #region Property for a list of current portolio names
        public List<string> PortfolioNames
        {
            get
            {
                List<string> portfolioNames = new List<string>();
                PortfolioQuery query = new PortfolioQuery(FinanceNamespace.PORTFOLIOS + Details());
                PortfolioFeed portfolioFeed = FinanceService.Query(query);
                try
                {
                    foreach (PortfolioEntry entry in portfolioFeed.Entries)
                    {
                        portfolioNames.Add(entry.Title.Text);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw ex;
                }

                return portfolioNames;
            }
        }

        public Dictionary<string, PortfolioEntry> Portfolios
        {
            get
            {
                Dictionary<string, PortfolioEntry> portfolios = new Dictionary<string, PortfolioEntry>();
                PortfolioQuery query = new PortfolioQuery(FinanceNamespace.PORTFOLIOS + Details());
                PortfolioFeed portfolioFeed = FinanceService.Query(query);
                try
                {
                    foreach (PortfolioEntry entry in portfolioFeed.Entries)
                    {
                        portfolios.Add(entry.Title.Text, entry);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //throw ex;
                }

                return portfolios;
            }
        }
    
        #endregion 

        #region Methods for creating and deleting a portofolio
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
            try
            {
                return FinanceService.Insert(new Uri(FinanceNamespace.PORTFOLIOS), entry);
            }
            catch (GDataRequestException ex)
            {
                Console.WriteLine("Unable to create portfolio, Exception: {0}", ex.Message);
                return null;
            }
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

        public bool DeleteAllPortfolios()
        {
            try
            {
                foreach (string title in PortfolioNames)
                {
                    DeletePortfolio(title);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while trying to delete all the portfolios: {0}", ex.Message);
            }

            return false;
        }
        
        public bool DeletePortfolio(string title)
        {
            PortfolioQuery query = new PortfolioQuery(FinanceNamespace.PORTFOLIOS + Details());
            PortfolioFeed portfolioFeed = FinanceService.Query(query);
            try
            {
                foreach (PortfolioEntry entry in portfolioFeed.Entries)
                {
                    if (entry.Title.Text == title)
                    {
                        FinanceService.Delete(entry);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception trying to delete portfolio = {0}, Message = {1}", title, ex.Message);
            }

            return false;
        }

        public TransactionEntry AddSymbol(string fullSymbolName, string portfolioTitle)
        {
            PortfolioQuery query = new PortfolioQuery(FinanceNamespace.PORTFOLIOS + Details());
            PortfolioFeed portfolioFeed = FinanceService.Query(query);
            try
            {
                foreach (PortfolioEntry entry in portfolioFeed.Entries)
                {
                    if (entry.Title.Text == portfolioTitle)
                    {
                        return AddSymbol(fullSymbolName, entry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw ex;
            }

            return null;
        }

        public TransactionEntry AddSymbol(string fullSymbolName, PortfolioEntry entry)
        {
            if (!fullSymbolName.Contains(":")) return null;

            return AddSymbol(fullSymbolName.Split(':')[0], fullSymbolName.Split(':')[1], entry);
        }

        public TransactionEntry AddSymbol(string exchange, string symbol, PortfolioEntry entry)
        {
            PositionFeed positionFeed = FinanceService.Query(new PositionQuery(entry.EditUri.Content + FinanceNamespace.POSITIONAPPENDQUERY + Details()));

            TransactionEntry transactionEntry = new TransactionEntry()
            {
                TransactionData = new TransactionData()
                {
                    Type = TransactionTypes.BUY
                }
            };

            PositionEntry positionEntry = new PositionEntry()
            {
                Symbol = new Symbol()
                {
                    StockSymbol = symbol,
                    Exchange = exchange
                }
            };

            Uri uri = new Uri(positionFeed.Feed + "/" + positionEntry.Symbol.Exchange + ":" + positionEntry.Symbol.StockSymbol + "/" + FinanceNamespace.TRANSACTIONS);
            try
            {
                return FinanceService.Insert(uri, transactionEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while trying to add symbol={0} to portfolio={1}", symbol + ":" + exchange, entry.Title.Text);
                Console.WriteLine("Exception: {0}", ex.Message);
                return null;
            }
        }

        #endregion

        #region Retrieve Symbols from portfolio
        // TODO: add the detailed query here also.
        public Dictionary<string, PositionEntry> RetrieveSymbols(PortfolioEntry entry)
        {
            PositionFeed positionFeed = FinanceService.Query(new PositionQuery(entry.EditUri.Content + FinanceNamespace.POSITIONAPPENDQUERY + Details()));
            Dictionary<string, PositionEntry> symbols = new Dictionary<string, PositionEntry>();

            foreach (PositionEntry positionEntry in positionFeed.Entries)
            {
                symbols.Add(positionEntry.Symbol.Exchange + ":" + positionEntry.Symbol.StockSymbol, positionEntry);
            }
            return symbols;
        }

        public Dictionary<string, PositionEntry> RetrieveSymbols(string title)
        {
            return RetrieveSymbols(Portfolios[title]);
        }
        #endregion 

        #region Delete Symbol from a portfolio
        public void DeleteSymbol(string symbolRemove, string title)
        {
            DeleteSymbol(symbolRemove, Portfolios[title]);
        }

        public void DeleteSymbol(string symbolRemove, PortfolioEntry portfolioEntry)
        {
            DeleteSymbol(RetrieveSymbols(portfolioEntry)[symbolRemove]);
        }

        public void DeleteSymbol(PositionEntry positionEntry)
        {
            TransactionFeed transactionFeed = FinanceService.Query(new TransactionQuery(positionEntry.TransactionHerf + Details())); //+ FinanceNamespace.TRANSACTIONSAPPENDQUERY));

            foreach (TransactionEntry transactionEntry in transactionFeed.Entries)
            {
                FinanceService.Delete(new Uri(transactionEntry.EditUri.Content));
            }
        }
        #endregion 
    }
}
