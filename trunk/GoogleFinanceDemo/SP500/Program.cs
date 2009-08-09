using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
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



            Console.Write("Enter google username: ");
            string user = Console.ReadLine();

            Console.Write("Enter google password: ");
            string password = Console.ReadLine();

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
                Console.WriteLine("'p' = Portfolio Details");
                Console.WriteLine("'P' = Stock Details");
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
                            // Create transaction
                            Console.WriteLine("Enter the full symbol name exchange:symbol: [NASDAQ:GOOG] ");
                            // TODO: need to implement the buying and selling of stock, not just adding it to a portfolio.
                            Console.WriteLine("NOT YET IMPLEMENTED");
                        }
                        else
                        {
                            // add symbol to that list.
                            Console.WriteLine("Enter the full symbol name exchange:symbol: [NASDAQ:GOOG] ");
                            s = Console.ReadLine();
                            string fullSymbolName = s;

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
                        Dictionary<string, PositionEntry> symbols = googleFinanceManager.RetrieveSymbols(googleFinanceManager.PortfolioNames[portfolioIndex]);
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

                        break;
                    #endregion

                    #region 'P' Stock Details
                    case 'P':

                        break;
                    #endregion

                    #region
                    #endregion

                    #region 'X' Exit Program
                    case 'X':
                        Console.WriteLine("Exiting SP500 Program");
                        return;
                        break;
                    #endregion 
                }

              

            }
        }
    }
}
