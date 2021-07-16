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
    public class MsgmintCCC : StdMsg
    {

        #region Properties
        [JsonProperty("mintCCC", Order = 1)]
        public mintCCC mintCCC { get; private set; }

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
        public MsgmintCCC(mintCCC mintCCC)
        {
            Trace.Assert(mintCCC != null);
            // Assigns the properties
            this.mintCCC = mintCCC;
            base.setProperties("commercio/MsgmintCCC", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.mintCCC.toJson();
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
        public MsgmintCCC(List<StdCoin> depositAmount, String signerDid)
        {
            Trace.Assert(depositAmount != null);
            Trace.Assert(signerDid != null);
            // Assigns the properties
            this.depositAmount = depositAmount;
            this.signerDid = signerDid;
            base.setProperties("commercio/MsgmintCCC", _toJson());
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
