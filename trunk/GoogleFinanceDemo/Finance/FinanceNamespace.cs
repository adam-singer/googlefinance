using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Finance
{
    public class FinanceNamespace
    {
      /** Google Finance (GF) namespace */
      public const String GF = "http://schemas.google.com/finance/2007";

      /** Google Finance (GF) namespace prefix */
      public const String GF_PREFIX = GF + "#";

      /** Google Finance (GF) namespace alias */
      public const String GF_ALIAS = "gf";

      /** XML writer namespace for Google Finance (GF) */
      //public static XmlNamespace GF_NS = new XmlNamespace(GF_ALIAS, GF);
      public XmlNamespaceManager GF_NS;



      // NOTE: Check out GDataSpreadsheetsNameTable for proper namespace constants
      public const string Prefix = "gf";
      public const string NSGFinance = "http://schemas.google.com/finance/2007";


      public FinanceNamespace()
      {
          NameTable nt = new NameTable();
          GF_NS = new XmlNamespaceManager(nt);
          GF_NS.AddNamespace(GF_ALIAS, GF);
      }
    }
}
