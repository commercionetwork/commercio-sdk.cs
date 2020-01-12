// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//
using System;
using System.Collections.Generic;
using System.Text;
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
        [JsonProperty("type")]
        public String type { get; set; }

        [JsonProperty("created")]
        public String iso8601creationTimestamp { get; set; }

        [JsonProperty("creator")]
        public String creatorKeyId { get; set; }

        [JsonProperty("signatureValue")]
        public String signatureValue { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentProof(String type, String iso8601creationTimestamp, String creatorKeyId, String signatureValue)
        {
            Trace.Assert(type != null);
            Trace.Assert(iso8601creationTimestamp != null);
            Trace.Assert(creatorKeyId != null);
            Trace.Assert(signatureValue != null);
            this.type = type;
            this.iso8601creationTimestamp = iso8601creationTimestamp;
            this.creatorKeyId = creatorKeyId;
            this.signatureValue = signatureValue;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentProof(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("type", out outValue))
                this.type = outValue as String;
            if (json.TryGetValue("created", out outValue))
                this.iso8601creationTimestamp = outValue as String;
            if (json.TryGetValue("creator", out outValue))
                this.creatorKeyId = outValue as String;
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
            output.Add("created", this.iso8601creationTimestamp);
            output.Add("creator", this.creatorKeyId);
            output.Add("signatureValue", this.signatureValue);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}