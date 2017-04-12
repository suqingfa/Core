using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace REST.Test
{
	public class Test
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public Test()
		{
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			_client = _server.CreateClient();
		}

		[Fact]
		public async Task GetAllTest()
		{
			var response = await _client.GetAsync("/api/person");
			response.EnsureSuccessStatusCode();

			var persons = await response.Content.ReadAsAsync<IEnumerable<Person>>();

			Assert.Equal(2, persons.Count());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task GetTest(int id)
		{
			var response = await _client.GetAsync($"/api/person/{id}");
			response.EnsureSuccessStatusCode();

			var person = await response.Content.ReadAsAsync<Person>();

			Assert.NotNull(person);
		}
	}
}
