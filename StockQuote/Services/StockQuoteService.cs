using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using StockQuote.Dtos;

namespace StockQuote.Services
{
    public class StockQuoteService
    {
        private readonly HttpClient _httpClient;
        
        public StockQuoteService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Environment.GetEnvironmentVariable("finhub_api_baseUrl")) };
            _httpClient.DefaultRequestHeaders.Add("X-Finnhub-Token", Environment.GetEnvironmentVariable("finhub_api_token"));
        }

        public async Task<QuoteResponse> GetStockQuote(string symbol)
        {
            var quotePath = $"quote?symbol={symbol}";

            var response = await _httpClient.GetAsync(quotePath);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<QuoteResponse>();
  
        }
    }
}
