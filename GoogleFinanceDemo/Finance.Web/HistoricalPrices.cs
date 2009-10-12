using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Finance.Web
{
    public class HistoricalPrice
    {
        public DateTime Date { set; get; }
        public double Open { set; get; }
        public double High { set; get; }
        public double Low { set; get; }
        public double Close { set; get; }
        public int Volume { set; get; }

        public HistoricalPrice()
        {
        }
    }

    public class DateRange
    {
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        public DateRange()
        {
        }
    }

    public enum HistoricalPeriod
    {
        DAILY,
        WEEKLY
    }

    class HistoricalPrices
    {
        string googleQueryHistoricalPrices = "http://www.google.com/finance/historical?q=NASDAQ:CSCO";

        // http://www.google.com/finance/historical?q=NASDAQ:CSCO&histperiod=daily
        // http://www.google.com/finance/historical?q=NASDAQ:CSCO&histperiod=weekly
        // http://www.google.com/finance/historical?cid=99624&startdate=Oct+7%2C+2004&enddate=Oct+6%2C+2009&histperiod=weekly
        // http://www.google.com/finance/historical?cid=99624&startdate=Oct+7%2C+2004&enddate=Oct+6%2C+2009

        // csv output file.
        // http://www.google.com/finance/historical?cid=99624&startdate=Oct+7%2C+2004&enddate=Oct+6%2C+2009&output=csv

        /*
cid = Company Id
startdate = Start date of the historical prices
enddate = End date of the historical prices
histperiod = weekly or daily history periods
start = index on which to display the historical price
num = number of historical prices to display (this has some max like 100 or 200)
output = output the data in a format (I think it currently supports CSV only)

http://www.google.com/finance/historical?cid=700146&startdate=Oct+10%2C+2008&enddate=Oct+9%2C+2009&output=csv

http://www.google.com/finance/historical?cid=700146&startdate=Oct+9%2C+2008&enddate=Oct+9%2C+2009&histperiod=weekly&output=csv

http://www.google.com/finance/historical?cid=700146&startdate=Oct%209%2C%202008&enddate=Oct%209%2C%202009&histperiod=weekly&start=0&num=20
         */

//        
//          
//          .historical_chart {
//background: no-repeat url(http://www.google.com/chart?cht=lfe&chc=&chs=270x160&chm=B,EDF7FF,0,0,0&chd=s:hdRYZZZfZWVQQQObZYYYYVcWTXXXXVRVSSSPRJFJJJQLQQRRRIKOKOOOVVWTUUUTWTSSSSQPQQQQQOPQQUUUUYWXSSSQRMNNNNNIMKNNNQSTOIIIKMNQUUUTOPPPPPPKKIJJJEIHFGGGEFKFDDAGILLLLLPRPNNNUSRWUUUQSXabbbXTVZZZZZZXaaaaWYWXccccehhjjjijjfeeedeaaZZZefdaZZZZdbddddijijllllmlmlllighffffcddgffffdedddddbabcccdekmooosvuxwwwwxvxxxx00yzyyyvvuutttqrtwyyyxxxwxxxvsuuwwwwxy333323555557521111756311135779998&chco=3f8ce5) 
         
//         * Some Interesting things to look at
//         * cht = Chart Type
//         * chs = Chart Scale
//         * chm = Chart Members, Chart Variables?
//         * chd = Chart Data
//         * chco = Chart Color
//         


        public string CompanyId { set; get; }
        public DateRange DateRange { private set; get; }
        public int StartIndex { private set; get; }
        public int NumberOfHistoricalPrices { private set; get; }

        public List<HistoricalPrice> Prices { set; get; }

        public Uri HistoricalChart { set; get; }

        public string ExportCSV
        {
            get
            {
                return "";
            }
        }

        private HtmlDocument htmlDocument;

        // <title /> of the web page. 
        public string Name { private set; get; }

        // <meta name="Description" />
        public string Description { private set; get; }


        public HistoricalPrices(HtmlDocument htmlDocument) : this(htmlDocument, new DateRange(), 0, HistoricalPeriod.DAILY)
        {
            // TODO: Fixup this constructor so it does the proper things.
            this.htmlDocument = htmlDocument;
            Prices = new List<HistoricalPrice>();
            Parse();
        }
        public HistoricalPrices(HtmlDocument htmlDocument, DateRange dateRange, int startIndex, HistoricalPeriod historicalPeriod)
        {
        }

        public void Refresh(DateRange dateRange, int startIndex, HistoricalPeriod historicalPeriod)
        {
        }

        private void Parse()
        {
            // Parse the <title /> of the web page and set it to Name
            foreach (var name in htmlDocument.DocumentNode.ChildNodes)
            {
                if (name.Name == "title")
                {
                    Name = name.InnerText;
                    break;
                }
            }

            // Parse the <meta name="Description" />
            foreach (var description in htmlDocument.DocumentNode.ChildNodes)
            {
                if (description.Name == "meta" && description.Attributes["name"].Value == "Description")
                {
                    Description = description.InnerText;
                    break;
                }
            }
            // For parsing the prices the first two tags we need to look for are.
            // <div id="prices" class="gf-table-wrapper sfe-break-bottom-16">
            // <table id="historical_price" class="gf-table">

            HtmlNode historicalPricesNode = htmlDocument.GetElementbyId("historical_price");
            HtmlNodeCollection historicalPricesNode_ChildNodes = historicalPricesNode.ChildNodes;
            HtmlNode nodePrices = null;
            foreach (var tbodyChild in historicalPricesNode_ChildNodes)
            {
                if (tbodyChild.Name == "tbody")
                {
                    // We know that we have now found the table's body.
                    nodePrices = tbodyChild.NextSibling.NextSibling;
                    break;
                }
            }

            foreach (var p in nodePrices.ChildNodes)
            {
                if (p.Name == "tr")
                {
                    HtmlNode date = p.ChildNodes[0];
                    HtmlNode open = p.ChildNodes[1];
                    HtmlNode high = p.ChildNodes[2];
                    HtmlNode low = p.ChildNodes[3];
                    HtmlNode close = p.ChildNodes[4];
                    HtmlNode volume = p.ChildNodes[5];
                    HistoricalPrice historicalPrice = new HistoricalPrice()
                    {
                        Date = DateTime.Parse(date.InnerText),
                        Open = double.Parse(open.InnerText),
                        High = double.Parse(high.InnerText),
                        Low = double.Parse(low.InnerText),
                        Close = double.Parse(close.InnerText),
                        Volume = int.Parse(volume.InnerText.Trim(new char[] { ',' }))
                    };

                    Prices.Add(historicalPrice);
                }
            }

        }
    }
}
