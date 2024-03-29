﻿// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 10, 2020
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting to buy a membership.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class mintCCC
    {
        #region Properties
        [JsonProperty("deposit_amount", Order = 2)]
        public List<StdCoin> depositAmount { get; private set; }
        [JsonProperty("depositor", Order = 1)]
        public String signerDid { get; private set; }

        [JsonProperty("id", Order = 3)]
        public String id { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public mintCCC(List<StdCoin> depositAmount, String signerDid)
        {
            Trace.Assert(depositAmount != null);
            Trace.Assert(signerDid != null);
            Trace.Assert(id != null);
            Boolean CheckUuid= GenericUtils.matchUuidv4(id); //Check if the uuid is ok
            Trace.Assert(CheckUuid != false);
            this.depositAmount = depositAmount;
            this.signerDid = signerDid;
            this.id = id;
                        
        }

        // Alternate constructor from Json JObject
        public mintCCC(JObject json)
        {
            this.depositAmount = ((JArray)json["deposit_amount"]).Select(elem => (new StdCoin((JObject)elem))).ToList();
            this.signerDid = (String)json["depositor"];
            this.id = (String)json["id"];

            //Object outValue;
            //if (json.TryGetValue("deposit_amount", out outValue))
            //    this.depositAmount = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new StdCoin(elem)).ToList();  // RC - This need to be checked - 20200910;
            //if (json.TryGetValue("depositor", out outValue))
            //    this.signerDid = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("deposit_amount", (this.depositAmount?.Select(elem => elem?.toJson())?.ToList()));  // RC - This need to be checked - 20200910
            output.Add("depositor", this.signerDid);
            output.Add("id", this.id);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
