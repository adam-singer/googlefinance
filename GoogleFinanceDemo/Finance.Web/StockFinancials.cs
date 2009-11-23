using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
namespace Finance.Web
{

    // id="fs-type-tabs" 
    // This id provides us with the three different tabs on the financials page,
    // "income statement", "balance sheet", "cash flow"

    // Each tab has two views: Quarterly Data |  Annual Data
    // Both views have id's:
    // <A id="interim" class="ac">Quarterly Data</A>&nbsp;|&nbsp;
    // <A id="annual" class="nac">Annual Data</A>

    // Income Statement Quarterly and Annual have the same number of row elements (?)
    // Income Statement Quarterly has five column of data, six column total inclding the element names.
    // Income Statement Annual has has four column of data, five column total including the element names. 

    // Balance Sheet Quarterly and Annual have the same number of row elements (?)
    // Balance Sheet Quarterly has five columns of data, six columns total including the element names
    // Balance Sheet Annual has four columns of data, five columns total including the element names. 

    // Cash Flow Quarterly and Annual have the same number of row element (20)
    // Cash Flow Quarterly and Annual have four columns of data, five columns total including the element names.

    // inc interim div = income statement interim divider 
    // <DIV id="incinterimdiv" style="display:block">

    // inc annual div = income statement annual divider
    // <DIV id="incannualdiv" style="display:none">

    // bal interim div = balance sheet interim divider
    // <DIV id="balinterimdiv" style="display:none">

    // bal annual div = balence sheet annual divider
    // <DIV id="balannualdiv" style="display:none">

    // cas interim div = cash flow interim divider
    // <DIV id="casinterimdiv" style="display:none">

    // cas annual div  = cash flow annual divider 
    // <DIV id="casannualdiv" style="display:none">


    public enum TimeFrame
    {
        interim, // Quarterly Data
        annual // Annual Data
    }


    #region [Sub Pages]
    public class CashFlow
    {
        public List<DateTime> Periods = new List<DateTime>();
        public List<CashFlowTypeValue> CashFlowValues;
        public Dictionary<string, List<CashFlowTypeValue>> CashFlowValuesDatabase = new Dictionary<string, List<CashFlowTypeValue>>();

        //TODO: Create properties for each of the cash flow annual data. 

        #region Constructors
        // Not sure if we are going to need to pass
        // the entire document or just pass a collection
        // of htmlnodes to this object and let it handle
        // the parsing how it see fit.

        private HtmlNodeCollection htmlNodeCollection;
        private HtmlDocument htmlDocument;
        public CashFlow(HtmlDocument htmlDocument, TimeFrame timeFrame)
        {
            this.TimeFrame = timeFrame;
            this.htmlDocument = htmlDocument;
        }

        public CashFlow(HtmlNodeCollection htmlNodeCollection, TimeFrame timeFrame)
        {
            this.TimeFrame = timeFrame;
            this.htmlNodeCollection = htmlNodeCollection;

            if (this.TimeFrame == TimeFrame.annual)
            {
                ParseCashFlowAnnual();

                // TODO: Remove this function, it was only created for debugging code.
                //DumpCashFlowValuesDatabase();
            }
            else
            {

            }
        }
        #endregion


        // // This would be fore the, "12 months ending 2008-12-31", maybe have a better
        //// data structure to explain what that data means. Or just explain it in the documentation... if there will be any.

        //public Dictionary<DateTime, string> DateToRowElementMapping;
        //public Dictionary<string, double> RowElementMappingToValue;
        /*
         * Example how how to use this:
         * Periods.Add(DateTime.Parse("12-01-07"));
         * DateToRowElementMapping.Add(Periods[0], "NetIncomeStartingLine")
         * RowElementMappingToValue.Add("NetIncomeStartingLine", 12345.00);
         * 
         */
        public class CashFlowTypeValue
        {
            public DateTime Date { set; get; }
            public string DateDescriptsion { set; get; }
            public string MemeberElementName { set; get; }
            public double Value { set; get; }


            #region Constructors
            public CashFlowTypeValue(DateTime date, string dateDescriptsion, string memeberElementName, double value)
            {
                Date = date;
                DateDescriptsion = dateDescriptsion;
                MemeberElementName = memeberElementName;
                Value = value;
            }

            public CashFlowTypeValue()
            {
            }
            #endregion 

            public override string ToString()
            {
                return string.Format("{0} {1}, {2}", Date, MemeberElementName, Value);
            }
        }

       
        private void ParseCashFlowAnnual()
        {
            // 1) Parse all the datetimes
            HtmlNode node = htmlNodeCollection[1];
            foreach (var i in node.ChildNodes)
            {
                if (typeof(HtmlNode) == i.GetType() && i.Name == "thead")
                {
                    //Console.WriteLine(i.Name);
                    HtmlNodeCollection dates = i.FirstChild.ChildNodes;
                    foreach (var ii in dates)
                    {
                        if (typeof(HtmlNode) == ii.GetType())
                        {
                            
                            string[] dateArray = ii.InnerText.Split(new char[] { ' ' });

                            try
                            {
                                DateTime tt = DateTime.Parse(dateArray[dateArray.Length-1]);
                                //Console.WriteLine(tt.ToString());
                                Periods.Add(tt);
                            }
                            catch (Exception ex)
                            {
                                //throw new FormatException("Could not properly parse Cash Flow Financials Dates", ex);
                                //Console.WriteLine("Exception: ");
                                //Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            // 2) Make data structure for the datetime => row element name
            foreach (var i in node.ChildNodes)
            {
                if (typeof(HtmlNode) == i.GetType() && i.Name == "tbody")
                {
                    //Console.WriteLine(i.Name);

                    foreach (var ii in i.ChildNodes)
                    {
                        //Console.WriteLine(ii.Name);
                        int nodeCount = 0;

                        if (typeof(HtmlNode) == ii.GetType() && ii.Name == "tr")
                        {

                            CashFlowTypeValue[] cashFlowTypeValue = new CashFlowTypeValue[Periods.Count];

                            int ci = 0;
                            Periods.ForEach(p =>
                                {
                                    cashFlowTypeValue[ci] = new CashFlowTypeValue()
                                    {
                                        Date = p
                                    };
                                    ci++;
                                });

                            int nodeCountRow = 0;
                            foreach (var iii in ii.ChildNodes)
                            {
                                /*
                                #text
                                tr
                                ==>#text
                                ==>td 		// MemberElementName
                                ==>#text
                                ==>td 		// Nearest Date Value
                                ==>#text
                                ==>td 		// 2nd Nearest Date Value
                                ==>#text
                                ==>td 		// 3rd Nearest Date Value 
                                ==>#text
                                ==>td 		// 4th Nearest Date Value 
                                ==>#text
                                #text
                                */
                                //Console.WriteLine("==>{0}", iii.Name);
                                if (typeof(HtmlNode) == iii.GetType() && iii.Name == "td")
                                {
                                    switch (nodeCountRow)
                                    {
                                        case 0:
                                            //cashFlowTypeValue.MemeberElementName = iii.InnerText;
                                            // TODO: use generic .All(Func<>) instead of this loop.
                                            for (int z = 0; z < Periods.Count; z++)
                                            {
                                                cashFlowTypeValue[z].MemeberElementName = iii.InnerText.Trim().Trim(new char[] { '\n' });
                                            }
                                            break;

                                        case 1:
                                            //cashFlowTypeValue.Value = double.Parse(iii.InnerText);
                                            try
                                            {
                                                cashFlowTypeValue[0].Value = double.Parse(iii.InnerText.Trim().Trim(new char[] { '\n' }));
                                            }
                                            catch (FormatException ex)
                                            {
                                                //Console.WriteLine("FormatException: {0}", ex.Message);
                                                //throw new FormatException("Problem parsing first date's value.", ex);
                                            }
                                            break;

                                        case 2:
                                            try
                                            {
                                                cashFlowTypeValue[1].Value = double.Parse(iii.InnerText.Trim().Trim(new char[] { '\n' }));
                                            }
                                            catch (FormatException ex)
                                            {
                                                //Console.WriteLine("FormatException: {0}", ex.Message);
                                                //throw new FormatException("Problem parsing second date's value.", ex);
                                            }
                                            break;

                                        case 3:
                                            try
                                            {
                                                cashFlowTypeValue[2].Value = double.Parse(iii.InnerText.Trim().Trim(new char[] { '\n' }));
                                            }
                                            catch (FormatException ex)
                                            {
                                                //Console.WriteLine("FormatException: {0}", ex.Message);
                                                //throw new FormatException("Problem parsing third date's value.", ex);
                                            }
                                            break;

                                        case 4:
                                            try
                                            {
                                                cashFlowTypeValue[3].Value = double.Parse(iii.InnerText.Trim().Trim(new char[] { '\n' }));
                                            }
                                            catch (FormatException ex)
                                            {
                                                //Console.WriteLine("FormatException: {0}", ex.Message);
                                                //throw new FormatException("Problem parsing forth date's value.", ex);
                                            }
                                            break;
                                    }
                                    nodeCountRow++;
                                }

                             
                            }

                            CashFlowValues = new List<CashFlowTypeValue>();
                            for (int z = 0; z < Periods.Count; z++)
                            {
                                CashFlowValues.Add(cashFlowTypeValue[z]);
                            }

                            CashFlowValuesDatabase.Add(CashFlowValues[0].MemeberElementName, CashFlowValues);
                               

                            //CashFlowValues.Add(cashFlowTypeValue);

                            nodeCount++;
                        }
                    }
                }// Should produce our data set.
            }
            // 3) Make data structure for the row element => value 
            
        }
        

        // Remove this function, This is only used for dumping some data structures out to the console. 
        public void DumpCashFlowValuesDatabase()
        {
            Console.WriteLine("==========================");

            CashFlowValuesDatabase.Keys.ToList<string>().ForEach(p =>
            {
                Console.WriteLine("CashFlowValuesDatabase: Values : {0}", p);
                CashFlowValuesDatabase[p].ForEach(d =>
                {
                    Console.WriteLine("CashFlowValueType:");
                    Console.Write(d.Date);
                    Console.Write("\t" + d.MemeberElementName + "\t");
                    Console.Write(d.Value + "\n");
                });
                
            });

            Console.WriteLine("==========================");

        }


        public TimeFrame TimeFrame
        {
            private set;
            get;
        }

        public string Title; // This would equal something like "In Milions of USD (except for per share items)

        //public List<DateTime> Periods; // This would be fore the, "12 months ending 2008-12-31", maybe have a better
        // data structure to explain what that data means. Or just explain it in the documentation... if there will be any.

        public string NetIncomeStartingLine;
        public string DepreciationDepletion;
        public string Amortization;
        public string DeferredTaxes;
        public string NonCashItems;
        public string ChangesInWorkingCapital;

        public string CashFromOperatingAcivities;
        public string CapitalExpenditures;
        public string OtherInvestingCashFlowItemsTotal;
        
        public string CashFromInvestingActivities;
        public string FinancingCashFlowItems;
        public string TotalCashDividendsPaid;
        public string IssuanceRetirementOfStockNet;
        public string IssuanceRetirementOfDebtNet;

        public string CashFromFinancingActivities;
        public string ForeignExchangeEffects;

        public string NetChangeInCash;
        public string CashInterestPaidSupplemental;
        public string CashTaxesPaidSupplemental;
        
    }
    public class BalanceSheet
    {
        
        public BalanceSheet()
        { }

        public TimeFrame TimeFrame
        {
            set;
            get;
        }

        string CashEquivalents;
        string ShortTermInvestments;
        string CashAndShortTermInvestments;
        string AccountsReceivableTradeNet;
        string ReceivablesOther;
        string TotalReceivablesNet;
        string TotalInventory;
        string PrepaidExpenses;
        string OtherCurrentAssetsTotal;
        string TotalCurrentAssets;
        string PropertyPlantEquipmentTotalGross;
        string GoodwillNet;
        string IntangiblesNet;
        string LongTermInvestments;
        string OtherLongTermAssetsTotal;
        string TotalAssets;
        string AccountsPayable;
        string AccruedExpenses;
        string NotesPayableShortTermDebt;
        string CurrentPortOfLTDebtCapitalLeases;
        string OtherCurrentLiabilitiesTotal;
        string TotalCurrentLiabilities;
        string LongTermDebt;
        string CapitalLeaseObligations;
        string TotalLongTermDebt;
        string TotalDebt;
        string DeferredIncomeTax;
        string MinorityInterest;
        string OtherLiabilitiesTotal;
        string TotalLiabilities;
        string RedeemablePreferredStockTotal;
        string PreferredStockNonRedeemableNet;
        string CommonStockTotal;
        string AdditionalPaidInCapital;
        string RetainedEarningsAccumulatedDeficit;
        string TreasuryStockCommon;
        string OtherEquityTotal;
        string TotalEquity;
        string TotalLiabilitiesAndShareholdersEquity;
        string SharesOutsCommonStockPrimaryIssue;
        string TotalCommonSharesOutstanding;



    }
    public class IncomeStatement
    {
        public TimeFrame TimeFrame
        {
            set;
            get;
        }

        string currancy;
        string weeksending;
        int weeks;
        DateTime date;

        string Revenue;
        string RevenueOtherTotal;
        string RevenueTotal;
        string RevenueCostTotal;
        string GrossProfit;
        string SellingGeneralAdminExpensesTotal;
        string ResearchAndDevelopment;
        string DepreciationAmortization;
        string InterestExpenseIncomeNetOperating;
        string UnusualExpenseIncome;
        string OtherOperatingExpensesTotal;
        string TotalOperatingExpense;
        string OperatingIncome;
        string InterestIncomeExpenseNetNonOperating;
        string GainLossOnSaleOfAssets;
        string OtherNet;
        string IncomeBeforeTax;
        string IncomeAfterTax;
        string MinorityInterest;
        string EquityInAffiliates;
        string NetIncomeBeforeExtraItems;
        string AccountingChange;
        string DiscontinuedOperatings;
        string ExtraordinaryItem;
        string NetIncome;
        string PreferredDividends;
        string IncomeAvailableToCommonExclExtraItems;
        string IncomeAvailableToCommonInclExtraItems;
        string BasicWeightedAverageShares;
        string BasicEPSExcludingExtraordinaryItems;
        string BasicEPSIncludingExtraOrdinaryItems;
        string DilutionAdjustment;
        string DilutedWeightedAverageShares;
        string DilutedEPSExcludingExtraordinaryItems;
        string DilutedEPSIncludingExtraordinaryItems;
        string DividendsPerShareCommonStockPrimaryIssue;
        string GrossDividendsCommonStock;
        string NetIncomeAfterStockBasedCompExpense;
        string BasicEPSAfterStockBasedCompExpense;
        string DilutedEPSAfterStockBasedCompExpense;
        string DepreciationSupplemental;
        string TotalSpecialItems;
        string NormalizedIncomeBeforeTaxes;
        string EffectOfSpecialItemsOnIncomeTaxes;
        string IncomeTaxesExImpactOfSpecialItems;
        string NormalizedIncomeAfterTaxes;
        string NormalizedIncomeAvailToCommon;
        string BasicNormalizedEPS;
        string DilutedNormalizedEPS;

    }
    #endregion 

    #region [Main Page]
    public class StockFinancials
    {
        /// <summary>
        /// Uri for the financials data from google., 
        /// TODO: figure out what fstype= parameter does...
        /// </summary>
        private string googleQueryFinancials = "http://www.google.com/finance?q={0}:{1}&fstype=ii";

        // Main Tabs { Income Statement, Balance Sheet, Cash Flow }
        // Views { Quarterly Data, Annual Data }

        private HtmlDocument htmlDocument;

        #region Constructor
        public StockFinancials(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;

            // Testing with Cash Flow First
            HtmlNodeCollection cashFlowAnnualDiv = htmlDocument.GetElementbyId("casannualdiv").ChildNodes;
            this.CashFlowAnnualData = new CashFlow(cashFlowAnnualDiv, TimeFrame.annual);

        }

        string exchange;
        string stock;
        public StockFinancials(string stock, string exchange)
        {
            this.exchange = exchange;
            this.stock = stock;

            HtmlWeb htmlWeb = new HtmlWeb();
            this.htmlDocument = htmlWeb.Load(string.Format(googleQueryFinancials ,this.exchange, this.stock));

            // Testing with Cash Flow First
            HtmlNodeCollection cashFlowAnnualDiv = htmlDocument.GetElementbyId("casannualdiv").ChildNodes;
            this.CashFlowAnnualData = new CashFlow(cashFlowAnnualDiv, TimeFrame.annual);
        }
        #endregion 
        // TODO: Refactor into some more intelligent member names.

        public IncomeStatement IncomeStatementQuarterlyData 
        {
            set; 
            get; 
        }
        public IncomeStatement IncomeStatementAnnualData
        {
            set;
            get;
        }

        public BalanceSheet BalanceSheetQuarterlyData
        {
            set;
            get;
        }

        public BalanceSheet BalanceSheetAnnualData
        {
            set;
            get;
        }

        public CashFlow CashFlowQuarterlyData
        {
            set;
            get;
        }

        public CashFlow CashFlowAnnualData
        {
            set;
            get;
        }

    }
    #endregion 
}
