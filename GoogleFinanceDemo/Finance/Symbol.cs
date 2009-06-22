using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Finance
{

    public class Symbol : SimpleElement
    {
        public Symbol() : base("symbol", "gf", "http://schemas.google.com/finance/2007")
        {
            
        }

        public Symbol(string initValue)
            : base("symbol", "gf", "http://schemas.google.com/finance/2007", initValue)
        {
            
        }

        public override IExtensionElementFactory CreateInstance(XmlNode node, AtomFeedParser parser)
        {
            Symbol e = base.CreateInstance(node, parser) as Symbol;

            return e;
        }

        //public Symbol(AbstractEntry ae) :base()
        //{
        //    ae.ExtensionFactories
        //}

        public string FullName
        {
            get
            {

                //return ReturnStringAttribute("fullName");
                return Attributes["fullName"] as string;
            }
        }

        public string Exchange
        {
            get
            {
                //return ReturnStringAttribute("exchange");
                return Attributes["exchange"] as string;
            }
        }
        public string StockSymbol
        {
            get
            {
                //return ReturnStringAttribute("symbol");
                return Attributes["symbol"] as string;
            }
        }



        //public string ReturnStringAttribute(string attributeName)
        //{
        //    IExtensionElementFactory i = FindExtension("symbol", "http://schemas.google.com/finance/2007");
        //    return ((XmlExtension)i).Node.Attributes[attributeName].Value as string;
        //}

    }
}
