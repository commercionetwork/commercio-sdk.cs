// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Commercio network's did document is described here:
/// https://scw-gitlab.zotsell.com/Commercio.network/Cosmos-application/blob/master/Commercio%20Decentralized%20ID%20framework.md
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    //  Dart code marks this class as @JsonSerializable(explicitToJson: true): I have no way to control the toJson behaviour like Dart does
    // I need to call directly the toJson method on the classes! 
    public class DidDocument
    {
        #region Properties
        [JsonProperty("@context", Order = 1)]
        public String context { get; set; }

        [JsonProperty("id", Order = 3)]
        public String id { get; set; }

        [JsonProperty("publicKey", Order = 5)]
        public List<DidDocumentPublicKey> publicKeys { get; set; }

        // No more present in 2.1. package
        //[JsonProperty("authentication", Order = 2)]
        //public List<String> authentication { get; set; }

        [JsonProperty("proof", Order = 4)]
        public DidDocumentProof proof { get; set; }

        [JsonProperty("service", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public List<DidDocumentService> service { get; set; }

        /// Returns the [PublicKey] that should be used as the public encryption
        /// key when encrypting data that can later be read only by the owner of
        /// this Did Document.
        [JsonProperty("encryptionKey", Order = 2)]
        public RSAPublicKey encryptionKey
        {
            get
            {
                DidDocumentPublicKey pubKey = publicKeys.FirstOrDefault(key => key.type == "RsaVerificationKey2018");
                if (pubKey == null)
                    return null;
                return new RSAPublicKey(RSAKeyParser.parsePublicKeyFromPem(pubKey.publicKeyPem));
                //// If existent, creates the RSA public key
                //BigInteger modulus = new BigInteger(pubKey.publicKeyHex, radix: 16);
                //BigInteger exponent = new BigInteger("65537", radix: 10);
                //return new RSAPublicKey(new RsaKeyParameters(false, modulus, exponent));
            }
        }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocument(String context, String id, List<DidDocumentPublicKey> publicKeys, DidDocumentProof proof, List<DidDocumentService> service)
        {
            Trace.Assert(context != null);
            Trace.Assert(id != null);
            Trace.Assert(publicKeys != null);
            Trace.Assert(proof != null);
            this.context = context;
            this.id = id;
            this.publicKeys = publicKeys;
            this.proof = proof;
            this.service = service;
        }

        // Alternate constructor from Json Dictionary
        public DidDocument(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("@context", out outValue))
                this.context = outValue as String;
            if (json.TryGetValue("id", out outValue))
                this.id = outValue as String;
            if (json.TryGetValue("publicKey", out outValue))
                this.publicKeys = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new DidDocumentPublicKey(elem)).ToList();
            if (json.TryGetValue("proof", out outValue))
               this.proof = (outValue == null ? null : new DidDocumentProof( (Dictionary<String, Object>) outValue));
            if (json.TryGetValue("service", out outValue))
                this.service = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new DidDocumentService(elem)).ToList();
        }

        #endregion

        #region Public Methods
        // I need to call directly the toJson method on the classes! 
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("@context", this.context);
            output.Add("id", this.id);
            output.Add("publicKey", this.publicKeys?.Select(elem => elem?.toJson()?.ToList()));
            output.Add("proof", this.proof?.toJson());
            output.Add("service", this.service?.Select(elem => elem?.toJson()?.ToList()));
            return (output);
        }

    #endregion

    #region Helpers
    #endregion
    }
}
