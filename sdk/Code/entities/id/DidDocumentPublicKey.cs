// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Contains the data of public key contained inside a Did document.'
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace commercio.sdk
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DidDocumentPubKeyType
    {
        [EnumMember(Value = "RsaVerificationKey2018")]
        RSA,
        [EnumMember(Value = "Ed25519VerificationKey2018")]
        ED25519,
        [EnumMember(Value = "Secp256k1VerificationKey2018")]
        SECP256K1
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects

    //  Dart code marks this class as @JsonSerializable(explicitToJson: true): I have no way to control the toJson behaviour like Dart does
    // I need to call directly the toJson method on the classes! 
    public class DidDocumentPublicKey
    {
        /*
        private readonly Dictionary<DidDocumentPubKeyType, String> didDocumentPubKeyType = new Dictionary<DidDocumentPubKeyType, String> {
            { DidDocumentPubKeyType.RSA, "RsaVerificationKey2018" },
            { DidDocumentPubKeyType.ED25519, "Ed25519VerificationKey2018" },
            { DidDocumentPubKeyType.SECP256K1, "Secp256k1VerificationKey2018" },
        };
        */

        #region Properties
        [JsonProperty("id", Order = 2)]
        public String id { get; set; }

        [JsonProperty("type", Order = 4)]
        public String type { get; set; }

        [JsonProperty("controller", Order = 1)]
        public String controller { get; set; }

        [JsonProperty("publicKeyPem", Order = 3)]
        public String publicKeyPem { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentPublicKey(String id, String type, String controller, String publicKeyPem)
        {
            Trace.Assert(id != null);
            Trace.Assert(type != null);
            Trace.Assert(controller != null);
            Trace.Assert(publicKeyPem != null);
            this.id = id;
            this.type = type;
            this.controller = controller;
            this.publicKeyPem = publicKeyPem;
        }

        // Alternate constructor from Json JObject
        public DidDocumentPublicKey(JObject json)
        {
            this.id = (String)json["id"]; ;
            this.type = (String)json["type"];
            this.controller = (String)json["controller"];
            this.publicKeyPem = (String)json["publicKeyPem"];
            
            //Object outValue;
            //if (json.TryGetValue("id", out outValue))
            //    this.id = outValue as String;
            //if (json.TryGetValue("type", out outValue))
            //    this.type = outValue as String;
            //if (json.TryGetValue("controller", out outValue))
            //    this.controller = outValue as String;
            //if (json.TryGetValue("publicKeyPem", out outValue))
            //    this.publicKeyPem = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("id", this.id);
            output.Add("type", this.type);
            output.Add("controller", this.controller);
            output.Add("publicKeyPem", this.publicKeyPem);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
