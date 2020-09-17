// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
//
/// Represents the payload that should be put inside a
/// [MsgRequestDidPowerUp] message.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class DidPowerUpRequestPayload
    {
        #region Properties
        [JsonProperty("sender_did", Order = 2)]
        public String senderDid { get; set; }

        [JsonProperty("pairwise_did", Order = 1)]
        public String pairwiseDid { get; set; }

        [JsonProperty("timestamp", Order = 4)]
        public String timeStamp { get; set; }

        [JsonProperty("signature", Order = 3)]
        public String signature { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidPowerUpRequestPayload(String senderDid, String pairwiseDid, String timeStamp, String signature)
        {
            Trace.Assert(senderDid != null);
            Trace.Assert(pairwiseDid != null);
            Trace.Assert(timeStamp != null);
            Trace.Assert(signature != null);
            this.senderDid = senderDid;
            this.pairwiseDid = pairwiseDid;
            this.timeStamp = timeStamp;
            this.signature = signature;
        }

        // Alternate constructor from Json JObject
        public DidPowerUpRequestPayload(JObject json)
        {
            this.senderDid = (String)json["sender_did"];
            this.pairwiseDid = (String)json["pairwise_did"];
            this.timeStamp = (String)json["timestamp"];
            this.signature = (String)json["signature"];

            //Object outValue;
            //if (json.TryGetValue("sender_did", out outValue))
            //    this.senderDid = outValue as String;
            //if (json.TryGetValue("pairwise_did", out outValue))
            //    this.pairwiseDid = outValue as String;
            //if (json.TryGetValue("timestamp", out outValue))
            //    this.timeStamp = outValue as String;
            //if (json.TryGetValue("signature", out outValue))
            //    this.signature = outValue as String;
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("sender_did", this.senderDid);
            output.Add("pairwise_did", this.pairwiseDid);
            output.Add("timestamp", this.timeStamp);
            output.Add("signature", this.signature);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
