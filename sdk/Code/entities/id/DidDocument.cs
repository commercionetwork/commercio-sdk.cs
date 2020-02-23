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

        [JsonProperty("id", Order = 4)]
        public String id { get; set; }

        [JsonProperty("publicKey", Order = 6)]
        public List<DidDocumentPublicKey> publicKeys { get; set; }

        [JsonProperty("authentication", Order = 2)]
        public List<String> authentication { get; set; }

        [JsonProperty("proof", Order = 5)]
        public DidDocumentProof proof { get; set; }

        [JsonProperty("service", Order = 7)]
        public List<DidDocumentService> services { get; set; }

        /// Returns the [PublicKey] that should be used as the public encryption
        /// key when encrypting data that can later be read only by the owner of
        /// this Did Document.
        [JsonProperty("encryptionKey", Order = 3)]
        public RSAPublicKey encryptionKey
        {
            get
            {
                DidDocumentPublicKey pubKey = publicKeys.FirstOrDefault(key => key.type == DidDocumentPubKeyType.RSA);
                if (pubKey == null)
                    return null;
                // If existent, creates the RSA public key
                BigInteger modulus = new BigInteger(pubKey.publicKeyHex, radix: 16);
                BigInteger exponent = new BigInteger("65537", radix: 10);
                return new RSAPublicKey(new RsaKeyParameters(false, modulus, exponent));
            }
        }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocument(String context, String id, List<DidDocumentPublicKey> publicKeys, List<String> authentication, DidDocumentProof proof, List<DidDocumentService> services)
        {
            Trace.Assert(context != null);
            Trace.Assert(id != null);
            Trace.Assert(publicKeys != null);
            Trace.Assert(authentication != null);
            Trace.Assert(proof != null);
            this.context = context;
            this.id = id;
            this.publicKeys = publicKeys;
            this.authentication = authentication;
            this.proof = proof;
            this.services = services;
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
            if (json.TryGetValue("authentication", out outValue))
                this.authentication = outValue as List<String>; // *** To be checked - The List of Strings are not passed to Json Dictionaries
            if (json.TryGetValue("proof", out outValue))
               this.proof = (outValue == null ? null : new DidDocumentProof( (Dictionary<String, Object>) outValue));
            if (json.TryGetValue("service", out outValue))
                this.services = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new DidDocumentService(elem)).ToList();
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
            output.Add("authentication", this.authentication);
            output.Add("proof", this.proof?.toJson());
            output.Add("service", this.services?.Select(elem => elem?.toJson()?.ToList()));
            return (output);
        }

    #endregion

    #region Helpers
    #endregion
    }
}
