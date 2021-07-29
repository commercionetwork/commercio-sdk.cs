using commercio.sacco.lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commercio.sdk.Code.entities.mint
{
    public class ExchangeTradePosition
    {
        [JsonProperty("created_at", Order =1)]
        public DateTime createdAt { get; set; }
        public string id;
        public string owner;
        [JsonProperty("exchange_rate", Order = 2)]
        public string exchangeRate { get; set; }
        public string collateral;
        public StdCoin credits;

        #region Costructor
        [JsonConstructor]
        public ExchangeTradePosition(DateTime createdAt,string id,string owner,string exchange_rate,string collateral,StdCoin credits)
        {
            this.createdAt = createdAt;
            this.id = id;
            this.owner = owner;
            this.exchangeRate = exchange_rate;
            this.collateral = collateral;
            this.credits = credits;
        }

        public ExchangeTradePosition(JObject json)
        {
            this.createdAt = (DateTime)json["created_at"];
            this.id = (String)json["id"];
            this.owner = (String)json["owner"];
            this.exchangeRate = (String)json["exchange_rate"];
            this.collateral = (String)json["collateral"];
            this.credits = new StdCoin(json["credits"] as JObject);
        }
        #endregion
    }
}
