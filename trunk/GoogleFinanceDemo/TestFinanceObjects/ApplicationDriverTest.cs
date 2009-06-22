using Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestFinanceObjects
{
    /// <summary>
    ///This is a test class for ApplicationDriverTest and is intended
    ///to contain all ApplicationDriverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ApplicationDriverTest
    {
        public static string user = "YOUNEEDTOSETME";
        string password = "YOUNEEDTOSETME";

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ApplicationDriver Constructor
        ///</summary>
        [TestMethod()]
        public void ApplicationDriverConstructorTest()
        {
            
            ApplicationDriver target = new ApplicationDriver(user, password);
            Console.WriteLine(target.Portfolios.Count);
            Console.WriteLine(target.Portfolios.ToString());
            
        }

        /// <summary>
        ///A test for Portfolios
        ///</summary>
        [TestMethod()]
        public void PortfoliosTest()
        {
            
            ApplicationDriver target = new ApplicationDriver(user, password);

            List<string> actual;
            actual = target.Portfolios;
            foreach (string a in actual)
            {
                Console.WriteLine(a);
            }
            
        }

        /// <summary>
        ///A test for CurrentPositions
        ///</summary>
        [TestMethod()]
        public void CurrentPositionsTest()
        {
           
            ApplicationDriver target = new ApplicationDriver(user, password);

            List<PositionEntry> actual;
            string curPort = target.CurrentPortfolio;
            target.CurrentPortfolio = "My Portfolio";
            Console.WriteLine(curPort);
            actual = target.CurrentPositions;
            foreach (PositionEntry a in actual)
            {
                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
                Console.WriteLine(a);
                Console.WriteLine("Title = " + a.Title.Text);
                Console.WriteLine("GainPercentage = " + a.GainPercentage);
                Console.WriteLine("Return1Week = " + a.Return1Week);
                Console.WriteLine("Return1Year = " + a.Return1Year);
                Console.WriteLine("Return3Month = " + a.Return3Month);
                Console.WriteLine("Return3Year = " + a.Return3Year);
                Console.WriteLine("Return4Week = " + a.Return4Week);
                Console.WriteLine("Return5Year = " + a.Return5Year);
                Console.WriteLine("ReturnOverall = " + a.ReturnOverall);
                Console.WriteLine("Shares = " + a.Shares);
                Console.WriteLine("ReturnYTD = " + a.ReturnYTD);
               
                // XXX: Need to put for loops around the money objects cause there could be more then one.
                Console.WriteLine("DaysGain Money = " + a.DaysGain.Money.Amount);
                Console.WriteLine("DaysGain Currency Code = " + a.DaysGain.Money.CurrencyCode);

                Console.WriteLine("CostBasis Money = " + a.CostBasis.Money.Amount);
                Console.WriteLine("CostBasis Currency Code = " + a.CostBasis.Money.CurrencyCode);

                Console.WriteLine("Gain Money = " + a.Gain.Money.Amount);
                Console.WriteLine("Gain Currency Code = " + a.Gain.Money.CurrencyCode);

                Console.WriteLine("MarketValue Money = " + a.MarketValue.Money.Amount);
                Console.WriteLine("MarketValue Currency Code = " + a.MarketValue.Money.CurrencyCode);


                Console.WriteLine("Symbol StockSymbol = " + a.Symbol.StockSymbol);
                Console.WriteLine("Symbol Exchange = " + a.Symbol.Exchange);
                Console.WriteLine("Symbol FullName = " + a.Symbol.FullName);

                Console.WriteLine("FeedLink feedlink = " + a.FeedLink.Href);

                //Console.WriteLine("CurrencyCode = " + a.CurrencyCode);
                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            }
            
        }

        /// <summary>
        ///A test for CurrentPortfolio
        ///</summary>
        [TestMethod()]
        public void CurrentPortfolioTest()
        {
            ApplicationDriver target = new ApplicationDriver(user, password);
            target.CurrentPortfolio = "My Portfolio";
            PortfolioEntry pe = target.CurrentPortfolioEntry;
            Console.WriteLine(pe.Title.Text);
            Console.WriteLine(pe.CurrencyCode);
            Console.WriteLine(pe.GainPercentage);
            Console.WriteLine(pe.Return3Month);
            Console.WriteLine(pe.ReturnYTD);
            Console.WriteLine(pe.Return1Year);
            Console.WriteLine(pe.Return3Year);
            Console.WriteLine(pe.Return5Year);
            Console.WriteLine(pe.ReturnOverall);
            Console.WriteLine(pe.Return4Week);
            Console.WriteLine(pe.Return1Week);
        }
    }
}
