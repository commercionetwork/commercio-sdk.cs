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
    public class DidDocumentService
    {
        #region Properties
        [JsonProperty("id")]
        public String id { get; set; }

        [JsonProperty("type")]
        public String type { get; set; }

        [JsonProperty("serviceEndpoint")]
        public String endpoint { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentService(String id, String type, String endpoint)
        {
            Trace.Assert(id != null);
            Trace.Assert(type != null);
            Trace.Assert(endpoint != null);
            this.id = id;
            this.type = type;
            this.endpoint = endpoint;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentService(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("id", out outValue))
                this.id = outValue as String;
            if (json.TryGetValue("type", out outValue))
                this.type = outValue as String;
            if (json.TryGetValue("serviceEndpoint", out outValue))
                this.endpoint = outValue as String;
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("id", this.id);
            output.Add("type", this.type);
            output.Add("serviceEndpoint", this.endpoint);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
