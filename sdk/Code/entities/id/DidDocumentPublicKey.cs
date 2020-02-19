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

        private readonly Dictionary<DidDocumentPubKeyType, String> didDocumentPubKeyType = new Dictionary<DidDocumentPubKeyType, String> {
            { DidDocumentPubKeyType.RSA, "RsaVerificationKey2018" },
            { DidDocumentPubKeyType.ED25519, "Ed25519VerificationKey2018" },
            { DidDocumentPubKeyType.SECP256K1, "Secp256k1VerificationKey2018" },
        };

        #region Properties
        [JsonProperty("id")]
        public String id { get; set; }

        [JsonProperty("type")]
        public DidDocumentPubKeyType type { get; set; }

        [JsonProperty("controller")]
        public String controller { get; set; }

        [JsonProperty("publicKeyHex")]
        public String publicKeyHex { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentPublicKey(String id, DidDocumentPubKeyType type, String controller, String publicKeyHex)
        {
            Trace.Assert(id != null);
            // Trace.Assert(type != null);
            Trace.Assert(controller != null);
            Trace.Assert(publicKeyHex != null);
            this.id = id;
            this.type = type;
            this.controller = controller;
            this.publicKeyHex = publicKeyHex;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentPublicKey(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("id", out outValue))
                this.id = outValue as String;
            if (json.TryGetValue("type", out outValue))
            {
                try
                {
                    // See if the type of Public Key is OK
                    if (didDocumentPubKeyType.TryGetValue((DidDocumentPubKeyType)outValue, out String keyValue))
                    {
                        // OK - The keyType string is OK
                        this.type = (DidDocumentPubKeyType)outValue;
                    }
                    else
                    {
                        // Error - We cannot recognizee the key type
                        System.ArgumentException argEx = new System.ArgumentException($"Unsupported key type value {keyValue} - Supported types: {String.Join(',', (new List<String>(didDocumentPubKeyType.Values)))} ");
                        throw argEx;
                    }
                }
                catch
                {
                    // Error in getting key type
                    // Error - We cannot recognizee the key type
                    System.ArgumentException argEx = new System.ArgumentException($"Invalid key type value - Supported types: {String.Join(',', (new List<String>(didDocumentPubKeyType.Values)))} ");
                    throw argEx;
                }
            }
            if (json.TryGetValue("controller", out outValue))
                this.controller = outValue as String;
            if (json.TryGetValue("publicKeyHex", out outValue))
                this.publicKeyHex = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("id", this.id);
            output.Add("type", didDocumentPubKeyType[this.type]);
            output.Add("controller", this.controller);
            output.Add("publicKeyHex", this.publicKeyHex);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
