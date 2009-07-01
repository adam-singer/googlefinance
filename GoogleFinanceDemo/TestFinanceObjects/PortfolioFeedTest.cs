using Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Google.GData.Client;
using System.Text;
using System.IO;

namespace TestFinanceObjects
{
    
    
    /// <summary>
    ///This is a test class for PortfolioFeedTest and is intended
    ///to contain all PortfolioFeedTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PortfolioFeedTest
    {


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
        ///A test for PortfolioFeed Constructor
        ///</summary>
        [TestMethod()]
        public void PortfolioFeedConstructorTest()
        {
            Uri uriBase = null; // TODO: Initialize to an appropriate value
            IService iService = null; // TODO: Initialize to an appropriate value
            PortfolioFeed target = new PortfolioFeed(uriBase, iService);
            Assert.AreNotEqual(target, null);
        }

        /// <summary>
        ///A test for CreateFeedEntry
        ///</summary>
        [TestMethod()]
        public void CreateFeedEntryTest()
        {

             string xml = "<?xml version='1.0' encoding='UTF-8'?><feed xmlns='http://www.w3.org/2005/Atom' xmlns:openSearch='http://a9.com/-/spec/opensearchrss/1.0/' xmlns:gf='http://schemas.google.com/finance/2007' xmlns:gd='http://schemas.google.com/g/2005'><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#portfolio'/><title type='text'>Portfolio Feed</title><link rel='alternate' type='text/html' href='http://finance.google.com/finance/portfolio?action=view'/><link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><link rel='http://schemas.google.com/g/2005#post' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios?positions=true&amp;returns=true'/><openSearch:totalResults>1</openSearch:totalResults><openSearch:startIndex>1</openSearch:startIndex><openSearch:itemsPerPage>1</openSearch:itemsPerPage><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#portfolio'/><title type='text'>My Portfolio</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/1'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/1'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions'><feed><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>My Portfolio</title><link rel='alternate' type='text/html' href='http://finance.google.com/finance/portfolio?action=view&amp;pid=1'/><link rel='http://schemas.google.com/g/2005#feed' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios'/><openSearch:totalResults>9</openSearch:totalResults><openSearch:itemsPerPage>9</openSearch:itemsPerPage><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:MSFT</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Microsoft Corporation</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AMSFT'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AMSFT'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:MSFT/transactions'/><gf:positionData gainPercentage='20.82242991' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='1000.0'><gf:costBasis><gd:money amount='1070.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-440.001' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='22280.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='23350.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Microsoft Corporation' symbol='MSFT'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:AAPL</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Apple Inc.</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AAAPL'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AAAPL'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:AAPL/transactions'/><gf:positionData gainPercentage='355.1' return1w='0.02122168053' return1y='355.1' return3m='355.1' return3y='355.1' return4w='355.1' return5y='355.1' returnOverall='355.1' returnYTD='355.1' shares='100.0'><gf:costBasis><gd:money amount='40.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='258.0002' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='14204.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='14244.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Apple Inc.' symbol='AAPL'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:ACAS</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>American Capital Ltd.</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AACAS'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AACAS'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:ACAS/transactions'/><gf:positionData gainPercentage='0.1045296167' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='10000.0'><gf:costBasis><gd:money amount='28700.0' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='4500.0' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='3000.0' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='31700.0' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='American Capital Ltd.' symbol='ACAS'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:ING</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>ING Groep N.V. (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AING'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AING'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:ING/transactions'/><gf:positionData gainPercentage='0.05503634476' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='124.0'><gf:costBasis><gd:money amount='1194.12' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-2.48' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='65.72' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='1259.84' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='ING Groep N.V. (ADR)' symbol='ING'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:FORTY</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Formula Systems (1985) Ltd. (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AFORTY'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NASDAQ%3AFORTY'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NASDAQ:FORTY/transactions'/><gf:positionData gainPercentage='0.03818953324' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='130.0'><gf:costBasis><gd:money amount='919.1' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='17.55' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='35.1' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='954.2' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NASDAQ' fullName='Formula Systems (1985) Ltd. (ADR)' symbol='FORTY'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:DT</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Deutsche Telekom AG (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ADT'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ADT'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:DT/transactions'/><gf:positionData gainPercentage='0.01224846894' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='242.0'><gf:costBasis><gd:money amount='2766.06' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='14.519758' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='33.88' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='2799.94' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='Deutsche Telekom AG (ADR)' symbol='DT'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:CHN</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>China Fund Inc. (The)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ACHN'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3ACHN'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:CHN/transactions'/><gf:positionData gainPercentage='0.03348104382' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='244.0'><gf:costBasis><gd:money amount='4955.64' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='7.320244' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='165.92' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='5121.56' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='China Fund Inc. (The)' symbol='CHN'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:FTE</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>France Telecom SA (ADR)</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AFTE'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AFTE'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:FTE/transactions'/><gf:positionData gainPercentage='0.02424786709' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='420.0'><gf:costBasis><gd:money amount='9353.4' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='-42.0' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='226.8' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='9580.2' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='France Telecom SA (ADR)' symbol='FTE'/></entry><entry><id>http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:GNK</id><updated>2009-06-27T05:40:34.000Z</updated><category scheme='http://schemas.google.com/g/2005#kind' term='http://schemas.google.com/finance/2007#position'/><title type='text'>Genco Shipping &amp; Trading Limited</title><link rel='self' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AGNK'/><link rel='edit' type='application/atom+xml' href='http://finance.google.com/finance/feeds/default/portfolios/NYSE%3AGNK'/><gd:feedLink href='http://finance.google.com/finance/feeds/financeCoding@gmail.com/portfolios/1/positions/NYSE:GNK/transactions'/><gf:positionData gainPercentage='0.1024649589' return1w='0.0' return1y='0.0' return3m='0.0' return3y='0.0' return4w='0.0' return5y='0.0' returnOverall='0.0' returnYTD='0.0' shares='10.0'><gf:costBasis><gd:money amount='206.9' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='14.69999' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='21.2' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='228.1' currencyCode='USD'/></gf:marketValue></gf:positionData><gf:symbol exchange='NYSE' fullName='Genco Shipping &amp; Trading Limited' symbol='GNK'/></entry></feed></gd:feedLink><gf:portfolioData currencyCode='USD' gainPercentage='0.8135848188' return1w='0.02122168053' return1y='355.1' return3m='355.1' return3y='355.1' return4w='355.1' return5y='355.1' returnOverall='355.1' returnYTD='355.1'><gf:costBasis><gd:money amount='49205.22' currencyCode='USD'/></gf:costBasis><gf:daysGain><gd:money amount='4327.609192' currencyCode='USD'/></gf:daysGain><gf:gain><gd:money amount='40032.62' currencyCode='USD'/></gf:gain><gf:marketValue><gd:money amount='140032.62' currencyCode='USD'/></gf:marketValue></gf:portfolioData></entry></feed>";


            PortfolioFeed target = Parse(xml);


            
            for(int i=0; i<target.Entries.Count; i++)
            {
                PortfolioEntry portfolioEntry = target.Entries[i] as PortfolioEntry;


                Console.WriteLine("Title: " + portfolioEntry.Title.Text);
                Console.WriteLine("FeedLink: " + portfolioEntry.FeedLink.ToString());
                Console.WriteLine("CurrencyCode: " + portfolioEntry.CurrencyCode);
                Console.WriteLine("GainPercentage: " + portfolioEntry.GainPercentage.ToString("0.00000000%"));
                Console.WriteLine("Return1Week: " + portfolioEntry.Return1Week.ToString("0.00000000%"));
                Console.WriteLine("Return4Week: " + portfolioEntry.Return4Week.ToString("0.00000000%"));
                Console.WriteLine("Return3Month: " + portfolioEntry.Return3Month.ToString("0.00000000%"));
                Console.WriteLine("ReturnYTD: " + portfolioEntry.ReturnYTD.ToString("0.00000000%"));
                Console.WriteLine("Return1Year: " + portfolioEntry.Return1Year.ToString("0.00000000%"));
                Console.WriteLine("Return3Year: " + portfolioEntry.Return3Year.ToString("0.00000000%"));
                Console.WriteLine("Return5Year: " + portfolioEntry.Return5Year.ToString("0.00000000%"));
                Console.WriteLine("ReturnOverall: " + portfolioEntry.ReturnOverall.ToString("0.00000000%"));

                CostBasis costBasis = portfolioEntry.CostBasis;
                foreach (Money m in costBasis.Money)
                {
                    Console.WriteLine("Money: Amount = " + m.Amount as string);
                    Console.WriteLine("Money: CurrencyCode = " + m.CurrencyCode);
                }

                DaysGain daysGain = portfolioEntry.DaysGain;
                foreach (Money m in daysGain.Money)
                {
                    Console.WriteLine("Money: Amount = " + m.Amount as string);
                    Console.WriteLine("Money: CurrencyCode = " + m.CurrencyCode);
                }

                Gain gain = portfolioEntry.Gain;
                foreach (Money m in gain.Money)
                {
                    Console.WriteLine("Money: Amount = " + m.Amount as string);
                    Console.WriteLine("Money: CurrencyCode = " + m.CurrencyCode);
                }

                MarketValue marketValue = portfolioEntry.MarketValue;
                foreach (Money m in marketValue.Money)
                {
                    Console.WriteLine("Money: Amount = " + m.Amount as string);
                    Console.WriteLine("Money: CurrencyCode = " + m.CurrencyCode);
                }
            }
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
