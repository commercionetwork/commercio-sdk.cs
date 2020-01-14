// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Message that should be used when wanting to send a document
/// receipt transaction.
//
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgSendDocumentReceipt : StdMsg
    {
        #region Properties

        public CommercioDocReceipt receipt { get; private set; }

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
        public MsgSendDocumentReceipt(CommercioDocReceipt receipt)
        {
            Trace.Assert(receipt != null);
            // Assigns the properties
            this.receipt = receipt;
            base.setProperties("commercio/MsgSendDocumentReceipt", receipt.toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers
        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.receipt.toJson();
            return wk;
        }

        #endregion
    }
}
