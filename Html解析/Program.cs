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
            string html = client.GetStringAsync("https://book.douban.com/subject_search?search_text=%E6%B7%B1%E5%BA%A6%E5%AD%A6%E4%B9%A0").Result;

            //HTML 解析成 IDocument
            var dom = new HtmlParser().Parse(html);
            //从dom中提取所有class='col-sm-6'的div标签
            var listDiv = dom.QuerySelectorAll(".info");
            foreach (var item in listDiv)
            {
                Console.WriteLine(item.QuerySelector("a").GetAttribute("href"));
            }
        }
    }
}