// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class DocsHelper
    {
        #region Instance Variables

        // This doesn't seem to be used, although it is there in Dart code... I will keep it as a placeholder
        // Use static HttpClient to avoid exhausting system resources for network connections.
        // private static HttpClient client = new HttpClient();

        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Creates a new transaction that allows to share the document associated
        /// with the given [contentUri] and having the given [metadata]
        /// and [checksum]. If [encryptedData] is specified, encrypts the proper
        /// data for the specified [recipients] and then sends the transaction
        /// to the blockchain.
        public static async Task<TransactionResult> shareDocument(
            String id,
            CommercioDocMetadata metadata,
            List<String> recipients,
            Wallet wallet,
            CommercioDoSign doSign = null,
            CommercioDocChecksum checksum = null,
            KeyParameter aesKey = null,
            List<EncryptedData> encryptedData = null,
            StdFee fee = null,
            String contentUri = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {
            // Build a generic document
            CommercioDoc commercioDoc = await CommercioDocHelper.fromWallet(
                wallet: wallet,
                recipients: recipients,
                id: id,
                metadata: metadata,
                checksum: checksum,
                contentUri: contentUri,
                doSign: doSign,
                encryptedData: encryptedData,
                aesKey: aesKey
            );


            // Build the tx message
            MsgShareDocument msg = new MsgShareDocument(document: commercioDoc);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Create a new transaction that allows to share
        /// a list of previously generated documents [commercioDocsList].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> shareDocumentsList(
            List<CommercioDoc> commercioDocsList,
            Wallet wallet,
            StdFee fee = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {
            List<MsgShareDocument> msgs = commercioDocsList
                .Select(x => new MsgShareDocument(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        /// Returns the list of all the [CommercioDoc] that the
        /// specified [address] has sent.
        public static async Task<List<CommercioDoc>> getSendDocuments(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/docs/{address}/sent";
            JArray response = await Network.queryChain(url) as JArray;
            return response.Select((json) => new CommercioDoc((JObject) json)).ToList();
        }

        /// Returns the list of all the [CommercioDoc] that the
        /// specified [address] has received.
        public static async Task<List<CommercioDoc>> getReceivedDocuments(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/docs/{address}/received";
            JArray response = await Network.queryChain(url) as JArray;
            return response.Select((json) => new CommercioDoc((JObject)json)).ToList();
        }

        /// Creates a new transaction which tells the [recipient] that the document
        /// having the specified [documentId] and present inside the transaction with
        /// hash [txHash] has been properly seen.
        /// [proof] optional proof of reading.
        public static async Task<TransactionResult> sendDocumentReceipt(
            String recipient,
            String txHash,
            String documentId,
            Wallet wallet,
            String proof = "",
            StdFee fee  = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {
            CommercioDocReceipt commercioDocReceipt = CommercioDocReceiptHelper.fromWallet(
                wallet: wallet,
                recipient: recipient,
                txHash: txHash,
                documentId: documentId,
                proof: proof
            );

            MsgSendDocumentReceipt msg = new MsgSendDocumentReceipt(receipt: commercioDocReceipt);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee : fee, mode: mode);
        }

        /// Creates a new transaction which sends
        /// a list of previously generated receipts [commercioDocReceiptsList].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> sendDocumentReceiptsList(
            List<CommercioDocReceipt> sendDocumentReceiptsList,
            Wallet wallet,
            StdFee fee = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {
            List<MsgSendDocumentReceipt> msgs = sendDocumentReceiptsList
                .Select(x => new MsgSendDocumentReceipt(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        /// Returns the list of all the [CommercioDocReceipt] that
        /// have been sent from the given [address].
        public static async Task<List<CommercioDocReceipt>> getSentReceipts(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/receipts/{address}/sent";
            JArray response = await Network.queryChain(url) as JArray;
            return response.Select((json) => new CommercioDocReceipt((JObject)json)).ToList();
        }

        /// Returns the list of all the [CommercioDocReceipt] that
        /// have been received from the given [address].
        public static async Task<List<CommercioDocReceipt>> getReceivedReceipts(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/receipts/{address}/received";
            JArray response = await Network.queryChain(url) as JArray;
            return response.Select((json) => new CommercioDocReceipt((JObject)json)).ToList();
        }

        #endregion

        #region Helpers
        #endregion
    }
}
