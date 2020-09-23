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
    public class CloseCdp
    {
        #region Properties
        [JsonProperty("signer", Order = 2)]
        public String signerDid { get; private set; }
        [JsonProperty("cdp_timestamp", Order = 1)]
        public String timeStamp { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CloseCdp(String signerDid, String timeStamp)
        {
            Trace.Assert(signerDid != null);
            Trace.Assert(timeStamp != null);
            this.signerDid = signerDid;
            this.timeStamp = timeStamp;
        }

        // Alternate constructor from Json JObject
        public CloseCdp(JObject json)
        {
            this.signerDid = (String)json["signer"];
            this.timeStamp = (String)json["cdp_timestamp"];

            //Object outValue;
            //if (json.TryGetValue("signer", out outValue))
            //    this.signerDid = outValue as String;
            //if (json.TryGetValue("cdp_timestamp", out outValue))
            //    this.timeStamp = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("signer", this.signerDid);
            output.Add("cdp_timestamp", this.timeStamp);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
