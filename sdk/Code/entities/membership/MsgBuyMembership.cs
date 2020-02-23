// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
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
    public class MsgBuyMembership : StdMsg
    {
        #region Properties
        [JsonProperty("membershipType", Order = 2)]
        public MembershipType membershipType { get; private set; }
        [JsonProperty("buyerDid", Order = 1)]
        public String buyerDid { get; private set; }

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
        public MsgBuyMembership(MembershipType membershipType, String buyerDid)
        {
            // Trace.Assert(membershipType != null);
            Trace.Assert(buyerDid != null);
            // Assigns the properties
            this.membershipType = membershipType;
            this.buyerDid = buyerDid;
            base.setProperties("commercio/MsgBuyMembership", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("membership_type", (Enum.GetName(typeof(MembershipType), this.membershipType)).ToLower()); 
            wk.Add("buyer", this.buyerDid);
            return wk;
        }
        #endregion
    }
}
