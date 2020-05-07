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
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class TxHelper
    {
        private static readonly String defaultGas = "200000";
        private static readonly String defaultDenom = "ucommercio";
        private static readonly String defaultAmount = "10000";

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// Creates a transaction having the given [msgs] and [fee] inside,
        /// signs it with the given [Wallet] and sends it to the blockchain.
        public static async Task<TransactionResult> createSignAndSendTx( List<StdMsg> msgs, Wallet wallet, StdFee fee = null)
        {
            if (fee == null)
            {
                // Set the default value for fee
                fee = new StdFee(gas: defaultGas, amount: new List<StdCoin> { new StdCoin(denom: defaultDenom, amount: defaultAmount) });
            }
            StdTx stdTx = TxBuilder.buildStdTx(stdMsgs: msgs, fee: fee);
            StdTx signedTx = await TxSigner.signStdTx(wallet: wallet, stdTx: stdTx);
            return await TxSender.broadcastStdTx(wallet: wallet, stdTx: signedTx);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
