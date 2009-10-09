using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Finance.Web
{
    /// <summary>
    /// Percentage class helps with parsing out the format that google puts percents in.
    /// </summary>
    public class Percentage
    {
        string percentage;
        double dpercentage;

        public Percentage(string percentage)
        {
            this.percentage = percentage;
            // Refactor this, I thought anything in () would be negative in terms of google's finance format....
            // turns out this is wrong!
            if (this.percentage.Contains('('))
            {
                dpercentage = double.Parse(this.percentage.Trim(new char[] { '(', ')', '%' }));
            }
            else
            {
                dpercentage = double.Parse(this.percentage.Trim(new char[] { '%' }));
            }
        }

        public Percentage(double percentage)
        {
            dpercentage = percentage;
            this.percentage = dpercentage.ToString();
        }
        public Percentage(int percentage)
        {
            dpercentage = double.Parse(percentage.ToString());
            this.percentage = dpercentage.ToString();
        }
        public Percentage(decimal percentage)
        {
            dpercentage = double.Parse(percentage.ToString());
            this.percentage = dpercentage.ToString();
        }

        public double ToDouble()
        {
            return dpercentage;
        }

        public int ToInt()
        {
            return (int)dpercentage;
        }

        public string ToString()
        {
            return percentage;
        }

        //public string ToOriginalString()
        //{
        //    return percentage;
        //}

        public decimal ToDecimal()
        {
            return (decimal)dpercentage;
        }
    }

    /// <summary>
    /// Navigation Link that contains a text descriptsion.
    /// </summary>
    public class NavigateElement
    {
        public string Text { set; get; }
        public Uri Link { set; get; }
    }

    /// <summary>
    /// The Company Summary Section.
    /// </summary>
    public class Summary
    {
        /// <summary>
        /// Description of the company. Parsed by looking for id="summary"
        /// </summary>
        public string CompanyDescriptsion { set; get; }
        
        /// <summary>
        /// External link to the companies description. Parsed by looking for id="m-rprofile"
        /// </summary>
        public NavigateElement ExternalCompanyProfile { set; get; }
    }

    /// <summary>
    /// Top mangement in the company. 
    /// </summary>
    public class Mangement
    {
        /// <summary>
        /// Full name of employee
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Title of the employee
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Age of the empolyee
        /// </summary>
        public string Age { set; get; }

        /// <summary>
        /// External link to employee's compensation 
        /// </summary>
        public Uri ProfileLink { set; get; }
    }

    public class Range
    {
        public double High { set; get; }
        public double Low { set; get; }
        public string ToString()
        {
            return string.Format("{0} - {1}", Low, High);
        }
    }

    public class FiftyTwoWeek
    {
        public double High { set; get; }
        public double Low { set; get; }
        public string ToString()
        {
            return string.Format("{0} - {1}", Low, High);
        }
    }

    public class ListingHeader
    {
        public string CompanyName { set; get; }
        public string ListingType { set; get; }
        public string ListingSymbol { set; get; }
    }

    /// <summary>
    /// TODO: Enclose Summary, News, Financials and Related Companies into a single data structure.
    /// TODO: Check if we need to create a baseuri, downloaded files do not need this, but when query from web we do for google links.
    /// TODO: Fix anything that could be a negative value.
    /// </summary>
    public class StockSummary
    {
        #region Constructor
        /// <summary>
        /// Default constructor that does nothing. 
        /// </summary>
        public StockSummary(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;

            // Parse out the main id to find the data for the stock we are looking at. 
            HtmlNode node = this.htmlDocument.GetElementbyId("price-panel");
            HtmlNode node2 = node.FirstChild.NextSibling;
            HtmlNode node3 = node2.FirstChild.NextSibling;
            HtmlNode node4 = node3.FirstChild.NextSibling;
            RefernceId = node4.Attributes["id"].Value.TrimEnd(new char[] {'l'});
        }
        #endregion 

        #region Refernce Id
        /// <summary>
        /// This is the main id used for parsing data from this html document.
        /// </summary>
        public string RefernceId { private set; get; }
        #endregion 

        #region Main Html Document
        /// <summary>
        /// Html Document used for parsing.
        /// </summary>
        private HtmlDocument htmlDocument;
        public HtmlDocument HtmlDocument
        {
            get { return htmlDocument; }
            set { HtmlDocument = value; }
        }
        #endregion 

        #region Summary Navigation Links
        /// <summary>
        /// Navigate Company News with following link.
        /// </summary>
        public NavigateElement CompanyNews
        { 
            get
            {
                return CreateStockNavigateElement("nav-cn");   
            }
        }

        // Navigate Related Companies
        public NavigateElement RelatedCompanies
        {
            get
            {
                return CreateStockNavigateElement("nav-cr");
            }
        }

        // Navigate Company Historical Prices
        public NavigateElement CompanyHistory
        {
            get
            {
                return CreateStockNavigateElement("nav-ch");
            }
        }

        public NavigateElement CompanyFinancials
        {
            get
            {
                return CreateStockNavigateElement("nav-cf");
            }
        }

        public NavigateElement GoogleFinanceMain
        {
            get
            {
                return CreateStockNavigateElement("nav-m");
            }
        }

        public NavigateElement GoogleFinanceNews
        {
            get
            {
                return CreateStockNavigateElement("nav-n");
            }
        }

        public NavigateElement GoogleFinancePortfolios
        {
            get
            {
                return CreateStockNavigateElement("nav-p");
            }
        }

        public NavigateElement GoogleStockScreener
        {
            get
            {
                return CreateStockNavigateElement("nav-s");
            }
        }

        public NavigateElement GoogleDomesticTrends
        {
            get
            {
                return CreateStockNavigateElement("nav-q");
            }
        }

        /// <summary>
        /// Creates the navigation object with text and forwarding link.
        /// </summary>
        /// <param name="id">html id</param>
        /// <returns>object with text and uri</returns>
        private NavigateElement CreateStockNavigateElement(string id)
        {
            HtmlNode n = htmlDocument.GetElementbyId(id);
            return new NavigateElement()
            {
                Text = n.InnerText.Replace(Environment.NewLine, ""),
                Link = new Uri(new Uri(@"http://finance.google.com"),n.Attributes["href"].Value)
            };
        }
        #endregion 

        #region Company Section
        /// <summary>
        /// Company Section returns the descriptsion of the company and an external link for more information. 
        /// </summary>
        public Summary CompanySection
        {
            get {
                Summary summary = new Summary();

                HtmlNode n = htmlDocument.GetElementbyId("summary");

                // Select the company description with linq.
                var i = from na in n.ChildNodes.Cast<HtmlNode>()
                        where na.HasAttributes == true 
                        && na.Attributes["class"].Value == "sfe-section"
                        select new
                        {
                            CompanyDescriptsion = from nn in na.ChildNodes.Cast<HtmlNode>()
                                                  where nn.Name == "div" && nn.HasAttributes && nn.Attributes["class"].Value == "companySummary"
                                                  select nn.InnerText
                        };
                // Assign the description to the summary object. 
                summary.CompanyDescriptsion = i.ToArray()[0].CompanyDescriptsion.ToArray()[0];

                n = htmlDocument.GetElementbyId("m-rprofile");
                summary.ExternalCompanyProfile = new NavigateElement()
                {
                    Text = n.InnerText,
                    Link = new Uri(n.Attributes["href"].Value)
                };
               
                return summary; 
            }
        }
        #endregion 

        #region Mangement Section 
        public List<Mangement> Mangement
        {
            get
            {
                List<Mangement> mangement = new List<Mangement>();
                //<table id="mgmt-table" width="100%">
                HtmlNode n = htmlDocument.GetElementbyId("mgmt-table").FirstChild;
                //<tbody>
                HtmlNodeCollection v = n.ChildNodes;
                int ageLoopCounter = -1;
                foreach (var node in v)
                {
                    if (node.HasAttributes)
                    {
                        string b = node.GetAttributeValue("tabindex", "NOTFOUND");
                        string c = node.GetAttributeValue("style", "NOTFOUND");
                        if (node.Name == "tr" && b != "NOTFOUND" && node.Attributes["tabindex"].Value == "0")
                        {
                            var innerNodeName = node.ChildNodes.Cast<HtmlNode>().Where(p => p.Name == "td" && p.Attributes["class"].Value == "p linkbtn").Take(1);
                            var innerNodePosition = node.ChildNodes.Cast<HtmlNode>().Where(
                                p => p.Name == "td" 
                                && p.GetAttributeValue("class", "NOTFOUND") != "NOTFOUND" 
                                && p.Attributes["class"].Value == "t").Take(1);
                            
                            mangement.Add(new Mangement() {
                                Name = innerNodeName.ToArray()[0].InnerText.Replace(Environment.NewLine,"").TrimStart(new char[] {' ', '\r', '\n'}).TrimEnd(' ', '\r', '\n'), 
                                Title = innerNodePosition.ToArray()[0].InnerText.Replace(Environment.NewLine,"").TrimEnd(new char[] { ' ', '\r', '\n' })
                            });

                            ageLoopCounter++;
                        }
                        else if (node.Name == "tr" && c != "NOTFOUND" && node.Attributes["style"].Value == "display: none;")
                        {
                            var innerNodeAgeLink = node.ChildNodes.Cast<HtmlNode>().Where(
                                p => p.Name =="td"
                                && p.GetAttributeValue("class", "NOTFOUND") != "NOTFOUND"
                                && p.Attributes["class"].Value == "t").Take(1);

                            var innerNodeProfileLink = innerNodeAgeLink.ToArray()[0].ChildNodes.Cast<HtmlNode>().Where(
                                p => p.Name == "a"
                                    && p.GetAttributeValue("class", "NOTFOUND") != "NOTFOUND"
                                    && p.Attributes["class"].Value == "e-p").Take(1);

                            mangement[ageLoopCounter].Age = innerNodeAgeLink.ToArray()[0].InnerText.Split('\n')[1].Trim(new char[] {'A','g','e',':',' ','\r','\n'});
                            mangement[ageLoopCounter].ProfileLink = new Uri(innerNodeProfileLink.ToArray()[0].Attributes["href"].Value);
                        }
                    }
                }

                return mangement;
            }
        }
        #endregion 

        #region Companies Sector
        /// <summary>
        /// Returns the name of the sector and uri.
        /// </summary>
        public NavigateElement Sector { 
            get
            { // <div class="sfe-section"
                HtmlNode n = htmlDocument.GetElementbyId("sector");
                
                return new NavigateElement()
                    {
                        Link = new Uri(new Uri(@"http://www.google.com/finance"), n.Attributes["href"].Value),
                        Text = n.InnerText
                    }; 
            }
        }
        #endregion 

        #region Companies Industry
        /// <summary>
        /// Returns the name of the industry and uri.
        /// </summary>
        public NavigateElement Industry
        {
            get
            {// <div class="sfe-section"
                HtmlNode n = htmlDocument.GetElementbyId("sector").ParentNode;

                HtmlNode ind = n.ChildNodes.Cast<HtmlNode>().ToArray()[3];

                return new NavigateElement()
                {
                    Text = ind.InnerText,
                    Link = new Uri(new Uri(@"http://www.google.com/finance"), ind.Attributes["href"].Value)
                };
            }

        }
        #endregion 

        #region Snapshot of Stock Price
        // These tags provide a snapshot of the companies numbers. 
        // <div id="snap-panel" class="goog-inline-block" style="overflow: hidden; height: 82px;">
        //<ol id="snap-data">


        // Price Data
        // TODO: Need to place exceptions in place here and return "-", meaning
        // data not available.
      
        // id="ref_[0-9]*_l", l = listing price
        public double  ListingPrice
        {  
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "l");
                return double.Parse(n.InnerText.Trim(new char[] { '(', ')' }));
            }
        }

        // id="ref_[0-9]*_c", c = change in listing price
        public double ListingChangePrice {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "c");
                string i = n.InnerText.Trim(new char[] { '(', ')' });
                return double.Parse(i); 
            } 
        }

        // id="ref_[0-9]*_cp", cp = change in listing price percentage
        public double ListingChangePricePercentage {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "cp");
                Percentage p = new Percentage(n.InnerText);
                return p.ToDouble();
            }
        }
        // id="ref_[0-9]*_el", el = extended listing
        public double ExtendedListingPrice {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "el");
                string i = n.InnerText.Trim(new char[] { '(', ')' });
                return double.Parse(i);
            }
        }
        // id="ref_[0-9]*_ec", ec = extended change in listing price
        public double ExtendedChangeListingPrice {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "ec");
                string i = n.InnerText.Trim(new char[] { '(', ')'});
                return double.Parse(i);
            }
        }

        // id="ref_[0-9]*_ecp", ecp = extended change in listing price percentage
        public double ExtendedChangeListingPricePercentage {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "ecp");
                Percentage p = new Percentage(n.InnerText);
                return p.ToDouble();
            }
        }
        // id="ref_[0-9]*_elt", elt = extended listing time
        public string  ExtendedListingTime {
            get
            {
                // TODO: Need to parse this into a datetime object.
                HtmlNode n = htmlDocument.GetElementbyId(RefernceId + "elt");
                string i = n.InnerText.Split(new char[] {'&'})[0];
                //DateTime time = DateTime.Parse(i,);
                return i;
            }
        }
        #endregion 


        // id="companyheader"
        public ListingHeader CompanyHeader
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("companyheader");

                string[] a = n.InnerText.Replace("&nbsp", " ").Split(new char[] { ';' });
                
                
                //string[] a = n.InnerText.Split(new char[] {'&'});

                

                string companyName = a[0].Trim(new char[] { '\r', '\n' }).TrimEnd(new char[] { '&' });
                string listingType = a[1].Split(new char[] { ',' })[0].Split(new char[] { '(' })[1];
                string listingSymbol = a[1].Split(new char[] { ',' })[1].Split(new char[] { ')' })[0].TrimStart(new char[] {' '});


                return new ListingHeader()
                {
                    CompanyName = companyName,
                    ListingType = listingType,
                    ListingSymbol = listingSymbol
                };
            }
        }

        public Uri WatchThisStockLink
        {
            get;
            set;
        }

        public NavigateElement SimilarCompanies
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("similar");
                return new NavigateElement()
                {
                    Text = "Similar Stocks",
                    Link = new Uri(n.Attributes["href"].Value)
                };
            }
        }

        public NavigateElement FindMoreResults
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("searchmore");
                return new NavigateElement()
                {
                    Text = "More Results Link",
                    Link = new Uri(n.Attributes["href"].Value)
                };
            }
        }



        // <ol id="snap-data">
        // This is the snapshot finacial data. 
        // TODO: clean up how this is done. 
        public Range Range {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Range")
                                {
                                   
                                }
                                else if (s.Name == "span")
                                {
                                    return new Range()
                                    {
                                        High = double.Parse(s.InnerText.Split(new char[] { ' ' })[2]),
                                        Low = double.Parse(s.InnerText.Split(new char[] { ' '})[0])
                                    };
                                }
                            }
                        }
                    }
                }
               

                return new Range();
            }
        }
        public FiftyTwoWeek FiftyTwoWeek {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "52 week")
                                {
                                    return new FiftyTwoWeek()
                                    {
                                        High = double.Parse(s.NextSibling.NextSibling.InnerText.Split(new char[] { ' ' })[2]),
                                        Low = double.Parse(s.NextSibling.NextSibling.InnerText.Split(new char[] { ' ' })[0])
                                    };
                                }
                            }
                        }
                    }
                }
                return new FiftyTwoWeek();
            }
        }
        public double Open {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Open")
                                {
                                    return double.Parse(s.NextSibling.NextSibling.InnerText);
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }
        
        public string Volume {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Vol / Avg.")
                                {
                                    return s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[0];
                                }
                            }
                        }
                    }
                }

                return "";
            }
        }
        public string Average {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Vol / Avg.")
                                {
                                    return s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[1];
                                }
                            }
                        }
                    }
                }
                return "";
            }
        }

        public string MarketCapital {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Mkt cap")
                                {
                                    return s.NextSibling.NextSibling.InnerText;
                                }
                            }
                        }
                    }
                }
                return "";
            }
        }

        public double P_E
        {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "P/E")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText);
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }

        public double Dividend
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Div/yield")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[0]);
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[0]), ex);
                                    }
                                    catch (IndexOutOfRangeException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText, ex));
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }


        public double Yield
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Div/yield")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[1]);
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText.Split(new char[] { '/' })[1]), ex);
                                    }
                                    catch (IndexOutOfRangeException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }

        public double EPS
        {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "EPS")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText);
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }

        public string Shares
        {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Shares")
                                {
                                    try {
                                        return s.NextSibling.NextSibling.InnerText;
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return "";
            }
        }

        public double Beta {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Beta")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText);
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            }
        }

        public double InterestOwned {
            get
            {// TODO: need a way to return the total amount of trading.
                HtmlNode n = htmlDocument.GetElementbyId("snap-data");
                foreach (var i in n.ChildNodes)
                {
                    if (i.GetType() == typeof(HtmlNode))
                    {
                        foreach (var s in i.ChildNodes)
                        {
                            if (s.GetType() == typeof(HtmlNode))
                            {
                                if (s.Name == "span" && s.InnerText == "Inst. own")
                                {
                                    try
                                    {
                                        return double.Parse(s.NextSibling.NextSibling.InnerText.TrimEnd(new char[] { '%' }));
                                    }
                                    catch (FormatException ex)
                                    {
                                        throw new DataNotAvailable(string.Format("Data is not available for this item: {0} = {1}", s.InnerText, s.NextSibling.NextSibling.InnerText), ex);
                                    }
                                }
                            }
                        }
                    }
                }
                return 0.0;
            } 
        }


        public Uri HomePage
        {
            get
            {
                HtmlNode n = htmlDocument.GetElementbyId("fs-chome");
                return new Uri(n.Attributes["href"].Value);
            }
        }

    }
}
