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
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgburnCCC : StdMsg
    {


        #region Properties
        [JsonProperty("burnCCC", Order = 1)]
        public burnCCC burnCCC { get; private set; }

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
        public MsgburnCCC(burnCCC burnCCC)
        {
            Trace.Assert(burnCCC != null);
            // Assigns the properties
            this.burnCCC = burnCCC;
            base.setProperties("commercio/MsgburnCCC", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.burnCCC.toJson();
            return wk;
        }
        #endregion

        /*
        #region Properties
        public String signerDid { get; private set; }
        public int timeStamp { get; private set; }

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
        public MsgburnCCC(String signerDid, int timeStamp)
        {
            Trace.Assert(signerDid != null);
            // Trace.Assert(timeStamp != null);
            // Assigns the properties
            this.signerDid = signerDid;
            this.timeStamp = timeStamp;
            base.setProperties("commercio/MsgburnCCC", _toJson());
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
        */
    }
}
