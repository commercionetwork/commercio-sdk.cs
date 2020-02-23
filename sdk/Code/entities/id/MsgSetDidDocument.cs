// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Message that must be used when setting a Did document.
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
    public class MsgSetDidDocument : StdMsg
    {
        #region Properties
        [JsonProperty("didDocument", Order = 1)]
        public DidDocument didDocument { get; private set; }

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
        public MsgSetDidDocument(DidDocument didDocument)
        {
            Trace.Assert(didDocument != null);
            // Assigns the properties
            this.didDocument = didDocument;
            base.setProperties("commercio/MsgSetIdentity", _toJson());
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers

        private Dictionary<String, Object> _toJson()
        {
            Dictionary<String, Object> wk = this.didDocument.toJson();
            return wk;
        }
        #endregion
    }
}
