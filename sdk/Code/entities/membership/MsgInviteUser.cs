// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting to
/// invite a new user to join the system and being recognized as his invitee.
/// After doing so, if the invited user buys a membership, you will be
/// able to get a reward based on your current membership and the type
/// he has bought.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using commercio.sacco.lib;


namespace commercio.sdk
{
    public class MsgInviteUser : StdMsg
    {
        #region Properties
        public String recipientDid { get; private set; }
        public String senderDid { get; private set; }

        // The override of the value getter is mandatory to obtain a correct codified Json
        public new Dictionary<String, Object> value
        {
            get
            {
                return _toJson();
            }
        }

        #endregion

        #region Constructors

        /// Public constructor.
        public MsgInviteUser(String recipientDid, String senderDid)
        {
            Trace.Assert(recipientDid != null);
            Trace.Assert(senderDid != null);
            // Assigns the properties
            this.recipientDid = recipientDid;
            this.senderDid = senderDid;
            base.setProperties("commercio/MsgInviteUser", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("recipient", this.recipientDid);
            wk.Add("sender", this.senderDid);
            return wk;
        }
        #endregion
    }
}
