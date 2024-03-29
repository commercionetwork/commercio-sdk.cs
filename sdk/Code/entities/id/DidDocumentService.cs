﻿// 
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
using Newtonsoft.Json.Linq;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    //  Dart code marks this class as @JsonSerializable(explicitToJson: true): I have no way to control the toJson behaviour like Dart does
    // I need to call directly the toJson method on the classes! 
    public class DidDocumentService
    {
        #region Properties
        [JsonProperty("id", Order = 2)]
        public String id { get; set; }

        [JsonProperty("type", Order = 3)]
        public String type { get; set; }

        [JsonProperty("serviceEndpoint", Order = 1)]
        public String endpoint { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentService(String id, String type, String endpoint)
        {
            Trace.Assert(id != null);
            Trace.Assert(id.Length <= 64, "service.id must have a valid length");
            Trace.Assert(type != null);
            Trace.Assert(type.Length <= 64, "service.type must have a valid length");
            Trace.Assert(endpoint != null);
            Trace.Assert(endpoint.Length <= 512, "service.endpoint must have a valid length");
            this.id = id;
            this.type = type;
            this.endpoint = endpoint;
        }

        // Alternate constructor from Json JObject
        public DidDocumentService(JObject json)
        {
            this.id = (String)json["id"];
            this.type = (String)json["type"];
            this.endpoint = (String)json["serviceEndpoint"];

            //Object outValue;
            //if (json.TryGetValue("id", out outValue))
            //    this.id = outValue as String;
            //if (json.TryGetValue("type", out outValue))
            //    this.type = outValue as String;
            //if (json.TryGetValue("serviceEndpoint", out outValue))
            //    this.endpoint = outValue as String;
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
