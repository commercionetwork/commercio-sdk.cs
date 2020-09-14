// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 10, 2020
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting to buy a membership.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using commercio.sacco.lib;
using Newtonsoft.Json;

namespace commercio.sdk
{
    public class InviteUser
    {
        #region Properties
        [JsonProperty("recipient", Order = 1)]
        public String recipientDid { get; private set; }
        [JsonProperty("sender", Order = 2)]
        public String senderDid { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public InviteUser(String recipientDid, String senderDid)
        {
            Trace.Assert(recipientDid != null);
            Trace.Assert(senderDid != null);
            this.recipientDid = recipientDid;
            this.senderDid = senderDid;
        }

        // Alternate constructor from Json Dictionary
        public InviteUser(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("recipient", out outValue))
                this.recipientDid = outValue as String;
            if (json.TryGetValue("sender", out outValue))
                this.senderDid = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("recipient", this.recipientDid);
            output.Add("sender", this.senderDid);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
