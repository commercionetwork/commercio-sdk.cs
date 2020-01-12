// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform signature-related operations.
//

using System;
using System.Collections.Generic;
using System.Text;
using commercio.sacco.lib;
using Newtonsoft.Json;


namespace commercio.sdk
{
    public class SignHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Takes the given [data], converts it to an alphabetically sorted
        /// JSON object and signs its content using the given [wallet].
        public static byte[] signSorted(Object data, Wallet wallet)
        {
            Dictionary<String, Object> sorted = null;
            if (data is Dictionary<String, Object>)
            {
                sorted = MapSorter.sort((Dictionary < String, Object >) data);
            }
            // Encode the sorted JSON to a string
            String jsonData = JsonConvert.SerializeObject(sorted);
            // Create a Sha256 of the message
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(jsonData);
            // Sign and return the message
            return wallet.sign(utf8Bytes);
        }

        #endregion

        #region Helpers
        #endregion

        /*
        /// Allows to easily perform signature-related operations.
        class SignHelper {
          /// Takes the given [data], converts it to an alphabetically sorted
          /// JSON object and signs its content using the given [wallet].
          static Uint8List signSorted(dynamic data, Wallet wallet) {
            var sorted = data;
            if (data is Map<String, dynamic>) {
              sorted = MapSorter.sort(data);
            }

            return wallet.sign(utf8.encode(json.encode(sorted)));
          }
        }

        */
    }
}
