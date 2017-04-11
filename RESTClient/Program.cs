using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RESTClient
{
	class Program
	{
		static void Main()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			new Program().RunAsync().Wait();
		}

		private static readonly HttpClient client = new HttpClient();
		static Program()
		{
			client.BaseAddress = new Uri("http://localhost:52395");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		async Task RunAsync()
		{
			var add = await AddPerson(client);
			Console.WriteLine($"添加结果：{ add }");

			var result = await GetPerson(client);
			Console.WriteLine($"查询结果：{ string.Join(",", result)}");

			var put = await PutPerson(client);
			Console.WriteLine($"更新结果：{ put }");

			var delete = await DeletePerson(client);
			Console.WriteLine($"删除结果：{ delete }");

			result = await GetPerson(client);
			Console.WriteLine($"查询结果：{ string.Join(",", result) }");
		}

		async Task<Person> AddPerson(HttpClient client)
		{
			var response = await client.PostAsJsonAsync("api/person", new Person() { Ages = 20, Id = 10, Name = "测试", Sex = '男' });
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadAsAsync<Person>();
			return null;
		}

		async Task<IEnumerable<Person>> GetPerson(HttpClient client)
		{
			var response = await client.GetAsync("api/person");
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadAsAsync<IEnumerable<Person>>();
			return null;
		}

		async Task<Person> PutPerson(HttpClient client)
		{
			var response = await client.PutAsJsonAsync("api/person/10", new Person() { Ages = 10, Id = 1, Name = "测试更新", Sex = '男' });
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadAsAsync<Person>();
			return null;
		}

		async Task<int> DeletePerson(HttpClient client)
		{
			var response = await client.DeleteAsync("api/person/10");
			if (response.IsSuccessStatusCode)
				return await response.Content.ReadAsAsync<int>();
			return 0;
		}
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public char Sex { get; set; }
		public int Ages { get; set; }

		public override string ToString()
		{
			return $"{{Id={Id}, Name={Name}, Age={Ages}, Sex={Sex}}}";
		}
	}
}