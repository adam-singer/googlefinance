using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Finance;

namespace Finance.Web
{
    // Use stock news as the parent class for related companies 
    public class CompanyNews
    {
        // http://www.google.com/finance/company_news?q={0}&num={1}
        // q = symbol to query
        // num = number of items to display.
        string googleQueryCompanyNews = "http://www.google.com/finance/company_news?q={0}:{1}&num={2}";
        public List<NewsFeed> NewsItems { private set; get; }
        public HtmlDocument HtmlDocument { set; get; }

        private string stock;
        private string exchange;
        private int newsCount;

        public CompanyNews(HtmlDocument htmlDocument)
        {
            this.HtmlDocument = htmlDocument;
            NewsItems = new List<NewsFeed>();
            Parse();
        }

        // TODO: refactor to C# 4.0 to use default optional parameters in the constructors.
        public CompanyNews(string symbolName, int count)
        {
            throw new NotImplementedException("Need to implement this constructor");
        }

        public CompanyNews(string symbolName) : this(symbolName, 10)
        {
            throw new NotImplementedException("Need to implement this constructor");
        }

        public CompanyNews(Symbol symbolName)
            : this(symbolName, 10)
        {
            throw new NotImplementedException("Need to implement this constructor");
        }

        public CompanyNews(Symbol symbolName, int count)
        {
            throw new NotImplementedException("Need to implement this constructor");
        }

        public CompanyNews(string stock, string exchange) : this(stock,exchange,10)
        {

        }

        public CompanyNews(string stock, string exchange, int count)
        {
            this.stock = stock;
            this.exchange = exchange;
            this.newsCount = count;

            HtmlWeb htmlWeb = new HtmlWeb();
            this.HtmlDocument = htmlWeb.Load(string.Format(googleQueryCompanyNews, exchange, stock, count));
            NewsItems = new List<NewsFeed>();
            Parse();
        }

        private void Parse()
        {
            HtmlNode n = HtmlDocument.GetElementbyId("news-main");

            foreach (var node in n.ChildNodes)
            {
                if (node.GetType() == typeof(HtmlNode) && node.HasAttributes)
                {
                    if (node.Attributes["class"].Value == "g-section news sfe-break-bottom-16")
                    {
                        NewsFeed newsFeed = new NewsFeed();

                        foreach (var feedNode in node.ChildNodes)
                        {
                            if (feedNode.HasAttributes && feedNode.Attributes["class"].Value == "name")
                            {
                                HtmlNode nn = HtmlNode.CreateNode(feedNode.InnerHtml.Trim());
                                newsFeed.NewsLink = new Uri(nn.Attributes["href"].Value);
                                newsFeed.Title = nn.InnerText;
                            }
                            else if (feedNode.HasAttributes && feedNode.Attributes["class"].Value == "byline")
                            {
                                HtmlNode nn = HtmlNode.CreateNode(feedNode.InnerHtml);
                                newsFeed.Source = nn.NextSibling.InnerText;
                            }
                            else if (feedNode.HasAttributes && feedNode.Attributes["class"].Value == "g-c")
                            {
                                //HtmlNode nn=null;
                                foreach (var div in feedNode.ChildNodes)
                                {
                                    if (div.Name == "div")
                                    {
                                        newsFeed.Snapshot = HtmlNode.CreateNode(div.InnerHtml.Trim()).InnerText;
                                        //nn = div;
                                        break;
                                    }
                                }
                                
                                //HtmlNode nn = HtmlNode.CreateNode(feedNode.NextSibling.InnerHtml);
                                //newsFeed.Snapshot = nn.InnerText;

                                //if (nn != null)
                                //{
                                    foreach (var nnn in feedNode.ChildNodes)
                                    {
                                        //var i
                                        try
                                        {
                                            if (nnn.HasAttributes && nnn.Attributes["class"].Value == "more-rel")
                                            {
                                                newsFeed.RelatedNewsLinks.Add(new Uri(nnn.Attributes["href"].Value));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Gobble the exception up. 
                                        }

                                    }
                                //}
                            }
                        }

                        NewsItems.Add(newsFeed);
                    }
                }
            }
        }
        

        // Refresh the feeds
        public void Refresh()
        {
        }

        public void Refresh(int numberOfNewsItems)
        {
        }

       
        
        public void SetToNext(int number){}
    }
}
