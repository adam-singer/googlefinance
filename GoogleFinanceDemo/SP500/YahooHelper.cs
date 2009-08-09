using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SP500
{
    public class YahooHelper
    {
        // http://ilmusaham.wordpress.com/tag/stock-yahoo-data/
        // http://www.john-lai.com/2009/03/31/yahoo-stocks-api/
        // http://codefreezer.com/codedetail.php?article=66
        WebClient webClient; 
        string yahooStockString = "http://finance.yahoo.com/d/quotes.csv?s={0}&f=l1c1va2xj1b4j4dyekjm3m4rr5p5p6s7&e=.csv";

        /// <summary>
        /// Yahoo API helper, we use this object cause Google does not give us a way
        /// to tell or choose which market the symbol might be on. So we use this
        /// helper object.
        /// </summary>
        /// <param name="symbol">Ticker Symbol</param>
        public YahooHelper(string symbol)
        {
            webClient = new WebClient();
            string data = webClient.DownloadString(string.Format(yahooStockString, symbol));
            Parse(data);
        }

        public Dictionary<string, string> Quote { private set; get; }

        private void Parse(string data)
        {
            string[] fields = data.Replace("\"", "").Split(',');
            Dictionary<string, string> quote = new Dictionary<string, string>();

            // TODO: turn these into typed properties.
            quote.Add("price", fields[0]);
            quote.Add("change", fields[1]);
            quote.Add("volume", fields[2]);
            quote.Add("avg_daily_volume", fields[3]);
            
            // NOTE: Even yahoo's api sucks when determining which
            // market the symbol you are looking for is on.'
            // Again we need a better way or some kind of searchable
            // API like google already has builtin to its text boxes 
            // from javascript.

            // Correct the market exchange for google.
            if (fields[4] == "NasdaqNM") fields[4] = "Nasdaq";
            
            
            quote.Add("stock_exchange", fields[4]);
            quote.Add("market_cap", fields[5]);
            quote.Add("book_value", fields[6]);
            quote.Add("ebitda", fields[7]);
            quote.Add("dividend_per_share", fields[8]);
            quote.Add("dividend_yield", fields[9]);
            quote.Add("earnings_per_share", fields[10]);
            quote.Add("52_week_high", fields[11]);
            quote.Add("52_week_low", fields[12]);
            quote.Add("50day_moving_avg", fields[13]);
            quote.Add("200day_moving_avg", fields[14]);
            quote.Add("price_earnings_ratio", fields[15]);
            quote.Add("price_earnings_growth_ratio", fields[16]);
            quote.Add("price_sales_ratio", fields[17]);
            quote.Add("price_book_ratio", fields[18]);
            quote.Add("short_ratio", fields[19]);
            
            
            Quote = quote;
        }
       


    }
}
