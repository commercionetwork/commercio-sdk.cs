// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform CommercioMEMBERSHIP related operations.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MembershipHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// Sends a new transaction in order to invite the given [userDid].
        public static async Task<TransactionResult> inviteUser(String userDid, Wallet wallet, StdFee fee = null)
        {
            MsgInviteUser msg = new MsgInviteUser(recipientDid: userDid, senderDid: wallet.bech32Address);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee);
        }

        /// Buys the membership with the given [membershipType].
        public static async Task<TransactionResult> buyMembership(MembershipType membershipType, Wallet wallet, StdFee fee = null)
        {
            MsgBuyMembership msg = new MsgBuyMembership(membershipType: membershipType, buyerDid: wallet.bech32Address);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
