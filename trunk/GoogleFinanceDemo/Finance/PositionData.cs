using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;

namespace Finance
{
    // TODO: Abstract this object and Portfolio object so they are from the same object.

    public class PositionData : SimpleContainer 
    {
        public PositionData() :
            base(FinanceNamespace.POSITIONDATA, FinanceNamespace.PREFIX_FINANCE, FinanceNamespace.NAMESPACE_FINANCE)
        {
            this.ExtensionFactories.Add(new CostBasis());
            this.ExtensionFactories.Add(new DaysGain());
            this.ExtensionFactories.Add(new Gain());
            this.ExtensionFactories.Add(new MarketValue());

            Attributes.Add(FinanceNamespace.GAINPERCENTAGE, null);
            Attributes.Add(FinanceNamespace.RETURN1W, null);
            Attributes.Add(FinanceNamespace.RETURN4W, null);
            Attributes.Add(FinanceNamespace.RETURN3M, null);
            Attributes.Add(FinanceNamespace.RETURNYTD, null);
            Attributes.Add(FinanceNamespace.RETURN1Y, null);
            Attributes.Add(FinanceNamespace.RETURN3Y, null);
            Attributes.Add(FinanceNamespace.RETURN5Y, null);
            Attributes.Add(FinanceNamespace.RETURNOVERALL, null);
            Attributes.Add(FinanceNamespace.SHARES, null);
        }


        public DaysGain DaysGain
        {
            get
            {
                return FindExtension(FinanceNamespace.DAYSGAIN,
                                         FinanceNamespace.NAMESPACE_FINANCE) as DaysGain;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.DAYSGAIN,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        public CostBasis CostBasis
        {
            get
            {
                return FindExtension(FinanceNamespace.COSTBASIS,
                                         FinanceNamespace.NAMESPACE_FINANCE) as CostBasis;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.COSTBASIS,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        public Gain Gain
        {
            get
            {
                return FindExtension(FinanceNamespace.GAIN,
                                         FinanceNamespace.NAMESPACE_FINANCE) as Gain;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.GAIN,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        public MarketValue MarketValue
        {
            get
            {
                return FindExtension(FinanceNamespace.MARKETVALUE,
                                         FinanceNamespace.NAMESPACE_FINANCE) as MarketValue;
            }
            set
            {
                ReplaceExtension(FinanceNamespace.MARKETVALUE,
                                    FinanceNamespace.NAMESPACE_FINANCE,
                                    value);
            }
        }

        /// <summary>
        /// Percentage gain.
        /// </summary>
        public double GainPercentage
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.GAINPERCENTAGE] as string);
            }
            set
            {
                Attributes[FinanceNamespace.GAINPERCENTAGE] = value.ToString();
            }

        }

        /// <summary>
        /// 1 week return (percentage).
        /// </summary>
        public double Return1Week
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN1W] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN1W] = value.ToString();
            }
        }

        /// <summary>
        /// 4 week return (percentage).
        /// </summary>
        public double Return4Week
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN4W] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN4W] = value.ToString();
            }
        }

        /// <summary>
        /// 3 month return (percentage).
        /// </summary>
        public double Return3Month
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN3M] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN3M] = value.ToString();
            }
        }

        /// <summary>
        /// Year-to-date return (percentage).
        /// </summary>
        public double ReturnYTD
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURNYTD] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURNYTD] = value.ToString();
            }
        }

        /// <summary>
        /// 1 year return (percentage).
        /// </summary>
        public double Return1Year
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN1Y] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN1Y] = value.ToString();
            }
        }

        /// <summary>
        /// 3 year return (percentage).
        /// </summary>
        public double Return3Year
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN3Y] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN3Y] = value.ToString();
            }
        }

        /// <summary>
        /// 5 year return (percentage).
        /// </summary>
        public double Return5Year
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURN5Y] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURN5Y] = value.ToString();
            }
        }

        /// <summary>
        /// Overall return (percentage).
        /// </summary>
        public double ReturnOverall
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.RETURNOVERALL] as string);
            }
            set
            {
                Attributes[FinanceNamespace.RETURNOVERALL] = value.ToString();
            }
        }

        public double Shares
        {
            get
            {
                return double.Parse(Attributes[FinanceNamespace.SHARES] as string);
            }
            set
            {
                Attributes[FinanceNamespace.SHARES] = value.ToString();
            }
        }
    }
}
