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
            String contentUri,
            CommercioDocMetadata metadata,
            List<String> recipients,
            List<StdCoin> fees,
            Wallet wallet,
            CommercioDocChecksum checksum,
            KeyParameter aesKey,
            List<EncryptedData> encryptedData = null,
            CommercioDoSign doSign = null
        )
        {
            if (encryptedData == null)
            {
                encryptedData = new List<EncryptedData>();
            }
            // Get a default aes key for encryption if needed
            if (aesKey == null) 
            {
                aesKey = KeysHelper.generateAesKey();
            }

            // Build a generic document
            CommercioDoc document = new CommercioDoc(
                senderDid: wallet.bech32Address,
                recipientDids: recipients,
                uuid: id,
                contentUri: contentUri,
                metadata: metadata,
                checksum: checksum,
                encryptionData: null,
                doSign: doSign
            );

            // Encrypt its contents, if necessary
            CommercioDoc finalDoc = document;
            if (encryptedData.Count > 0)
            {
                finalDoc = await DocsUtils.encryptField(
                    document,
                    aesKey,
                    encryptedData,
                    recipients,
                    wallet
                );
            }

            // Build the tx message
            MsgShareDocument msg = new MsgShareDocument(document: finalDoc);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, new StdFee(gas: "200000", amount: fees));
        }

        /// Returns the list of all the [CommercioDoc] that the
        /// specified [address] has sent.
        public static async Task<List<CommercioDoc>> getSendDocuments(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/docs/{address}/sent";
            List <Dictionary<String, Object >> response = await Network.queryChain(url) as List<Dictionary<String, Object>>;
            return response.Select((json) => new CommercioDoc(json)).ToList();
        }

        /// Returns the list of all the [CommercioDoc] that the
        /// specified [address] has received.
        public static async Task<List<CommercioDoc>> getReceivedDocuments(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/docs/{address}/received";
            List<Dictionary<String, Object>> response = await Network.queryChain(url) as List<Dictionary<String, Object>>;
            return response.Select((json) => new CommercioDoc(json)).ToList();
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
            StdFee fee  = null
        )
        {
            MsgSendDocumentReceipt msg = new MsgSendDocumentReceipt(
                new CommercioDocReceipt(
                    uuid: Guid.NewGuid().ToString(), // *** This should be equivalent to Uuid().v4() in Dart
                    recipientDid: recipient,
                    txHash: txHash,
                    documentUuid: documentId,
                    proof: proof,
                    senderDid: wallet.bech32Address
                )
            );
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee : fee);
        }

        /// Returns the list of all the [CommercioDocReceipt] that
        /// have been sent from the given [address].
        public static async Task<List<CommercioDocReceipt>> getSentReceipts(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/receipts/{address}/sent";
            List<Dictionary<String, Object>> response = await Network.queryChain(url) as List<Dictionary<String, Object>>;
            return response.Select((json) => new CommercioDocReceipt(json)).ToList();
        }

        /// Returns the list of all the [CommercioDocReceipt] that
        /// have been received from the given [address].
        public static async Task<List<CommercioDocReceipt>> getReceivedReceipts(String address, Wallet wallet)
        {
            String url = $"{wallet.networkInfo.lcdUrl}/receipts/{address}/received";
            List<Dictionary<String, Object>> response = await Network.queryChain(url) as List<Dictionary<String, Object>>;
            return response.Select((json) => new CommercioDocReceipt(json)).ToList();
        }

        #endregion

        #region Helpers
        #endregion
    }
}
