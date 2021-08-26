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
    public class burnCCC
    {
        #region Properties
        [JsonProperty("signer", Order = 2)]
        public String signerDid { get; private set; }
        [JsonProperty("etp_timestamp", Order = 1)]
        public String timeStamp { get; private set; }

        [JsonProperty("id", Order = 3)]
        public String id { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public burnCCC(String signerDid, String timeStamp)
        {
            Trace.Assert(signerDid != null);
            Trace.Assert(timeStamp != null);
            Trace.Assert(id != null);
            Boolean CheckUuid = GenericUtils.matchUuidv4(id); //Check if the uuid is ok
            Trace.Assert(CheckUuid != false);
            this.signerDid = signerDid;
            this.timeStamp = timeStamp;
            this.id = id;
        }

        // Alternate constructor from Json JObject
        public burnCCC(JObject json)
        {
            this.signerDid = (String)json["signer"];
            this.timeStamp = (String)json["etp_timestamp"];
            this.id = (String)json["id"];

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
            output.Add("etp_timestamp", this.timeStamp);
            output.Add("id", this.id);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
