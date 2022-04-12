using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using StockQuote.Services;
using StockQuote.Dtos;

namespace StockQuote
{
    public class GetStockPrice
    {
        private readonly ILogger _logger;
        private readonly StockQuoteService _stockQuoteService;

        public GetStockPrice(ILoggerFactory loggerFactory, StockQuoteService stockQuoteService)
        {
            _logger = loggerFactory.CreateLogger<GetStockPrice>();
            _stockQuoteService = stockQuoteService;
        }

        [Function("GetStockPrice")]
        [OpenApiOperation(operationId: "GetStockPrice", tags: new[] { "stock-price/symbol" })]
        [OpenApiParameter(name: "symbol", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Symbol to get stock data from")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "OK response")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "stock-price/symbol/{symbol:alpha}/open")] HttpRequestData req, string symbol)
        {
            _logger.LogInformation($"Getting open stock price for symbol: {symbol}");

            var quoteResponse = await _stockQuoteService.GetStockQuote(symbol);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync<QuoteResponse>(quoteResponse);

            return response;
        }
    }
}
