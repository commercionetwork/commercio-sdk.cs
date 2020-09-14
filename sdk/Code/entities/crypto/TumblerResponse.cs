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
    public class TumblerResponse
    {
        #region Properties

        [JsonProperty("height", Order = 1)]
        public String height { get; set; }
        [JsonProperty("result", Order = 2)]
        public Result result { get; set; }

        #endregion

        // Static contructor from raw json 
        static public TumblerResponse fromRawJson(String rawJson)
        {
            Dictionary<String, Object> ObjClassDeJsoned = JsonConvert.DeserializeObject<Dictionary<String, Object>>(rawJson);
            return new TumblerResponse(ObjClassDeJsoned);
        }

        #region Constructors

        public TumblerResponse(String height, Result result)
        {
            Trace.Assert(height != null);
            Trace.Assert(result != null);
            this.height = height;
            this.result = result;
        }

        // Alternate constructor from Json Dictionary
        public TumblerResponse(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("height", out outValue))
                this.height = outValue as String;
            if (json.TryGetValue("result", out outValue))
                this.result = new Result(outValue as Dictionary<String, Object>);
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

    public class Result
    {
        #region Properties

        [JsonProperty("tumbler_address", Order = 1)]
        public String tumblerAddress { get; set; }

        #endregion

        // Static contructor from raw json 
        // was: Constructor from encoded json
        //      factory Result.fromRawJson(String str) => Result.fromJson(json.decode(str));
        static public Result fromRawJson(String rawJson)
        {
            Dictionary<String, Object> ObjClassDeJsoned = JsonConvert.DeserializeObject<Dictionary<String, Object>>(rawJson);
            return new Result(ObjClassDeJsoned);
        }

        #region Constructors

        public Result(String tumblerAddress)
        {
            Trace.Assert(tumblerAddress != null);
            this.tumblerAddress = tumblerAddress;
        }

        // Alternate constructor from Json Dictionary
        public Result(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("tumbler_address", out outValue))
                this.tumblerAddress = outValue as String;
        }

        #endregion

        #region Public Methods

        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("tumbler_address", this.tumblerAddress);
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
