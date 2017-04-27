using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Text;

namespace Html解析
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //加载Web 的页面并解析内容
            HttpClient client = new HttpClient();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(client.GetStringAsync("http://www.gongjuji.net").Result);

            //以document 为基准
            string rowPath = "/html/body/div[2]/div[2]";
            HtmlNode row = doc.DocumentNode.SelectSingleNode(rowPath);

            //创建row为基准
            HtmlNodeCollection titles = row.SelectNodes("//h3");
            foreach (HtmlNode item in titles)
            {
                Console.WriteLine(item.InnerText);
            }

            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a");
            foreach (HtmlNode item in links)
            {
                Console.WriteLine(item.Attributes["href"].Value);
            }
        }
    }
}