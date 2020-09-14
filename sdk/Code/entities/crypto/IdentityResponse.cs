// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
//
//
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace commercio.sdk
{
    public class IdentityResponse
    {
        #region Properties

        [JsonProperty("height", Order = 1)]
        public String height { get; set; }
        [JsonProperty("result", Order = 2)]
        public ResultId result { get; set; }

        #endregion

        // Static contructor from raw json 
        static public TumblerResponse fromRawJson(String rawJson)
        {
            Dictionary<String, Object> ObjClassDeJsoned = JsonConvert.DeserializeObject<Dictionary<String, Object>>(rawJson);
            return new TumblerResponse(ObjClassDeJsoned);
        }

        #region Constructors

        public IdentityResponse(String height, ResultId result)
        {
            Trace.Assert(height != null);
            Trace.Assert(result != null);
            this.height = height;
            this.result = result;
        }

        // Alternate constructor from Json Dictionary
        public IdentityResponse(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("height", out outValue))
                this.height = outValue as String;
            if (json.TryGetValue("result", out outValue))
                this.result = new ResultId(outValue as Dictionary<String, Object>);
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("height", this.height);
            output.Add("result", this.result.toJson());
            return (output);
        }

        public String toRawJson()
        {
            String output;

            output = JsonConvert.SerializeObject(this);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion

    }

    public class ResultId
    {
        #region Properties

        [JsonProperty("owner", Order = 2)]
        public String owner { get; set; }
        [JsonProperty("did_document", Order = 1)]
        public DidDocument didDocument { get; set; }

        #endregion

        // Static contructor from raw json 
        // was: Constructor from encoded json
        //      factory Result.fromRawJson(String str) => Result.fromJson(json.decode(str));
        static public ResultId fromRawJson(String rawJson)
        {
            Dictionary<String, Object> ObjClassDeJsoned = JsonConvert.DeserializeObject<Dictionary<String, Object>>(rawJson);
            return new ResultId(ObjClassDeJsoned);
        }

        #region Constructors

        public ResultId(String owner, DidDocument didDocument)
        {
            Trace.Assert(owner != null);
            Trace.Assert(didDocument != null);
            this.owner = owner;
            this.didDocument = didDocument;
        }

        // Alternate constructor from Json Dictionary
        public ResultId(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("owner", out outValue))
                this.owner = outValue as String;
            if (json.TryGetValue("did_document", out outValue))
                this.didDocument = new DidDocument(outValue as Dictionary<String, Object>);
        }

        #endregion

        #region Public Methods

        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("owner", this.owner);
            output.Add("did_document", this.didDocument.toJson());
            return (output);
        }

        public String toRawJson()
        {
            String output;

            output = JsonConvert.SerializeObject(this);
            return (output);
        }
        #endregion

        #region Helpers
        #endregion

    }
}
