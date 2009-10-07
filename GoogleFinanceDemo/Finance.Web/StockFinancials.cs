using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance.Web
{
    class CashFlow
    {

    }
    class BalanceSheet
    {
        public BalanceSheet()
        { }

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
    class IncomeStatement
    {
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

    class StockFinancials
    {
        // Main Tabs { Income Statement, Balance Sheet, Cash Flow }
        // Views { Quarterly Data, Annual Data }

        public StockFinancials()
        {
        }

        public IncomeStatement QuarterlyData;
        public IncomeStatement AnnualData;

        public BalanceSheet QuarterlyData;
        public BalanceSheet AnnualData;

        public CashFlow QuarterlyData;
        public CashFlow AnnualData;
    }
}
