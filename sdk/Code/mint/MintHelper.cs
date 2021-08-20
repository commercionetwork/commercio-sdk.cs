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
using commercio.sdk.Code.entities.mint;
using Newtonsoft.Json.Linq;

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
        public static async Task<TransactionResult> mintCCC(int amount, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin("ucommercio", amount.ToString()) };

            mintCCC mintCCC = mintCCCHelper.fromWallet(wallet, depositAmount);

            MsgmintCCC msg = new MsgmintCCC(mintCCC: mintCCC);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee : fee, mode: mode);
        }

        /// Performs a transaction opening a new CDP [mintCCC] as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> mintCCCSingle(mintCCC mintCCC, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            MsgmintCCC msg = new MsgmintCCC(mintCCC: mintCCC);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        ///Luigi Arena 29/07/2021
        /// Performs a transaction opening a list of new CDP [mintCCC] as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> mintCCCList(
            List<mintCCC> mintCCCsList,
            Wallet wallet,
            StdFee fee = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {
            List<MsgmintCCC> msgs = mintCCCsList
                .Select(x => new MsgmintCCC(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        /// Returns the list of all the [ExchangeTradePosition] that the
        /// specified wallet has minted.
        public static async Task<List<ExchangeTradePosition>> getExchangeTradePositions(Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/commerciomint/etps/{wallet.bech32Address}";
            JArray response = await Network.queryChain(url) as JArray;
            return response.Select((json) => new ExchangeTradePosition((JObject)json)).ToList();
           
        }

        /// Closes the CDP having the given [timestamp].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> burnCCC(int timestamp, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            burnCCC burnCCC = burnCCCHelper.fromWallet(wallet, timestamp);

            MsgburnCCC msg = new MsgburnCCC(burnCCC: burnCCC);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Closes the open CDPs having the given [burnCCCs] list as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> burnCCCsList(List<burnCCC> burnCCCs, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List<MsgburnCCC> msgs = burnCCCs
                .Select(x => new MsgburnCCC(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
