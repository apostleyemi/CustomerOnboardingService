using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace CustomerOnboardingService.InterfaceRepositories.Repositories
{
	public class BankRepository : IBank
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BankRepository(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<List<BankDto>> GetBankList()
		{
			var bankData = new List<BankDto>();
			var httpRequestMessage = new HttpRequestMessage(
			HttpMethod.Get,"https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/GetAllBanks")
			{
				Headers =
			{
				{ HeaderNames.Accept, "application/vnd.github.v3+json" },
				{ HeaderNames.UserAgent, "HttpRequestsSample" },
				 { "Accept", "application/vnd.github.v3+json" },
				{ "User-Agent", "HttpRequestsSample" },
				{ "Cache-Control", "no-cache" },
				{ "Ocp-Apim-Subscription-Key", "080561.Cse" }

			}
			};
			try
			{
				var httpClient = _httpClientFactory.CreateClient();
				var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

				if (httpResponseMessage.IsSuccessStatusCode)
				{
					using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
					var response = await JsonSerializer.DeserializeAsync<BankResponse>(contentStream);
					bankData = response?.Result ?? new List<BankDto>();
				}
				
			}
			catch (Exception ex)
			{

                Console.WriteLine(ex);
            }
			
			return bankData;
		}
	}
}
