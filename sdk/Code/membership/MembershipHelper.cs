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
using System.Linq;
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
        public static async Task<TransactionResult> inviteUser(String userDid, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            InviteUser inviteUser = InviteUserHelper.fromWallet(wallet, userDid);

            MsgInviteUser msg = new MsgInviteUser(inviteUser: inviteUser);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Sends a new transaction in order to invite the given [inviteUsers] users list.
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> inviteUsersList(List<InviteUser> inviteUsers, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<MsgInviteUser> msgs = inviteUsers
                .Select(x => new MsgInviteUser(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        /// Buys the membership with the given [membershipType].
        public static async Task<TransactionResult> buyMembership(MembershipType membershipType, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            BuyMembership buyMembership = BuyMembershipHelper.fromWallet(wallet, membershipType);

            MsgBuyMembership msg = new MsgBuyMembership(buyMembership: buyMembership);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Buys the membership with the given [buyMemberships] memberships list.
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> buyMembershipsList(List<BuyMembership> buyMemberships, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<MsgBuyMembership> msgs = buyMemberships
                .Select(x => new MsgBuyMembership(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }



        #endregion

        #region Helpers
        #endregion

    }
}
