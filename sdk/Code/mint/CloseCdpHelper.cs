// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a CloseCdp and perform common related operations
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
    public class CloseCdpHelper
    {
        /// Creates a CloseCdp from the given [wallet] and [timeStamp].
        /// N.B.: [timeStamp] is the 'height' at which the position was opened
        public static CloseCdp fromWallet(Wallet wallet, int timeStamp)
        {
            return new CloseCdp(signerDid: wallet.bech32Address, timeStamp: timeStamp.ToString());
        }
    }
}
