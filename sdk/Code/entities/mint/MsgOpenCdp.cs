// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
//
/// Represents the transaction message that must be used when wanting to open a
/// Collateralized Debt position that allows to transform the user's
/// Commercio Token into Commercio Cash Credits.
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
    public class MsgOpenCdp : StdMsg
    {

        #region Properties
        [JsonProperty("openCdp", Order = 1)]
        public OpenCdp openCdp { get; private set; }

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
        public MsgOpenCdp(OpenCdp openCdp)
        {
            Trace.Assert(openCdp != null);
            // Assigns the properties
            this.openCdp = openCdp;
            base.setProperties("commercio/MsgOpenCdp", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.openCdp.toJson();
            return wk;
        }
        #endregion

        /*
        #region Properties
        public List<StdCoin> depositAmount { get; private set; }
        public String signerDid { get; private set; }

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
        public MsgOpenCdp(List<StdCoin> depositAmount, String signerDid)
        {
            Trace.Assert(depositAmount != null);
            Trace.Assert(signerDid != null);
            // Assigns the properties
            this.depositAmount = depositAmount;
            this.signerDid = signerDid;
            base.setProperties("commercio/MsgOpenCdp", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = new Dictionary<String, Object>();
            wk.Add("deposit_amount", this.depositAmount?.Select(elem => elem?.toJson()?.ToList()));
            wk.Add("depositor", this.signerDid);
            return wk;
        }
        #endregion
        */
    }
}
