// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the message that should be included into a transaction
/// when wanting to deposit a specific amount of tokens that can later
/// be used to power up private DIDs.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgRequestDidDeposit : StdMsg
    {
        #region Properties
        [JsonProperty("recipientDid", Order = 4)]
        public String recipientDid { get; private set; }
        [JsonProperty("amount", Order = 1)]
        public List<StdCoin> amount { get; private set; }
        [JsonProperty("depositProof", Order = 2)]
        public String depositProof { get; private set; }
        [JsonProperty("encryptionKey", Order = 3)]
        public String encryptionKey { get; private set; }
        [JsonProperty("senderDid", Order = 5)]
        public String senderDid { get; private set; }

        // The override of the value getter is mandatory to obtain a correct codified Json
        public override Dictionary<String, Object> value
        {
            get
            {
                return _toJson();
            }
        }

        #endregion

        #region Constructors

        /// Public constructor.
        public MsgRequestDidDeposit(String recipientDid, List<StdCoin> amount, String depositProof, String encryptionKey, String senderDid)
        {
            Trace.Assert(recipientDid != null);
            Trace.Assert(amount != null);
            Trace.Assert(amount.Count > 0);
            Trace.Assert(depositProof != null);
            Trace.Assert(encryptionKey != null);
            Trace.Assert(senderDid != null);
            // Assigns the properties
            this.recipientDid = recipientDid;
            this.amount = amount;
            this.depositProof = depositProof;
            this.encryptionKey = encryptionKey;
            this.senderDid = senderDid;
            base.setProperties("commercio/MsgRequestDidDeposit", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("recipient", this.recipientDid);
            wk.Add("amount", this.amount?.Select(elem => elem?.toJson()?.ToList()));
            wk.Add("proof", this.depositProof);
            wk.Add("encryption_key", this.encryptionKey);
            wk.Add("from_address", this.senderDid);
            return wk;
        }
        #endregion
    }
}
