// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 10, 2020
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting to buy a membership.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class BuyMembership
    {
        #region Properties
        [JsonProperty("tsp", Order = 3)]
        public String tsp { get; private set; }

        [JsonProperty("membership_type", Order = 2)]
        public String membershipType { get; private set; }
        [JsonProperty("buyer", Order = 1)]
        public String buyerDid { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public BuyMembership(String membershipType, String buyerDid, String tsp)
        {
            Trace.Assert(membershipType != null);
            Trace.Assert(buyerDid != null);
            Trace.Assert(tsp != null);
            this.membershipType = membershipType;
            this.buyerDid = buyerDid;
            this.tsp = tsp;
        }

        // Alternate constructor from Json JObject
        public BuyMembership(JObject json)
        {
            this.membershipType = (String)json["membership_type"];
            this.buyerDid = (String)json["buyer"];
            this.tsp = (String)json["tsp"];

            //Object outValue;
            //if (json.TryGetValue("membership_type", out outValue))
            //    this.membershipType = outValue as String;
            //if (json.TryGetValue("buyer", out outValue))
            //    this.buyerDid = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("membership_type", this.membershipType);
            output.Add("buyer", this.buyerDid);
            output.Add("tsp", this.tsp);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
