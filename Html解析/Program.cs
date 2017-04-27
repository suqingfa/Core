using AngleSharp.Parser.Html;
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
            string html = client.GetStringAsync("http://www.gongjuji.net").Result;

            //HTML 解析成 IDocument
            var dom = new HtmlParser().Parse(html);
            //从dom中提取所有class='col-sm-6'的div标签
            var listDiv = dom.QuerySelectorAll("div.col-sm-6");
            foreach (var item in listDiv)
            {
                Console.WriteLine(item.QuerySelector("h3").InnerHtml);
            }
        }
    }
}