﻿// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform network-related operations.
//

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace commercio.sdk
{
    public class Network
    {
        #region Properties

        // Use static HttpClient to avoid exhausting system resources for network connections.
        public static HttpClient client = new HttpClient();

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Queries the given [url] and returns an object of type [T],
        /// or `null` if some error raised.
        public static async Task<Object> queryChain(String url)
        {
            // Object outValue = null;
            try
            {
                // Get the response
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    System.ArgumentException argEx = new System.ArgumentException($"Expected status code OK (200) but got {response.StatusCode} - {response.ReasonPhrase} - {response.Content}");
                    throw argEx;
                }
                // Convert the response
                String encodedJson = await response.Content.ReadAsStringAsync();
                // Dictionary<String, Object> json = JsonConvert.DeserializeObject<Dictionary<String, Object>>(encodedJson);
                // Return the result part of the response
                // json.TryGetValue("result", out Object outValue);
                JObject json = JObject.Parse(encodedJson);
                JArray jResult = (JArray) json["result"];
                // outValue = jResult;
                // outValue = jArray.ToObject<List<Dictionary<String, Object>>>();
                return jResult;
            }
            catch (Exception e)
            {
                //LOG IT!!!
                // Log the exception, display it, etc
                Debug.WriteLine($"Unhandled Exception in queryChain - {e.HResult} - {e.Message}");
                throw; //can rethrow the error to allow it to bubble up, or not, and ignore it.
                // return null;
            }
        }

        public static async Task<Object> query(String url)
        {
            try
            {
                // Get the response
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    System.ArgumentException argEx = new System.ArgumentException($"Expected status code OK (200) but got {response.StatusCode} - {response.ReasonPhrase} - {response.Content}");
                    throw argEx;
                }
                // Convert the response
                String responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                //LOG IT!!!
                // Log the exception, display it, etc
                Debug.WriteLine($"Unhandled Exception in query - {e.HResult} - {e.Message}");
                // throw; //can rethrow the error to allow it to bubble up, or not, and ignore it.
                return null;
            }
        }


        #endregion

        #region Helpers
        #endregion
    }
}
