using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Finance.Web.Example
{
    class Program
    {
        static string googleStockQuery = @"http://www.google.com/finance?q=";
        static string googleStockQueryCompanynews = "http://www.google.com/finance/company_news?q=";
        static string googleStockQueryRelatedCompanies = "http://www.google.com/finance/related?q=";
        static string googleStockQueryHistoricalPrices = "http://www.google.com/finance/historical?q=";
        static string googleStockQueryFinancials = "http://www.google.com/finance?q={0}&fstype=ii";

        static void Main(string[] args)
        {
            Utils.WriteLine(ConsoleColor.Cyan,"Finance.Web.Example");

            HtmlWeb htmlWeb = new HtmlWeb();
            List<string> listings = new List<string>() 
            {   @"NASDAQ:GOOG",
                @"NASDAQ:CSCO", 
                @"NYSE:HTZ",
                @"NYSE:MMM",
                @"NYSE:ACE",
                @"NYSE:AGN",
                @"NYSE:AOC",
                @"NASDAQ:AMGN",
                @"NYSE:AIZ",
                @"NYSE:HRB",
                @"NASDAQ:AAPL"
            };

            List<string> listingHistoricalPricesFiles = new List<string>()
            {
                @"C:\tmp\CSCO\HistoricalPrices.htm",
                @"C:\tmp\GOOG\HistoricalPrices.htm"
            };

            List<string> listingStockSummaryFiles = new List<string>()
            {
                @"C:\tmp\CSCO\Summary.htm",
                @"C:\tmp\GOOG\Summary.htm"
            };

            // Loading historical prices from file system.
            foreach (var file in listingHistoricalPricesFiles)
            {
                HtmlDocument document = new HtmlDocument();
                document.Load(file);
                HistoricalPrices historicalPrices = new HistoricalPrices(document);

                historicalPrices.Prices.ForEach(p =>
                    {
                        Utils.WriteLine(ConsoleColor.Cyan, "{0}", historicalPrices.Name);
                        Utils.WriteLine(ConsoleColor.Cyan, "Description={0}", historicalPrices.Description);
                        Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                        Utils.WriteLine(ConsoleColor.Blue, "Date={0}", p.Date);
                        Utils.WriteLine(ConsoleColor.Blue, "Open={0}", p.Open);
                        Utils.WriteLine(ConsoleColor.Blue, "Low={0}", p.Low);
                        Utils.WriteLine(ConsoleColor.Blue, "High={0}", p.High);
                        Utils.WriteLine(ConsoleColor.Blue, "Close={0}", p.Close);
                        Utils.WriteLine(ConsoleColor.Blue, "Volume={0}", p.Volume);
                        Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                    });

                Console.WriteLine("Press <Enter> ...");
                Console.ReadLine();
            }

            // Loading historical prices from the web
            foreach (var item in listings)
            {
                string[] s = item.Split(new char[] { ':' });
                HistoricalPrices historicalPrices = new HistoricalPrices(s[1], s[0]);


                historicalPrices.Prices.ForEach(p =>
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "{0}", historicalPrices.Name);
                    Utils.WriteLine(ConsoleColor.Cyan, "Description={0}", historicalPrices.Description);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                    Utils.WriteLine(ConsoleColor.Blue, "Date={0}", p.Date);
                    Utils.WriteLine(ConsoleColor.Blue, "Open={0}", p.Open);
                    Utils.WriteLine(ConsoleColor.Blue, "Low={0}", p.Low);
                    Utils.WriteLine(ConsoleColor.Blue, "High={0}", p.High);
                    Utils.WriteLine(ConsoleColor.Blue, "Close={0}", p.Close);
                    Utils.WriteLine(ConsoleColor.Blue, "Volume={0}", p.Volume);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                });

                Console.WriteLine("Press <Enter> ...");
                Console.ReadLine();

                s = item.Split(new char[] { ':' });
                historicalPrices = new HistoricalPrices(s[1], s[0]);
                historicalPrices.Refresh(new DateRange(new DateTime(2007, 1, 1), new DateTime(2009, 1, 1)),
                    1, HistoricalPeriod.Weekly, 200);

                historicalPrices.Prices.ForEach(p =>
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "{0}", historicalPrices.Name);
                    Utils.WriteLine(ConsoleColor.Cyan, "Description={0}", historicalPrices.Description);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                    Utils.WriteLine(ConsoleColor.Blue, "Date={0}", p.Date);
                    Utils.WriteLine(ConsoleColor.Blue, "Open={0}", p.Open);
                    Utils.WriteLine(ConsoleColor.Blue, "Low={0}", p.Low);
                    Utils.WriteLine(ConsoleColor.Blue, "High={0}", p.High);
                    Utils.WriteLine(ConsoleColor.Blue, "Close={0}", p.Close);
                    Utils.WriteLine(ConsoleColor.Blue, "Volume={0}", p.Volume);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                });

                Console.WriteLine("Press <Enter> ...");
                Console.ReadLine();

                s = item.Split(new char[] { ':' });
                historicalPrices = new HistoricalPrices(s[1], s[0]);
                historicalPrices.Refresh(new DateRange(new DateTime(2005, 2, 23), new DateTime(2008, 6, 3)),
                    1, HistoricalPeriod.Daily, 200);

                historicalPrices.Prices.ForEach(p =>
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "{0}", historicalPrices.Name);
                    Utils.WriteLine(ConsoleColor.Cyan, "Description={0}", historicalPrices.Description);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                    Utils.WriteLine(ConsoleColor.Blue, "Date={0}", p.Date);
                    Utils.WriteLine(ConsoleColor.Blue, "Open={0}", p.Open);
                    Utils.WriteLine(ConsoleColor.Blue, "Low={0}", p.Low);
                    Utils.WriteLine(ConsoleColor.Blue, "High={0}", p.High);
                    Utils.WriteLine(ConsoleColor.Blue, "Close={0}", p.Close);
                    Utils.WriteLine(ConsoleColor.Blue, "Volume={0}", p.Volume);
                    Utils.WriteLine(ConsoleColor.Cyan, "==================================");
                });

                Console.WriteLine("Press <Enter> ...");
                Console.ReadLine();

            }

            // Loading from the web.
            foreach (var item in listings)
            {
                HtmlDocument document = htmlWeb.Load(googleStockQuery + item);

                #region StockSummary Example
                StockSummary stockSummary = new StockSummary(document);
                Utils.WriteLine(ConsoleColor.Cyan, "RefernceId : " + stockSummary.RefernceId);

                Utils.WriteLine(ConsoleColor.Cyan, "Company News.Text: " + stockSummary.CompanyNews.Text);
                Utils.WriteLine(ConsoleColor.Cyan, "Company News.Link: " + stockSummary.CompanyNews.Link.ToString());

                Utils.WriteLine(ConsoleColor.Cyan, "CompanySection.CompanyDescription: " + stockSummary.CompanySection.CompanyDescriptsion);
                Utils.WriteLine(ConsoleColor.Cyan, "CompanySection.ExternalCompanyProfile.Text: " + stockSummary.CompanySection.ExternalCompanyProfile.Text);
                Utils.WriteLine(ConsoleColor.Cyan, "CompanySection.EternalCompanyProfile.Link: " + stockSummary.CompanySection.ExternalCompanyProfile.Link.ToString());

                stockSummary.Mangement.ForEach(delegate(Mangement n)
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "Name: " + n.Name);
                    Utils.WriteLine(ConsoleColor.Cyan, "Age : " + n.Age);
                    Utils.WriteLine(ConsoleColor.Cyan, "Title : " + n.Title);
                    Utils.WriteLine(ConsoleColor.Cyan, "ProfileLink : " + n.ProfileLink);
                    Utils.WriteLine(ConsoleColor.Cyan, "");
                });

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Sector.Text : " + stockSummary.Sector.Text);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Sector.Link : " + stockSummary.Sector.Link);

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Industry.Text : " + stockSummary.Industry.Text);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Industry.Link : " + stockSummary.Industry.Link);


                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ListingPrice : " + stockSummary.ListingPrice);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ListingChangePrice : " + stockSummary.ListingChangePrice);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ListingChangePricePercentage : {0}%", stockSummary.ListingChangePricePercentage);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ExtendedListingPrice : " + stockSummary.ExtendedListingPrice);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ExtendedChangeListingPrice : " + stockSummary.ExtendedChangeListingPrice);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ExtendedChangeListingPricePercentage : " + stockSummary.ExtendedChangeListingPricePercentage);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.ExtendedListingTime : " + stockSummary.ExtendedListingTime);

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.CompanyHeader.CompanyName : " + stockSummary.CompanyHeader.CompanyName);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.CompanyHeader.ListingType : " + stockSummary.CompanyHeader.ListingType);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.CompanyHeader.ListingSymbol : " + stockSummary.CompanyHeader.ListingSymbol);


                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Range : {0} ", stockSummary.Range.ToString());
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.FiftyTwoWeek : {0} ", stockSummary.FiftyTwoWeek.ToString());
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Open : {0} ", stockSummary.Open);

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Volume : {0} ", stockSummary.Volume);
                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Average : {0} ", stockSummary.Average);

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.MarketCapital : {0} ", stockSummary.MarketCapital);


                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.P_E : {0} ", stockSummary.P_E);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.P_E : {0} ", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Dividend : {0}", stockSummary.Dividend);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.Dividend : {0}", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Yield : {0} ", stockSummary.Yield);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.Yield : {0} ", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.EPS : {0} ", stockSummary.EPS);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.EPS : {0} ", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Shares : {0} ", stockSummary.Shares);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.Shares : {0} ", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.Beta : {0} ", stockSummary.Beta);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.Beta : {0} ", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                try
                {
                    Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.InterestOwned : {0} %", stockSummary.InterestOwned);
                }
                catch (DataNotAvailable ex)
                {
                    Utils.WriteLine(ConsoleColor.Red, "stockSummary.InterestOwned : {0} %", "-");
                    Utils.WriteLine(ConsoleColor.DarkMagenta, ex.PotentialReason);
                }

                Utils.WriteLine(ConsoleColor.Cyan, "stockSummary.HomePage : {0} ", stockSummary.HomePage.ToString());
                #endregion 

                #region HistoricalPrices Example
                #endregion

                #region RelatedCompanies Example
                #endregion 

                #region StockFinancials Example
                #endregion 

                #region StockNews Example
                #endregion 

                Console.WriteLine("Press <Enter> ...");
                Console.ReadLine();

            }

            Utils.WriteLine(ConsoleColor.Red, "Press <Enter> to Exit ...");
            Console.ReadLine();
        }
    }
}
