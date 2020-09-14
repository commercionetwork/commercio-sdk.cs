// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
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
    //  Dart code marks this class as @JsonSerializable(explicitToJson: true): I have no way to control the toJson behaviour like Dart does
    // I need to call directly the toJson method on the classes! 
    public class DidDocumentProof
    {
        #region Properties
        [JsonProperty("type", Order = 5)]
        public String type { get; set; }

        [JsonProperty("created", Order = 1)]
        public String timestamp { get; set; }

        [JsonProperty("proofPurpose", Order = 3)]
        public String proofPurpose { get; set; }

        [JsonProperty("controller", Order = 2)]
        public String controller { get; set; }

        [JsonProperty("verificationMethod", Order = 6)]
        public String verificationMethod { get; set; }

        [JsonProperty("signatureValue", Order = 4)]
        public String signatureValue { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentProof(String type, String timestamp, String proofPurpose, String controller, String verificationMethod, String signatureValue)
        {
            Trace.Assert(type != null);
            Trace.Assert(timestamp != null);
            Trace.Assert(proofPurpose != null);
            Trace.Assert(controller != null);
            Trace.Assert(verificationMethod != null);
            Trace.Assert(signatureValue != null);
            this.type = type;
            this.timestamp = timestamp;
            this.proofPurpose = proofPurpose;
            this.controller = controller;
            this.verificationMethod = verificationMethod;
            this.signatureValue = signatureValue;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentProof(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("type", out outValue))
                this.type = outValue as String;
            if (json.TryGetValue("created", out outValue))
                this.timestamp = outValue as String;
            if (json.TryGetValue("proofPurpose", out outValue))
                this.proofPurpose = outValue as String;
            if (json.TryGetValue("controller", out outValue))
                this.controller = outValue as String;
            if (json.TryGetValue("verificationMethod", out outValue))
                this.verificationMethod = outValue as String;
            if (json.TryGetValue("signatureValue", out outValue))
                this.signatureValue = outValue as String;
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("type", this.type);
            output.Add("created", this.timestamp);
            output.Add("proofPurpose", this.proofPurpose);
            output.Add("controller", this.controller);
            output.Add("verificationMethod", this.verificationMethod);
            output.Add("signatureValue", this.signatureValue);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}