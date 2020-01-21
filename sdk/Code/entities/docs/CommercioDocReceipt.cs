// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents a document receipt that indicates that the document having
/// the given [documentUuid] present inside the transaction with has [txHash]
/// and sent by [recipientDid] has been received from the [senderDid].
//
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocReceipt
    {
        #region Properties
        [JsonProperty("uuid")]
        public String uuid { get; set; }

        [JsonProperty("sender")]
        public String senderDid { get; set; }

        [JsonProperty("recipient")]
        public String recipientDid { get; set; }

        [JsonProperty("tx_hash")]
        public String txHash { get; set; }

        [JsonProperty("document_uuid")]
        public String documentUuid { get; set; }

         /// Optional reading proof
        [JsonProperty("proof")]
        public String proof { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocReceipt(String uuid, String senderDid, String recipientDid, String txHash, String documentUuid, String proof = "")
        {
            Trace.Assert(uuid != null);
            Trace.Assert(senderDid != null);
            Trace.Assert(recipientDid != null);
            Trace.Assert(txHash != null);
            Trace.Assert(documentUuid != null);
            Trace.Assert(proof != null);
            this.uuid = uuid;
            this.senderDid = senderDid; 
            this.recipientDid = recipientDid;
            this.txHash = txHash;
            this.documentUuid = documentUuid;
            this.proof = proof;
        }

        // Alternate constructor from Json Dictionary
        public CommercioDocReceipt(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("uuid", out outValue))
                this.uuid = outValue as String;
            if (json.TryGetValue("sender", out outValue))
                this.senderDid = outValue as String;
            if (json.TryGetValue("recipient", out outValue))
                this.recipientDid = outValue as String;
            if (json.TryGetValue("tx_hash", out outValue))
                this.txHash = outValue as String;
            if (json.TryGetValue("document_uuid", out outValue))
                this.documentUuid = outValue as String;
            if (json.TryGetValue("proof", out outValue))
                this.proof = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("uuid", this.uuid);
            output.Add("sender", this.senderDid);
            output.Add("recipient", this.recipientDid);
            output.Add("tx_hash", this.txHash);
            output.Add("document_uuid", this.documentUuid);
            output.Add("proof", this.proof);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
