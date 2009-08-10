using Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Google.GData.Client;
using System.Text;
using Google.GData.Extensions;

/////////////////////////////
// TODO: Create Real Tests //
/////////////////////////////

namespace TestFinanceObjects
{
    /// <summary>
    ///This is a test class for ApplicationDriverTest and is intended
    ///to contain all ApplicationDriverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ApplicationDriverTest
    {
        public static string user = "USERNAME";
        string password = "PASSWORD";

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

        [TestMethod()]
        public void CurrentPortfolioTransactionsTest()
        {
            ApplicationDriver target = new ApplicationDriver(user, password);

            string curPort = target.CurrentPortfolioName;
            target.CurrentPortfolioName = "My Portfolio";
            Console.WriteLine(curPort);

            List<PositionEntry> positions;
            positions = target.CurrentPositions;
            foreach (PositionEntry a in positions)
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

                //// XXX: Need to put for loops around the money objects cause there could be more then one.
                //Console.WriteLine("DaysGain Money = " + a.DaysGain.Money.Amount);
                //Console.WriteLine("DaysGain Currency Code = " + a.DaysGain.Money.CurrencyCode);

                //Console.WriteLine("CostBasis Money = " + a.CostBasis.Money.Amount);
                //Console.WriteLine("CostBasis Currency Code = " + a.CostBasis.Money.CurrencyCode);

                //Console.WriteLine("Gain Money = " + a.Gain.Money.Amount);
                //Console.WriteLine("Gain Currency Code = " + a.Gain.Money.CurrencyCode);

                //Console.WriteLine("MarketValue Money = " + a.MarketValue.Money.Amount);
                //Console.WriteLine("MarketValue Currency Code = " + a.MarketValue.Money.CurrencyCode);


                Console.WriteLine("Symbol StockSymbol = " + a.Symbol.StockSymbol);
                Console.WriteLine("Symbol Exchange = " + a.Symbol.Exchange);
                Console.WriteLine("Symbol FullName = " + a.Symbol.FullName);

                Console.WriteLine("FeedLink feedlink = " + a.FeedLink.Href);

                //Console.WriteLine("CurrencyCode = " + a.CurrencyCode);
                Console.WriteLine("============================TRANSACTIONS============================");

                Console.WriteLine("Transactions");

                //foreach (TransactionEntry te in a.Transactions)
                //{
                //    Console.WriteLine("Id = " + te.Id.AbsoluteUri);
                //}

                

                Console.WriteLine("==========================END TRANSACTIONS==========================");

                Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
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
            string curPort = target.CurrentPortfolioName;
            target.CurrentPortfolioName = "My Portfolio";
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
                //Console.WriteLine("DaysGain Money = " + a.DaysGain.Money.Amount);
                //Console.WriteLine("DaysGain Currency Code = " + a.DaysGain.Money.CurrencyCode);

                //Console.WriteLine("CostBasis Money = " + a.CostBasis.Money.Amount);
                //Console.WriteLine("CostBasis Currency Code = " + a.CostBasis.Money.CurrencyCode);

                //Console.WriteLine("Gain Money = " + a.Gain.Money.Amount);
                //Console.WriteLine("Gain Currency Code = " + a.Gain.Money.CurrencyCode);

                //Console.WriteLine("MarketValue Money = " + a.MarketValue.Money.Amount);
                //Console.WriteLine("MarketValue Currency Code = " + a.MarketValue.Money.CurrencyCode);


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
            target.CurrentPortfolioName = "My Portfolio";
            PortfolioEntry pe = target.CurrentPortfolioEntry;
            Console.WriteLine(pe.Title.Text);
            //Console.WriteLine(pe.CurrencyCode);
            //Console.WriteLine(pe.GainPercentage);
            //Console.WriteLine(pe.Return3Month);
            //Console.WriteLine(pe.ReturnYTD);
            //Console.WriteLine(pe.Return1Year);
            //Console.WriteLine(pe.Return3Year);
            //Console.WriteLine(pe.Return5Year);
            //Console.WriteLine(pe.ReturnOverall);
            //Console.WriteLine(pe.Return4Week);
            //Console.WriteLine(pe.Return1Week);
        }

        /// <summary>
        ///A test for CreatePortfolio
        ///</summary>
        [TestMethod()]
        public void CreatePortfolioTest()
        {
            ApplicationDriver target = new ApplicationDriver(user, password);

            string curPort = target.CurrentPortfolioName;
            target.CurrentPortfolioName = "My Portfolio";
            Console.WriteLine(curPort);

            List<PositionEntry> positions;
            positions = target.CurrentPositions;

            //target.CreatePortfolio();
        }

        [TestMethod()]
        public void PortfolioParseTest()
        {
            string xml = "<?xml version='1.0' encoding='UTF-8'?><feed xmlns='http://www.w3.org/2005/Atom' xmlns:openSearch='http://a9.com/-/spec/opensearchrss/1.0/' xmlns:gf='http://schemas.google.com/finance/2007' xmlns:gd='http://schemas.google.com/g/2005'><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#portfolio'/><title type='text'>Portfolio Feed</title><link rel='alternate' type='text/html' href='http://finance.google.com/finance/portfolio?action=view'/><link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><link rel='http://schemas.google.com/g/2005#post' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios?positions=true&amp;returns=true'/><openSearch:totalResults>1</openSearch:totalResults><openSearch:startIndex>1</openSearch:startIndex><openSearch:itemsPerPage>1</openSearch:itemsPerPage><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#portfolio'/><title type='text'>My Portfolio</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/1'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/1'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions'><feed><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>My Portfolio</title><link rel='alternate' type='text/html' href='http://finance.google.com/finance/portfolio?action=view&amp;pid=1'/><link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><openSearch:totalResults>9</openSearch:totalResults><openSearch:itemsPerPage>9</openSearch:itemsPerPage><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:MSFT</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Microsoft Corporation</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AMSFT'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AMSFT'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:MSFT/transactions'/><gf:positionData gainPercentage='20.82242991' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='1000.0'><gf:costBasis><gd:money amount='1070.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-440.001' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='22280.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='23350.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Microsoft Corporation' symbol='MSFT'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:AAPL</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Apple Inc.</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AAAPL'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AAAPL'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:AAPL/transactions'/><gf:positionData gainPercentage='355.1' return1w='0.02122168053' return1y='355.1' return3m='355.1' return3y='355.1' return4w='355.1' return5y='355.1' returnOverall='355.1' returnYTD='355.1' shares='100.0'><gf:costBasis><gd:money amount='40.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='258.0002' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='14204.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='14244.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Apple Inc.' symbol='AAPL'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:ACAS</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>American Capital Ltd.</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AACAS'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AACAS'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:ACAS/transactions'/><gf:positionData gainPercentage='0.1045296167' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='10000.0'><gf:costBasis><gd:money amount='28700.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='4500.0' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='3000.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='31700.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='American Capital Ltd.' symbol='ACAS'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:ING</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>ING Groep N.V. (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AING'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AING'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:ING/transactions'/><gf:positionData gainPercentage='0.05503634476' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='124.0'><gf:costBasis><gd:money amount='1194.12' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-2.48' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='65.72' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='1259.84' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='ING Groep N.V. (ADR)' symbol='ING'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:FORTY</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Formula Systems (1985) Ltd. (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AFORTY'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AFORTY'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:FORTY/transactions'/><gf:positionData gainPercentage='0.03818953324' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='130.0'><gf:costBasis><gd:money amount='919.1' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='17.55' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='35.1' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='954.2' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Formula Systems (1985) Ltd. (ADR)' symbol='FORTY'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:DT</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Deutsche Telekom AG (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ADT'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ADT'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:DT/transactions'/><gf:positionData gainPercentage='0.01224846894' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='242.0'><gf:costBasis><gd:money amount='2766.06' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='14.519758' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='33.88' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='2799.94' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='Deutsche Telekom AG (ADR)' symbol='DT'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:CHN</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>China Fund Inc. (The)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ACHN'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ACHN'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:CHN/transactions'/><gf:positionData gainPercentage='0.03348104382' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='244.0'><gf:costBasis><gd:money amount='4955.64' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='7.320244' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='165.92' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='5121.56' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='China Fund Inc. (The)' symbol='CHN'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:FTE</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>France Telecom SA (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AFTE'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AFTE'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:FTE/transactions'/><gf:positionData gainPercentage='0.02424786709' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='420.0'><gf:costBasis><gd:money amount='9353.4' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-42.0' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='226.8' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='9580.2' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='France Telecom SA (ADR)' symbol='FTE'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:GNK</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Genco Shipping &amp; Trading Limited</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AGNK'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AGNK'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:GNK/transactions'/><gf:positionData gainPercentage='0.1024649589' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='10.0'><gf:costBasis><gd:money amount='206.9' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='14.69999' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='21.2' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='228.1' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='Genco Shipping &amp; Trading Limited' symbol='GNK'/></entry></feed></gd:feedLink><gf:portfolioData currencyCode='USD' gainPercentage='0.8135848188' return1w='0.02122168053' return1y='355.1' return3m='355.1' return3y='355.1' return4w='355.1' return5y='355.1' returnOverall='355.1' returnYTD='355.1'><gf:costBasis><gd:money amount='49205.22' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='4327.609192' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='40032.62' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='140032.62' currencyCode='USD'/></gf:marketValue></gf:portfolioData></entry></feed>";

            PortfolioFeed feed = Parse(xml);
            //PortfolioEntry entry = feed.Entries[0] as PortfolioEntry;
            PortfolioEntry enTest = feed.Entries[0] as PortfolioEntry;
            //enTest.portfolioDataElement.CurrencyCode

            ExtensionCollection<Money> i = enTest.PortfolioData.CostBasis.Money;

            ExtensionCollection<Money> ii = enTest.PortfolioData.DaysGain.Money;
            ExtensionCollection<Money> iii = enTest.PortfolioData.Gain.Money;
            ExtensionCollection<Money> iiii = enTest.PortfolioData.MarketValue.Money;

            foreach (Money m in ii)
            {
                Console.WriteLine(m.Amount);
                Console.WriteLine(m.CurrencyCode);
            }

            foreach (Money m in iii)
            {
                Console.WriteLine(m.Amount);
                Console.WriteLine(m.CurrencyCode);
            }

            foreach (Money m in iiii)
            {
                Console.WriteLine(m.Amount);
                Console.WriteLine(m.CurrencyCode);
            }

            PositionFeed feed2 = ParsePositionFeed(xml);
            PositionEntry enPositionTest = feed.Entries[0] as PositionEntry;


        }

        [TestMethod()]
        public void TestPositionEntry2()
        {
            //PositionEntry pe;
            PositionFeed pf;
            FinanceService service = new FinanceService("test");
            service.setUserCredentials(user, password);
            GDataLoggingRequestFactory factoryLogging = new GDataLoggingRequestFactory("finance", ApplicationDriver.Name);
            factoryLogging.MethodOverride = true;
            factoryLogging.CombinedLogFileName = @"c:\xmllog.log";
            service.RequestFactory = factoryLogging;

            //pf = new PositionFeed(new Uri("http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions"), service);
            pf = service.Query(new PositionQuery("http://finance.google.com/finance/feeds/default/portfolios/1/positions?returns=true"));

            foreach (PositionEntry p in pf.Entries)
            {
                Console.WriteLine(p.PositionData.Shares);
                Console.WriteLine(p.TransactionHerf);

                TransactionFeed tf = service.Query(new TransactionQuery(p.TransactionHerf + "?returns=true"));
                foreach (TransactionEntry te in tf.Entries)
                {
                    Console.WriteLine(tf.Feed);
                }
            }


            
        }

        private PositionFeed ParsePositionFeed(string xml)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(xml);
            PositionFeed feed = new PositionFeed(new Uri("http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions"), new FinanceService("Test"));
            feed.Parse(new MemoryStream(bytes), AlternativeFormat.Atom);
            return feed;
        }
        private PortfolioFeed Parse(string xml)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(xml);
            PortfolioFeed feed = new PortfolioFeed(new Uri("http://finance.google.com/finance/feeds/default/portfolios"), new FinanceService("Test"));
            feed.Parse(new MemoryStream(bytes), AlternativeFormat.Atom);
            return feed;
        }
    }
}
