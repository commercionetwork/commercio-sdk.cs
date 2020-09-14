// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a BuyMembership and perform common related operations
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class BuyMembershipHelper
    {
        /// Creates a BuyMembership from the given [wallet] and [membershipType].
        public static BuyMembership fromWallet(Wallet wallet, MembershipType membershipType)
        {
            return new BuyMembership(buyerDid: wallet.bech32Address, membershipType: MyEnumExtensions.ToDescriptionString(membershipType));
        }
    }
}
