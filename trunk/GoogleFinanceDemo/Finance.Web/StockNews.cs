using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Finance;

namespace Finance.Web
{
    // Use stock news as the parent class for related companies 
    class StockNews
    {
        // http://www.google.com/finance/company_news?q={0}&num={1}
        // q = symbol to query
        // num = number of items to display.

        public HtmlDocument HtmlDocument { set; get; }
        public StockNews(HtmlDocument htmlDocument)
        {
            this.HtmlDocument = htmlDocument;
        }

        // TODO: refactor to C# 4.0 to use default optional parameters in the constructors.
        public StockNews(string symbolName, int count)
        {
        }

        public StockNews(string symbolName) : this(symbolName, 10)
        {
        }

        public StockNews(Symbol symbolName)
            : this(symbolName, 10)
        {
        }

        public StockNews(Symbol symbolName, int count)
        {

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
                            if (feedNode.Attributes["class"].Value == "name")
                            {
                                HtmlNode nn = HtmlNode.CreateNode(feedNode.InnerHtml);
                                newsFeed.NewsLink = new Uri(nn.Attributes["href"].Value);
                                newsFeed.Title = nn.InnerText;
                            }
                            else if (feedNode.Attributes["class"].Value == "byline")
                            {
                                HtmlNode nn = HtmlNode.CreateNode(feedNode.InnerHtml);
                                newsFeed.Source = nn.NextSibling.InnerText;
                            }
                            else if (feedNode.Attributes["class"].Value == "g-c")
                            {
                                HtmlNode nn = HtmlNode.CreateNode(feedNode.NextSibling.InnerHtml);
                                newsFeed.Snapshot = nn.InnerText;

                                foreach (var nnn in nn.ChildNodes)
                                {
                                    if (nnn.Attributes["class"].Value == "more-rel")
                                    {
                                        // TODO: really need to handle exceptions here.
                                    }
                                }
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

        public IList<NewsFeed> NewsItems { private set; get; }
        
        public void SetToNext(int number){}
    }
}
