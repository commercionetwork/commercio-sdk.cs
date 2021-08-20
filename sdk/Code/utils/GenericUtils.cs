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
using System.Text.RegularExpressions;

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
            return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        // Get time Stamp in millisecond since Epoch (1.1.1970 - Unix Epoch)
        public static String getTimeStampEpoch()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000).ToString(); // RC 20200913: I don't know why two different time stamps are used...
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

        // Return [true] if the string [uuid] has a Uuid v4 format. Luigi Arena
        public static bool matchUuidv4(String uuid)
        {
            Regex regExp = new Regex(@"^[0-9A-F]{8}-[0-9A-F]{4}-4[0-9A-F]{3}-[89AB][0-9A-F]{3}-[0-9A-F]{12}$",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (regExp.IsMatch(uuid))
                return true;
            else
                return false;
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
