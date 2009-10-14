using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Finance.Web
{
    public class NewsFeed
    {
        public Uri NewsLink { set; get; }
        public string Title { set; get; }
        public string Source { set; get; }
        public string Snapshot { set; get; }
        public Uri RelatedNewsLinks { set; get; }
    }

    public class RelatedCompanies
    {
        // http://www.google.com/finance/company_news?q=NASDAQ:CSCO&start={0}&num={1}

        private HtmlDocument htmlDocument;
        #region Constructor
        public RelatedCompanies(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;

            Parse();
        }

        private void Parse()
        {
            HtmlNode n = htmlDocument.GetElementbyId("news-main");

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

                        NewsFeeds.Add(newsFeed);
                    }
                }
            }
        }
        #endregion

        // Refresh the feeds
        public void Refresh()
        {
        }

        public void Refresh(int numberOfNewsItems)
        {
        }

        public IList<NewsFeed> NewsFeeds { private set; get; }
        
        public void SetToNext(int number){}
        
    }
}
