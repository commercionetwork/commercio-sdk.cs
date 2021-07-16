// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace commercio.sdk
{
    public enum MembershipType
    {
        [EnumMember(Value = "bronze")]
        BRONZE,
        [EnumMember(Value = "silver")]
        SILVER,
        [EnumMember(Value = "gold")]
        GOLD,
        [EnumMember(Value = "black")]
        BLACK
    }

}
