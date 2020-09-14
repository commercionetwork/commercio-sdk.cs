// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to perform common operations related to CommercioID.
//
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class IdHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// Returns the Did Document associated with the given [did],
        /// or `null` if no Did Document was found.
        public static async Task<DidDocument> getDidDocument(String did, Wallet wallet) 
        {
            Object outValue;
            DidDocument DidDoc;

            String url = $"{wallet.networkInfo.lcdUrl}/identities/{did}";
            // This needs to be checked...
            // List<Dictionary<String, Object>> response = (List<Dictionary<String, Object>>)(await Network.queryChain(url));
            Dictionary<String, Object> response = (Dictionary<String, Object>)(await Network.queryChain(url));
            if (response == null)
            {
                return null;
            }
            bool DidFound = response.TryGetValue("did_document", out outValue);
            if (DidFound)
                DidDoc = new DidDocument(outValue as Dictionary<String, Object>);
            else
                DidDoc = null;
            return DidDoc;
        }

        /// Performs a transaction setting the specified [didDocument] as being
        /// associated with the address present inside the specified [wallet].
        public static Task<TransactionResult> setDidDocument(DidDocument didDocument, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            MsgSetDidDocument msg = new MsgSetDidDocument(didDocument: didDocument);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee, mode: mode);
        }

        /// Performs a transaction setting the [didDocuments] list as being
        /// associated with the address present inside the specified [wallet].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static Task<TransactionResult> setDidDocumentsList(List<DidDocument> didDocuments, Wallet wallet, StdFee fee = null, BroadcastingMode mode = BroadcastingMode.SYNC)
        {
            List <MsgSetDidDocument> msgs = didDocuments
                .Select (x => new MsgSetDidDocument(x))
                .ToList();
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            // And I need to declare the thing explicitly in C#!
            return TxHelper.createSignAndSendTx( msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        /// Creates a new Did power up request for the given [pairwiseDid] and of the given [amount].
        /// Signs everything that needs to be signed (i.e. the signature JSON inside the payload) with the
        /// private key contained inside the given  [senderWallet] and the [privateKey].
        public static async Task<TransactionResult> requestDidPowerUp(
            Wallet senderWallet, 
            String pairwiseDid, 
            List<StdCoin> amount, 
            RSAPrivateKey privateKey, 
            StdFee fee = null, 
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {

            RequestDidPowerUp requestDidPowerUp = await RequestDidPowerUpHelper.fromWallet(
                senderWallet,
                pairwiseDid,
                amount,
                privateKey
            );

            MsgRequestDidPowerUp msg = new MsgRequestDidPowerUp(requestDidPowerUp: requestDidPowerUp);

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, senderWallet, fee: fee, mode: mode);
        }

        /// Sends a new transaction from the sender [wallet]
        /// to request a list of Did PowerUp [requestDidPowerUpsList].
        /// Optionally [fee] and broadcasting [mode] parameters can be specified.
        public static async Task<TransactionResult> requestDidPowerUpList(
            List<RequestDidPowerUp> requestDidPowerUpsList,
            Wallet wallet,
            StdFee fee = null,
            BroadcastingMode mode = BroadcastingMode.SYNC
        )
        {

            List<MsgRequestDidPowerUp> msgs = requestDidPowerUpsList
                .Select(x => new MsgRequestDidPowerUp(x))
                .ToList();

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(msgs.ToList<StdMsg>(), wallet, fee: fee, mode: mode);
        }

        #endregion

        #region Helpers
        #endregion

    }
}
