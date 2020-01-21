// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the JSON object that should be created, signed and put
/// inside a [DidDepositRequestPayload] as the signature value.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class DidDepositRequestSignatureJson
    {
        #region Properties
        [JsonProperty("recipient")]
        public String recipient { get; set; }

        [JsonProperty("timestamp")]
        public String timeStamp { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDepositRequestSignatureJson(String recipient, String timeStamp)
        {
            Trace.Assert(recipient != null);
            Trace.Assert(timeStamp != null);
            this.recipient = recipient;
            this.timeStamp = timeStamp;
        }

        // Alternate constructor from Json Dictionary
        public DidDepositRequestSignatureJson(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("recipient", out outValue))
                this.recipient = outValue as String;
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
            output.Add("recipient", this.recipient);
            output.Add("timestamp", this.timeStamp);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
