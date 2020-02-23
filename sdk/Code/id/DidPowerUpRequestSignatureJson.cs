// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the JSON object that should be signed and put
/// inside a [DidPowerUpRequestPayload] as the signature value.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class DidPowerUpRequestSignatureJson
    {
        #region Properties
        [JsonProperty("pairwise_did", Order = 1)]
        public String pairwiseDid { get; set; }

        [JsonProperty("timestamp", Order = 2)]
        public String timeStamp { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidPowerUpRequestSignatureJson(String pairwiseDid, String timeStamp)
        {
            Trace.Assert(pairwiseDid != null);
            Trace.Assert(timeStamp != null);
            this.pairwiseDid = pairwiseDid;
            this.timeStamp = timeStamp;
        }

        // Alternate constructor from Json Dictionary
        public DidPowerUpRequestSignatureJson(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("pairwise_did", out outValue))
                this.pairwiseDid = outValue as String;
            if (json.TryGetValue("timestamp", out outValue))
                this.timeStamp = outValue as String;
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("pairwise_did", this.pairwiseDid);
            output.Add("timestamp", this.timeStamp);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
