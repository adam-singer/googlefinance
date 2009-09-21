using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

using Finance;
using Google.GData.Client;

namespace SP500
{
    class Program
    {
        static void Main(string[] args)
        {
            // File name of csv file.
            string csvFileName;
            // temp string for reading from the console.
            string s;
            // xelement document to store the csv file in.
            XElement xmlDoc = null;
            string portfolioName;
            int portfolioIndex;
            // Temporary portfolio entry
            PortfolioEntry portfolioEntry;
            TransactionEntry transactionEntry;
            Dictionary<string, PositionEntry> symbols;
            string fullSymbolName;
            string user;
            string password;

            if (ConfigurationSettings.AppSettings["Username"] == "" || 
                ConfigurationSettings.AppSettings["Password"] == "")
            {
                Console.Write("Enter google username: ");
                user = Console.ReadLine();
                Console.Write("Enter google password: ");
                password = Console.ReadLine();
            }
            else
            {
                user = ConfigurationSettings.AppSettings["Username"];
                password = ConfigurationSettings.AppSettings["Password"];
            }
           
            GoogleFinanceManager googleFinanceManager = new GoogleFinanceManager(user, password);
            
            bool consoleRunning = true;
            while (consoleRunning)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter following keys to test each funtion.");
                Console.WriteLine("========================================================");
                Console.WriteLine("'L' = Load SP500.csv file");
                Console.WriteLine("'C' = Create portfolio with the loaded .csv file with blank transactions");
                Console.WriteLine("'c' = Create a portfolio");
                Console.WriteLine("'a' = Add a stock to a portfolio");
                Console.WriteLine("'r' = Remove a stock from a portfolio");
                Console.WriteLine("'p' = Portfolios Details");
                Console.WriteLine("'P' = Portfolio Stocks Details");
                Console.WriteLine("'l' = List Portfolios");
                Console.WriteLine("'s' = List Stocks from a portfolio");
                Console.WriteLine("'D' = Delete Portfolio");
                Console.WriteLine("'X' = Exit Program");
                Console.WriteLine("========================================================");
                Console.WriteLine();
                Console.WriteLine();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.KeyChar)
                {
                    #region 'L' Load csv file
                    case 'L':
                        Console.WriteLine("Enter .csv file name or just use default: [SP500.csv]");
                        s = Console.ReadLine();
                        csvFileName = (s == "" ? "sp500.csv" : s);

                        try
                        {
                            xmlDoc = new XElement("Root", File.ReadAllLines("sp500.csv").Select
                            (
                                line =>
                                {
                                    var split = line.CsvSplit();
                                    return new XElement("Stock",
                                        new XElement("Ticker", split[0]),
                                        new XElement("Stock_Name", split[1]),
                                        new XElement("Sector_code", split[2]),
                                        new XElement("Sector_Name", split[3]),
                                        new XElement("Industry_Group_Code", split[4]),
                                        new XElement("Industry_Group_Name", split[5]),
                                        new XElement("Industry_Code", split[6]),
                                        new XElement("Industry_Name", split[7]),
                                        new XElement("Sub_Ind_Code", split[8]),
                                        new XElement("Sub_Ind_Name", split[9])

                                    );
                                }
                            ));

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception Parsing the .csv file: ", ex.Message);
                        }
                        break;
                    #endregion 

                    #region 'C' Create portfolio with loaded .csv file
                    case 'C':
                        if (xmlDoc == null)
                        {
                            Console.WriteLine("Please load the .csv file first with 'L' command");
                            break;
                        }

                        // User enters name.
                        Console.WriteLine("Enter the portfolio name: [SP500] ");
                        s = Console.ReadLine();
                        portfolioName = (s == "" ? "SP500" : s);

                        // TODO: fix up the variable names. get rid of the code outside of this while loop

                        // Create the portfolio
                        portfolioEntry = googleFinanceManager.CreatePortfolio(portfolioName);

                        Func<XElement, TransactionEntry> anonFunc = delegate(XElement el)
                        {
                            string sym = el.Value;
                            YahooHelper y = new YahooHelper(sym);
                            string exc = y.Quote["stock_exchange"];

                            return googleFinanceManager.AddSymbol(exc, sym, portfolioEntry);
                        };
                              
                        var q = from v in xmlDoc.Elements("Stock")
                                select v;

                        List<TransactionEntry> transactions = new List<TransactionEntry>();
                        foreach (XElement elx in q.Elements("Ticker"))
                        {
                            transactions.Add(anonFunc(elx));
                        }

                        break;
                    #endregion 

                    #region 'D' Delete portolios
                    case 'D':
                        Console.WriteLine("Enter portfolio name or leave blank to delete all portfolios: ");
                        s = Console.ReadLine();
                        if (s == "")
                        {
                            // Delete all the portfolios.
                            if (googleFinanceManager.DeleteAllPortfolios())
                            {
                                Console.WriteLine("All portfolios were deleted");
                            }
                            else
                            {
                                Console.WriteLine("There was a problem with googleFinanceManager, not all portfolios were deleted.");
                            }
                        }
                        else
                        {
                            // Delete indivual stock.
                            if (!googleFinanceManager.DeletePortfolio(s))
                            {
                                Console.WriteLine("Portfolio was not deleted, no title with the name {0}", s);
                            }
                            else 
                            {
                                Console.WriteLine("Portfolio was deleted: {0}", s);
                            }
                        }
                        break;
                    #endregion 

                    #region 'c' Create a portfolio
                    case 'c':
                        Console.WriteLine("Enter the portfolio name: [My Example Portfolio] ");
                        s = Console.ReadLine();
                        if (s == "")
                        {
                            Console.WriteLine("Portfolio Not Created");
                            break;
                        }
                        else
                        {
                            portfolioName = s;
                        }

                        // Create the portfolio
                        portfolioEntry = googleFinanceManager.CreatePortfolio(portfolioName);

                        if (portfolioEntry != null)
                        {
                            Console.WriteLine("Portfolio Created = {0}", portfolioEntry.Title.Text);
                        }
                        else
                        {
                            Console.WriteLine("googleFinanceManager.CreatePortfolio return null, Portfolio not created");
                        }
                        break;
                    #endregion
 
                    #region 'a' Add a stock to a portfolio
                    case 'a':
                        // Print out a list of portfolios to choose from
                        portfolioIndex=0;
                        foreach (string t in googleFinanceManager.PortfolioNames)
                        {
                            Console.WriteLine("[{0}] {1}", portfolioIndex++, t);
                        }

                        // ask user to input which they would like to select.
                        Console.WriteLine("Choose a portfolio to add a symbol too: [0] ");
                        s = Console.ReadLine();
                        s = (s==""?"0":s);
                        portfolioIndex = int.Parse(s);

                        // ask user if they would like to create a transaction or a simple add.
                        Console.WriteLine("Would you like to create a transaction for this symbol: [y/n]");
                        s = Console.ReadLine();
                        if ((s == "" ? "n" : s) == "y")
                        {
                            TransactionDataArgs transactionDataArgs = new TransactionDataArgs()
                            {
                                TransactionType = TransactionTypes.BUY,
                                Price = 0.0f,
                                Commission = 0.0f,
                                Notes = "blahblah",
                                Date = DateTime.Now,
                                Shares = 100.00
                            };


                            // Create transaction
                            Console.WriteLine("Enter the full symbol name exchange:symbol: [NASDAQ:GOOG] ");
                            // TODO: need to implement the buying and selling of stock, not just adding it to a portfolio.
                            fullSymbolName = Console.ReadLine();

                            Console.WriteLine("Enter the type of transaction [0] Buy, [2] Buy to Cover, [3] Sell, [4] Sell Short: [0]");
                            s = Console.ReadLine();
                            s = (s == "" ? "0" : s);
                            switch (int.Parse(s))
                            {
                                case 0:
                                    transactionDataArgs.TransactionType = TransactionTypes.BUY;
                                    break;

                                case 1:
                                    transactionDataArgs.TransactionType = TransactionTypes.BUYTOCOVER;
                                    break;

                                case 2:
                                    transactionDataArgs.TransactionType = TransactionTypes.SELL;
                                    break;

                                case 3:
                                    transactionDataArgs.TransactionType = TransactionTypes.SELLSHORT;
                                    break;
                            }


                            Console.WriteLine("Enter the price to purchase the stock: [10.95]");
                            s = Console.ReadLine();
                            s = (s == "" ? "10.95" : s);
                            transactionDataArgs.Price = float.Parse(s);

                            Console.WriteLine("Enter the price of commission: [10.95]");
                            s = Console.ReadLine();
                            s = (s == "" ? "10.95" : s);
                            transactionDataArgs.Commission = float.Parse(s);

                            Console.WriteLine("Enter some notes about this purchase: [Going to make money on this stock]");
                            s = Console.ReadLine();
                            s = (s == "" ? "Going to make money on this stock" : s);

                            Console.WriteLine("What date did you purchase this stock: [Today]");
                            s = Console.ReadLine();
                            try
                            {
                                transactionDataArgs.Date = DateTime.Parse(s);
                            }
                            catch (Exception)
                            {
                                transactionDataArgs.Date = DateTime.Now;
                            }

                            Console.WriteLine("How many shares do you want to purchase: [100] ");
                            s = Console.ReadLine();
                            s = (s == "" ? "100" : s);
                            transactionDataArgs.Shares = double.Parse(s);

                            Console.WriteLine("Enter Currency Code: [USD] ");
                            s = Console.ReadLine();
                            s = (s == "" ? "USD" : s);
                            transactionDataArgs.CurrencyCode = s;


                            transactionEntry = googleFinanceManager.AddSymbol(fullSymbolName, transactionDataArgs, googleFinanceManager.Portfolios[googleFinanceManager.PortfolioNames[portfolioIndex]]);
                            if (transactionEntry != null)
                            {

                                Console.WriteLine("Symbol={0} has successfully been added to Portfolio={1}", fullSymbolName, transactionEntry.Title.Text);
                                Console.WriteLine("Type={0}, Date={1}, Shares={2}, Notes={3}, Commission={4}, Price={5}",
                                   transactionDataArgs.TransactionType,
                                   transactionDataArgs.Date,
                                   transactionDataArgs.Shares,
                                   transactionDataArgs.Notes,
                                   transactionDataArgs.Commission,
                                   transactionDataArgs.Price);
                            }
                        }
                        else
                        {
                            // add symbol to that list.
                            Console.WriteLine("Enter the full symbol name exchange:symbol: [NASDAQ:GOOG] ");
                            s = Console.ReadLine();
                            fullSymbolName = s;

                            transactionEntry = googleFinanceManager.AddSymbol(fullSymbolName, googleFinanceManager.PortfolioNames[portfolioIndex]);

                            if (transactionEntry != null)
                            {
                                Console.WriteLine("Symbol={0} has successfully been added to Portfolio={1}", fullSymbolName, transactionEntry.Title.Text);

                            }
 
                        }
                        break;
                    #endregion

                    #region 'r' Remove a stock from a portfolio
                    case 'r':
                        // Print out a list of portfolios to choose from
                        portfolioIndex = 0;
                        foreach (string t in googleFinanceManager.PortfolioNames)
                        {
                            Console.WriteLine("[{0}] {1}", portfolioIndex++, t);
                        }

                        // ask user to input which they would like to select.
                        Console.WriteLine("Choose a portfolio to delete a symbol from: [0] ");
                        s = Console.ReadLine();
                        s = (s == "" ? "0" : s);
                        portfolioIndex = int.Parse(s);

                        string symbolRemove;
                        symbols = googleFinanceManager.RetrieveSymbols(googleFinanceManager.PortfolioNames[portfolioIndex]);
                        foreach (var sym in symbols)
                        {
                            Console.WriteLine("[{0}]", sym.Key);
                        }
                        Console.WriteLine("Choose a symbol to delete: [] ");

                        s = Console.ReadLine();
                        symbolRemove = s;

                        googleFinanceManager.DeleteSymbol(symbols[symbolRemove]);
                        break;
                    #endregion

                    #region 'p' Portfolio Details
                    case 'p':
                        googleFinanceManager.PortfolioDetails = true;
                        googleFinanceManager.PositionDetails = true;
                        googleFinanceManager.TransactionDetails = true;

                        foreach (var portfolio in googleFinanceManager.Portfolios)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Portfolio Name: {0}", portfolio.Key);
                            Console.WriteLine("CurrencyCode: {0}", portfolio.Value.CurrencyCode);
                            Console.WriteLine("GainPercentage: {0}", portfolio.Value.GainPercentage);
                            Console.WriteLine("Return1Week: {0}", portfolio.Value.Return1Week);
                            Console.WriteLine("Return4Week: {0}", portfolio.Value.Return4Week);
                            Console.WriteLine("Return3Month: {0}", portfolio.Value.Return3Month);
                            Console.WriteLine("ReturnYTD: {0}", portfolio.Value.ReturnYTD);
                            Console.WriteLine("Return1Year: {0}", portfolio.Value.Return1Year);
                            Console.WriteLine("Return3Year: {0}", portfolio.Value.Return3Year);
                            Console.WriteLine("Return5Year: {0}", portfolio.Value.Return5Year);
                            Console.WriteLine("ReturnOverall: {0}", portfolio.Value.ReturnOverall);
                            Console.WriteLine("CostBasis: ");
                            if (portfolio.Value.CostBasis != null)
                            foreach (var m in portfolio.Value.CostBasis.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }
                            
                            Console.WriteLine("DaysGain:");
                            if (portfolio.Value.DaysGain != null)
                            foreach (var m in portfolio.Value.DaysGain.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }
                            Console.WriteLine("Gain:");
                            if (portfolio.Value.Gain != null)
                            foreach (var m in portfolio.Value.Gain.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }
                            Console.WriteLine("MarketValue:");
                            if (portfolio.Value.MarketValue != null)
                            foreach (var m in portfolio.Value.MarketValue.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }

                            Console.WriteLine(); Console.WriteLine();
                        }

                        break;
                    #endregion

                    #region 'P' Stock Details
                    case 'P':

                        googleFinanceManager.PortfolioDetails = true;
                        googleFinanceManager.PositionDetails = true;
                        googleFinanceManager.TransactionDetails = true;

                        portfolioIndex = 0;
                        foreach (string t in googleFinanceManager.PortfolioNames)
                        {
                            Console.WriteLine("[{0}] {1}", portfolioIndex++, t);
                        }

                        Console.WriteLine("Choose a portfolio to print details of there stocks: [0] ");
                        s = Console.ReadLine();
                        s = (s == "" ? "0" : s);
                        portfolioIndex = int.Parse(s);

                        symbols = googleFinanceManager.RetrieveSymbols(googleFinanceManager.PortfolioNames[portfolioIndex]);
                        Console.WriteLine("Portfolio Name: {0}", googleFinanceManager.PortfolioNames[portfolioIndex]);
                        foreach (var sym in symbols)
                        {
                            Console.WriteLine();

                            Console.WriteLine("TransactionHerf: {0}", sym.Value.TransactionHerf);
                            Console.WriteLine("Symbol.FullName: {0}", sym.Value.Symbol.FullName);
                            Console.WriteLine("Symbol.Exchange: {0}", sym.Value.Symbol.Exchange);
                            Console.WriteLine("Symbol.StockSymbol: {0}", sym.Value.Symbol.StockSymbol);

                            Console.WriteLine("DaysGain:");
                            if (sym.Value.DaysGain != null)
                            foreach (var m in sym.Value.DaysGain.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }

                            Console.WriteLine("CostBasis: ");
                            if (sym.Value.CostBasis != null)
                            foreach (var m in sym.Value.CostBasis.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }
                            
                            Console.WriteLine("Gain:");
                            if (sym.Value.Gain != null)
                            foreach (var m in sym.Value.Gain.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }

                            Console.WriteLine("MarketValue:");
                            if (sym.Value.MarketValue != null)
                            foreach (var m in sym.Value.MarketValue.Money)
                            {
                                Console.WriteLine("\tAmount={0:c}", m.Amount);
                                Console.WriteLine("\tAmount={0}\n", m.CurrencyCode);
                            }

                            Console.WriteLine("GainPercentage: {0}", sym.Value.GainPercentage);
                            Console.WriteLine("Return1Week: {0}", sym.Value.Return1Week);
                            Console.WriteLine("Return4Week: {0}", sym.Value.Return4Week);
                            Console.WriteLine("Return3Month: {0}", sym.Value.Return3Month);
                            Console.WriteLine("ReturnYTD: {0}", sym.Value.ReturnYTD);
                            Console.WriteLine("Return1Year: {0}", sym.Value.Return1Year);
                            Console.WriteLine("Return3Year: {0}", sym.Value.Return3Year);
                            Console.WriteLine("Return5Year: {0}", sym.Value.Return5Year);
                            Console.WriteLine("ReturnOverall: {0}", sym.Value.ReturnOverall);
                            Console.WriteLine("Shares: {0}", sym.Value.Shares);
                           
                            Console.WriteLine(); 
                        }
                        break;
                    #endregion

                    #region 'l' List Portfolios
                    case 'l':
                        Console.WriteLine("Printing Portfolios");
                        googleFinanceManager.PortfolioNames.ForEach(n => Console.WriteLine("Name: {0}", n));
                        break;
                    #endregion

                    #region 's' List Stocks from a portfolio
                    case 's':
                        // Print out a list of portfolios to choose from
                        portfolioIndex = 0;
                        foreach (string t in googleFinanceManager.PortfolioNames)
                        {
                            Console.WriteLine("[{0}] {1}", portfolioIndex++, t);
                        }

                        // ask user to input which they would like to select.
                        Console.WriteLine("Choose a portfolio to add a symbol too: [0] ");
                        s = Console.ReadLine();
                        s = (s == "" ? "0" : s);
                        portfolioIndex = int.Parse(s);

                        PortfolioEntry entry = googleFinanceManager.Portfolios[googleFinanceManager.PortfolioNames[portfolioIndex]];
                        var stockKeys = googleFinanceManager.RetrieveSymbols(entry).Keys;
                        foreach (var v in stockKeys) Console.WriteLine("Ticker Symbol: {0}",v);
                        
                        break;
                    #endregion

                    #region 'X' Exit Program
                    case 'X':
                        Console.WriteLine("Exiting SP500 Program");
                        return;
                    #endregion 
                }
            }
        }
    }
}
