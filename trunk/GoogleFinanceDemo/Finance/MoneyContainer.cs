using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Extensions;

namespace Finance
{
    public class MoneyContainer : SimpleContainer
    {
        private ExtensionCollection<Money> money;
        public ExtensionCollection<Money> Money
        {
            get
            {
                if (money == null)
                {
                    money = new ExtensionCollection<Money>(this);
                }
                return money;
            }
        }

        public MoneyContainer(string name, string prefix, string ns)
            : base(name, prefix, ns)
        {
            this.ExtensionFactories.Add(new Money());
        }
    }
}