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
        [JsonProperty("@context", Order = 1)]
        public String context { get; set; }

        [JsonProperty("id", Order = 2)]
        public String id { get; set; }

        [JsonProperty("publicKey", Order = 3)]
        public List<DidDocumentPublicKey> publicKeys { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public DidDocumentProofSignatureContent(String context, String id, List<DidDocumentPublicKey> publicKeys)
        {
            Trace.Assert(context != null);
            Trace.Assert(id != null);
            Trace.Assert(publicKeys != null);
            this.context = context;
            this.id = id;
            this.publicKeys = publicKeys;
        }

        // Alternate constructor from Json Dictionary
        public DidDocumentProofSignatureContent(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("@context", out outValue))
                this.context = outValue as String;
            if (json.TryGetValue("id", out outValue))
                this.id = outValue as String;
            if (json.TryGetValue("publicKey", out outValue))
                this.publicKeys = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new DidDocumentPublicKey(elem)).ToList();  // RC - This need to be checked - 20200910;
        }

        #endregion

        #region Public Methods
        // No toJson problem here, all simple types
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("@context", this.context);
            output.Add("id", this.id);
            output.Add("publicKey", this.publicKeys?.Select(elem => elem?.toJson()?.ToList()));  // RC - This need to be checked - 20200910
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
