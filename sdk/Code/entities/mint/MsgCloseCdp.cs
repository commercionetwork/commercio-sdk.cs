// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting
/// to close a previously opened Collateralized Debt position to get
/// back the Commercio Tokens that have been locked with it.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgCloseCdp : StdMsg
    {
        #region Properties
        public String signerDid { get; private set; }
        public int timeStamp { get; private set; }

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
        public MsgCloseCdp(String signerDid, int timeStamp)
        {
            Trace.Assert(signerDid != null);
            // Trace.Assert(timeStamp != null);
            // Assigns the properties
            this.signerDid = signerDid;
            this.timeStamp = timeStamp;
            base.setProperties("commercio/MsgCloseCdp", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("signer", this.signerDid);
            wk.Add("cdp_timestamp", this.timeStamp.ToString());
            return wk;
        }
        #endregion
    }
}
