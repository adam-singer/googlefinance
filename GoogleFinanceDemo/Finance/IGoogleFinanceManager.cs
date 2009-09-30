using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Finance;

namespace Finance
{
    public interface IGoogleFinanceManager
    {
        /// <summary>
        /// Finance Service Proxy.
        /// </summary>
        FinanceService FinanceService { get; }

        /// <summary>
        /// Enables the return of portfolio details. 
        /// </summary>
        bool PortfolioDetails { set; get; }

        /// <summary>
        /// Enables the return of position details
        /// </summary>
        bool PositionDetails { set; get; }

        /// <summary>
        /// Enables the return of transaction details
        /// </summary>
        bool TransactionDetails { set; get; }

        /// <summary>
        /// Gets the portfolio names.
        /// </summary>
        List<string> PortfolioNames {get;}

        /// <summary>
        /// Gets the portfolio names with associated portfolio entries.
        /// </summary>
        Dictionary<string, PortfolioEntry> Portfolios {get;}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="portfolioData"></param>
        /// <returns></returns>
        PortfolioEntry CreatePortfolio(string title, PortfolioData portfolioData);
        PortfolioEntry CreatePortfolio(string title);
        PortfolioEntry CreatePortfolio(string title, string currencyCode);
        
        bool DeleteAllPortfolios();
        bool DeletePortfolio(string title);
        
        TransactionEntry AddSymbol(string fullSymbolName, string portfolioTitle);
        TransactionEntry AddSymbol(string fullSymbolName, PortfolioEntry entry);
        TransactionEntry AddSymbol(string exchange, string symbol, PortfolioEntry entry);
        TransactionEntry AddSymbol(string fullSymbolName, TransactionDataArgs args, string title);
        TransactionEntry AddSymbol(string exchange, string symbol, TransactionDataArgs args, string title);
        TransactionEntry AddSymbol(string fullSymbolName, TransactionDataArgs args, PortfolioEntry entry);
        TransactionEntry AddSymbol(string exchange, string symbol, TransactionDataArgs args, PortfolioEntry entry);

        Dictionary<string, PositionEntry> RetrieveSymbols(PortfolioEntry entry);
        Dictionary<string, PositionEntry> RetrieveSymbols(string title);
        List<TransactionEntry> RetrieveSymbolTransaction(PositionEntry entry);

        void DeleteSymbol(string symbolRemove, string title);
        void DeleteSymbol(string symbolRemove, PortfolioEntry portfolioEntry);
        void DeleteSymbol(PositionEntry positionEntry);

    }
}
