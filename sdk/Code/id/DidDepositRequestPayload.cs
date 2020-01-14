// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the JSON object that should be encrypted and put
/// inside a [MsgRequestDidDeposit] as its payload.
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
    public class DidDepositRequestPayload
    {
        #region Properties
        [JsonProperty("recipient")]
        public String recipient { get; set; }

        [JsonProperty("timestamp")]
        public String timeStamp { get; set; }

        [JsonProperty("signature")]
        public String signature { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDepositRequestPayload(String recipient, String timeStamp, String signature)
        {
            Trace.Assert(recipient != null);
            Trace.Assert(timeStamp != null);
            Trace.Assert(signature != null);
            this.recipient = recipient;
            this.timeStamp = timeStamp;
            this.signature = signature;
        }

        // Alternate constructor from Json Dictionary
        public DidDepositRequestPayload(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("recipient", out outValue))
                this.recipient = outValue as String;
            if (json.TryGetValue("timestamp", out outValue))
                this.timeStamp = outValue as String;
            if (json.TryGetValue("signature", out outValue))
                this.signature = outValue as String;
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
            output.Add("signature", this.signature);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
