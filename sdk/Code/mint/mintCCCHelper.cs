// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a mintCCC and perform common related operations
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
    public class mintCCCHelper
    {
        /// Creates an mintCCC from the given [wallet] and deposit [amount].
        public static mintCCC fromWallet(Wallet wallet, List<StdCoin> amount)
        {
            return new mintCCC(depositAmount: amount, signerDid: wallet.bech32Address);
        }
    }
}
