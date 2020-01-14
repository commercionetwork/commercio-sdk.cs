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
using System.Linq;
using Newtonsoft.Json;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class DidDocumentProofSignatureContent
    {
        #region Properties
        [JsonProperty("@context")]
        public String context { get; set; }

        [JsonProperty("id")]
        public String did { get; set; }

        [JsonProperty("publicKey")]
        public List<DidDocumentPublicKey> publicKeys { get; set; }

        [JsonProperty("authentication")]
        public List<String> authentication { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentProofSignatureContent(String context, String did, List<DidDocumentPublicKey> publicKeys, List<String> authentication)
        {
            Trace.Assert(context != null);
            Trace.Assert(did != null);
            Trace.Assert(publicKeys != null);
            Trace.Assert(authentication != null);
            this.context = context;
            this.did = did;
            this.publicKeys = publicKeys;
            this.authentication = authentication;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentProofSignatureContent(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("@context", out outValue))
                this.context = outValue as String;
            if (json.TryGetValue("id", out outValue))
                this.did = outValue as String;
            if (json.TryGetValue("timestamp", out outValue))
                this.publicKeys = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new DidDocumentPublicKey(elem)).ToList();
            if (json.TryGetValue("authentication", out outValue))
                this.authentication = outValue as List<String>; // *** To be checked - The List of Strings are not passed to Json Dictionaries
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("@context", this.context);
            output.Add("id", this.did);
            output.Add("publicKey", this.publicKeys);
            output.Add("authentication", this.authentication);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
