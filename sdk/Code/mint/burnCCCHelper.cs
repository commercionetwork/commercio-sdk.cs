// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a burnCCC and perform common related operations
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
    public class burnCCCHelper
    {
        /// Creates a burnCCC from the given [wallet] and [timeStamp].
        /// N.B.: [timeStamp] is the 'height' at which the position was opened
        public static burnCCC fromWallet(Wallet wallet, int timeStamp)
        {
            return new burnCCC(signerDid: wallet.bech32Address, timeStamp: timeStamp.ToString());
        }
    }
}
