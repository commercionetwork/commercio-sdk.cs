// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 10, 2020
// BlockIt s.r.l.
// 
/// Represents the transaction message that must be used when wanting to buy a membership.
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class OpenCdp
    {
        #region Properties
        [JsonProperty("deposit_amount", Order = 2)]
        public List<StdCoin> depositAmount { get; private set; }
        [JsonProperty("depositor", Order = 1)]
        public String signerDid { get; private set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public OpenCdp(List<StdCoin> depositAmount, String signerDid)
        {
            Trace.Assert(depositAmount != null);
            Trace.Assert(signerDid != null);
            this.depositAmount = depositAmount;
            this.signerDid = signerDid;
        }

        // Alternate constructor from Json Dictionary
        public OpenCdp(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("deposit_amount", out outValue))
                this.depositAmount = (outValue as List<Dictionary<String, Object>>)?.Select(elem => new StdCoin(elem)).ToList();  // RC - This need to be checked - 20200910;
            if (json.TryGetValue("depositor", out outValue))
                this.signerDid = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("deposit_amount", this.depositAmount?.Select(elem => elem?.toJson()?.ToList()));  // RC - This need to be checked - 20200910
            output.Add("depositor", this.signerDid);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
