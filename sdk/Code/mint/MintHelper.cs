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
using System.Linq;
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
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> openCdp(int amount, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin("ucommercio", amount.ToString()) };

            OpenCdp openCdp = OpenCdpHelper.fromWallet(wallet, depositAmount);

            MsgOpenCdp msg = new MsgOpenCdp(openCdp: openCdp);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee : fee, mode: mode);
        }

        /// Performs a transaction opening a new CDP [openCdp] as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> openCdpSingle(OpenCdp openCdp, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            MsgOpenCdp msg = new MsgOpenCdp(openCdp: openCdp);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }
        
        /// Closes the CDP having the given [timestamp].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> closeCdp(int timestamp, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            CloseCdp closeCdp = CloseCdpHelper.fromWallet(wallet, timestamp);

            MsgCloseCdp msg = new MsgCloseCdp(closeCdp: closeCdp);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Closes the open CDPs having the given [closeCdps] list as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> closeCdpsList(List<CloseCdp> closeCdps, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<MsgCloseCdp> msgs = closeCdps
                .Select(x => new MsgCloseCdp(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
