// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MsgShareDocument : StdMsg
    {
        #region Properties

        public CommercioDoc document { get; private set; }

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
        public MsgShareDocument(CommercioDoc document)
        {
            Trace.Assert(document != null);
            // Assigns the properties
            this.document = document;
            base.setProperties("commercio/MsgShareDocument", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers
        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.document.toJson();
            return wk;
        }

        #endregion
    }
}
