using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockQuote.Dtos
{

    public class QuoteResponse
    {
        [JsonProperty("Current")]
        public float c { get; set; }

        [JsonProperty("High")]
        public float h { get; set; }

        [JsonProperty("Low")]
        public float l { get; set; }

       [JsonProperty("Open")]
        public float o { get; set; }

        [JsonProperty("PreviousClose")]
        public float pc { get; set; }

        [JsonProperty("Timestamp")]
        public int t { get; set; }
    }

}
