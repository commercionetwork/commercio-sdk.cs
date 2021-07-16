// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a OpenCdp and perform common related operations
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
    public class OpenCdpHelper
    {
        /// Creates an OpenCdp from the given [wallet] and deposit [amount].
        public static OpenCdp fromWallet(Wallet wallet, List<StdCoin> amount)
        {
            return new OpenCdp(depositAmount: amount, signerDid: wallet.bech32Address);
        }
    }
}
