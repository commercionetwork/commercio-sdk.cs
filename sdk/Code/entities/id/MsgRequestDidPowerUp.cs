// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// Bloc
//
/// Represents the transaction message that should be used when asking
/// for a private Did power up.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgRequestDidPowerUp : StdMsg
    {
        #region Properties
        public String claimantDid { get; private set; }
        public List<StdCoin> amount { get; private set; }
        public String powerUpProof { get; private set; }
        public String encryptionKey { get; private set; }

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
        public MsgRequestDidPowerUp(String claimantDid, List<StdCoin> amount, String powerUpProof, String encryptionKey)
        {
            Trace.Assert(claimantDid != null);
            Trace.Assert(amount != null);
            Trace.Assert(amount.Count > 0);
            Trace.Assert(powerUpProof != null);
            Trace.Assert(encryptionKey != null);
            // Assigns the properties
            this.claimantDid = claimantDid;
            this.amount = amount;
            this.powerUpProof = powerUpProof;
            this.encryptionKey = encryptionKey;
            base.setProperties("commercio/MsgRequestDidPowerUp", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("claimant", this.claimantDid);
            wk.Add("amount", this.amount?.Select(elem => elem?.toJson()?.ToList()));
            wk.Add("proof", this.powerUpProof);
            wk.Add("encryption_key", this.encryptionKey);
            return wk;
        }
        #endregion
    }
}
