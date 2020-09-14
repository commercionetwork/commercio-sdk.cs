// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 10, 2020
// BlockIt s.r.l.
// 
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class RequestDidPowerUp
    {
        #region Properties
        [JsonProperty("claimant", Order = 2)]
        public String claimantDid { get; set; }

        [JsonProperty("amount", Order = 1)]
        public List<StdCoin> amount { get; set; }

        [JsonProperty("proof", Order = 4)]
        public String powerUpProof { get; set; }

        [JsonProperty("id", Order = 3)]
        public String uuid { get; set; }

        [JsonProperty("proof_key", Order = 5)]
        public String encryptionKey { get; set; }


        #endregion

        #region Constructors
        [JsonConstructor]
        public RequestDidPowerUp(String claimantDid, List<StdCoin> amount, String powerUpProof, String uuid, String encryptionKey)
        {
            Trace.Assert(claimantDid != null);
            Trace.Assert(amount != null);
            Trace.Assert(powerUpProof != null);
            Trace.Assert(uuid != null);
            Trace.Assert(encryptionKey != null);
            this.claimantDid = claimantDid;
            this.amount = amount;
            this.powerUpProof = powerUpProof;
            this.uuid = uuid;
            this.encryptionKey = encryptionKey;
        }

        // Alternate constructor from Json Dictionary
        public RequestDidPowerUp(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("claimant", out outValue))
                this.claimantDid = outValue as String;
            if (json.TryGetValue("amount", out outValue))
                this.amount = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new StdCoin(elem)).ToList();  // RC - This need to be checked - 20200910
            if (json.TryGetValue("proof", out outValue))
                this.powerUpProof = outValue as String;
            if (json.TryGetValue("id", out outValue))
                this.uuid = outValue as String;
            if (json.TryGetValue("proof_key", out outValue))
                this.encryptionKey = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("claimant", this.claimantDid);
            output.Add("amount", this.amount?.Select(elem => elem?.toJson()?.ToList()));  // RC - This need to be checked - 20200910
            output.Add("proof", this.powerUpProof);
            output.Add("id", this.uuid);
            output.Add("proof_key", this.encryptionKey);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           