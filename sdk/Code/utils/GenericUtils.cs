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
using System.ComponentModel;
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
            // return System.DateTime.UtcNow.ToString("o"); // This get a Iso8601 Time stamp - to be checked
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000).ToString();
        }
        #endregion

        #region Helpers
        #endregion
    }

    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this Object val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
