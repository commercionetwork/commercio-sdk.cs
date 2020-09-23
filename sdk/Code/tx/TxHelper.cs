// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
//
/// Allows to easily perform common transaction operations.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public enum BroadcastingMode
    {
        [EnumMember(Value = "async")]
        ASYNC,
        [EnumMember(Value = "block")]
        BLOCK,
        [EnumMember(Value = "sync")]
        SYNC,
    }

    public class TxHelper
    {

        private static readonly int defaultGas = 200000;
        private static readonly String defaultDenom = "ucommercio";
        private static readonly int defaultAmount = 10000;

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// Creates a transaction having the given [msgs] and [fee] inside,
        /// signs it with the given [Wallet] and sends it to the blockchain.
        /// Optional parameters can be [fee] and broadcasting [mode],
        /// that can be of type "sync", "async" or "block".
        public static async Task<TransactionResult> createSignAndSendTx( List<StdMsg> msgs, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            if (fee == null)
            {
                // Set the default value for fee
                int msgsNumber = msgs.Count > 0 ? msgs.Count : 1;
                fee = GenericUtils.calculateDefaultFee(msgsNumber: msgsNumber, fee: defaultAmount, denom: defaultDenom, gas: defaultGas);
            }

            StdTx stdTx = TxBuilder.buildStdTx(stdMsgs: msgs, fee: fee);
            StdTx signedTx = await TxSigner.signStdTx(wallet: wallet, stdTx: stdTx);
            String modeStr = MyEnumExtensions.ToEnumMemberAttrValue(mode);
            return await TxSender.broadcastStdTx(wallet: wallet, stdTx: signedTx, mode: modeStr);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
