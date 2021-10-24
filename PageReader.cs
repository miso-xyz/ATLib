using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using HtmlAgilityPack;
using System.Drawing;
using System.IO;

namespace ATLib
{
    public class ItemProp
    {
        public ItemProp(string name, string value) { Name = name; Content = value; }

        public string Name { get; set; }
        public string Content { get; set; }
    }

    public enum PageType
    {
        Track,
        Artist,
        Search,
        Charts,
        Unknown,
        Invalid
    }

    public class PageReader
    {
        public static PageType GetPageType(string URL)
        {
            if (URL.Contains("audiotool.com/track/")) { return PageType.Track; }
            return PageType.Unknown;
        }

        public static ItemProp[] GetProps(string URL)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetPageSource(URL));
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//meta");
            List<ItemProp> props = new List<ItemProp>();
            foreach (HtmlNode node in nodes)
            {
                string name = null, data = null;
                //bool isJunkNode = false;
                foreach (HtmlAttribute atr in node.Attributes)
                {
                    switch (atr.Name)
                    {
                        case "name":
                        case "itemprop":
                        case "property": name = atr.Value; break;
                        case "content": data = atr.Value; break;
                    }
                }
                if (name != null) { props.Add(new ItemProp(name, data)); }
            }
            return props.ToArray();
        }

        private static string GetPageSource(string URL) { ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; return Encoding.Default.GetString(new WebClient().DownloadData(URL)); }
    }
}
