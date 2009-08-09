using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Finance;

namespace Finance
{
    public interface IGoogleFinanceManager
    {
        FinanceService FinanceService { get; }

        bool PortfolioDetails { set; get; }
        bool PositionDetails { set; get; }
        bool TransactionDetails { set; get; }

        List<string> PortfolioNames {get;}
        Dictionary<string, PortfolioEntry> Portfolios {get;}
        PortfolioEntry CreatePortfolio(string title, PortfolioData portfolioData);
        PortfolioEntry CreatePortfolio(string title);
        PortfolioEntry CreatePortfolio(string title, string currencyCode);
        bool DeleteAllPortfolios();
        bool DeletePortfolio(string title);
        TransactionEntry AddSymbol(string fullSymbolName, string portfolioTitle);
        TransactionEntry AddSymbol(string fullSymbolName, PortfolioEntry entry);
        TransactionEntry AddSymbol(string exchange, string symbol, PortfolioEntry entry);
        Dictionary<string, PositionEntry> RetrieveSymbols(PortfolioEntry entry);
        Dictionary<string, PositionEntry> RetrieveSymbols(string title);
        void DeleteSymbol(string symbolRemove, string title);
        void DeleteSymbol(string symbolRemove, PortfolioEntry portfolioEntry);
        void DeleteSymbol(PositionEntry positionEntry);

    }
}
