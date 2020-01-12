// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform network-related operations.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;


namespace commercio.sdk
{
    public class Network
    {
        #region Properties

        // Use static HttpClient to avoid exhausting system resources for network connections.
        private static HttpClient client = new HttpClient();

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Queries the given [url] and returns an object of type [T],
        /// or `null` if some error raised.
        public static async Task<Object> queryChain(String url)
        {
            try
            {
                // Get the response
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    System.ArgumentException argEx = new System.ArgumentException($"Expected status code OK (200) but got ${response.StatusCode} - ${response.ReasonPhrase} - ${response.Content}");
                    throw argEx;
                }
                // Convert the response
                String encodedJson = await response.Content.ReadAsStringAsync();
                Dictionary<String, Object> json = JsonConvert.DeserializeObject<Dictionary<String, Object>>(encodedJson);
                // Return the result part of the response
                json.TryGetValue("result", out Object outValue);
                return outValue;
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
                    System.ArgumentException argEx = new System.ArgumentException($"Expected status code OK (200) but got ${response.StatusCode} - ${response.ReasonPhrase} - ${response.Content}");
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

        /*
        /// Allows to easily perform network-related operations.
        class Network {
          static var client = http.Client();

          /// Queries the given [url] and returns an object of type [T],
          /// or `null` if some error raised.
          static Future<dynamic> queryChain(String url) async {
            try {
              // Get the response
              final response = await client.get(url);
              if (response.statusCode != 200) {
                throw Exception(
                  "Expected status code 200 but got ${response.statusCode} - ${response.body}",
                );
              }

              // Return the result part of the response
              final json = jsonDecode(response.body) as Map<String, dynamic>;
              return json["result"];
            } catch (exception) {
              print(exception);
              return null;
            }
          }

          static Future<dynamic> query(String url) async {
            try {
              final response = await client.get(url);
              if (response.statusCode != 200) {
                throw Exception(
                  "Expected status code 200 but got ${response.statusCode} - ${response.body}",
                );
              }

              return response.body;
            } catch (exception) {
              return null;
            }
          }
        }

        */
    }
}
