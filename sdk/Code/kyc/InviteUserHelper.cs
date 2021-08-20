// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create an InviteUser and perform common related operations
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
    public class InviteUserHelper
    {
        /// Creates an InviteUser from the given [wallet] and [recipientDid].
        public static InviteUser fromWallet(Wallet wallet, String recipientDid)
        {
            return new InviteUser(recipientDid: recipientDid, senderDid: wallet.bech32Address);
        }
    }
}
