// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform CommercioMINT related transactions.
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class MintHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Opens a new CDP depositing the given Commercio Token [amount].
        public static async Task<TransactionResult> openCdp(int amount, Wallet wallet)
        {
            MsgOpenCdp msg = new MsgOpenCdp(
                depositAmount: new List<StdCoin> { new StdCoin("ucommercio", (amount * 1000000).ToString()) },
                signerDid: wallet.bech32Address
            );
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet);
        }

        /// Closes the CDP having the given [timestamp].
        public static async Task<TransactionResult> closeCdp(int timestamp, Wallet wallet)
        {
            MsgCloseCdp msg = new MsgCloseCdp(
                timeStamp: timestamp,
                signerDid: wallet.bech32Address
            );
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
