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
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using commercio.sacco.lib;

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
            // return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000).ToString(); // RC 20200913: THis was a old version @@@!
            return (DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
        }

        /// Calculates the default fees from
        /// the messages number [msgsNumber] contained in the transaction
        /// and the default values [fee], [denom] and [gas].
        public static StdFee calculateDefaultFee(int msgsNumber, int fee, String denom, int gas)
        {
            return new StdFee(
                gas: (gas * msgsNumber).ToString(), 
                amount: new List<StdCoin> { new StdCoin(denom: denom, amount: (fee * msgsNumber).ToString())}
            );    
        }

    #endregion

    #region Helpers
    #endregion
}

public static class MyEnumExtensions
    {
        public static string ToEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Value;
        }
    }
}
