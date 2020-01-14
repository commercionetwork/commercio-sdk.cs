// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
// In this class we collect all generic utils needed in SDK, accessed through static methods
//
using System;
using System.Text;
using System.Collections.Generic;

namespace commercio.sdk
{
    // We need to encapsulate here the utils that are simple functions in Dart
    public class GenericUtils
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        // Get time Stamp in Iso8601 format
        public static String getTimeStamp()
        {
            return System.DateTime.UtcNow.ToString("o"); // This get a Iso8601 Time stamp - to be checked
        }
        #endregion

        #region Helpers
        #endregion
    }
}
