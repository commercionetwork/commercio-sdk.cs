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
        public static Task<TransactionResult> setDidDocument(DidDocument didDocument, Wallet wallet, StdFee fee = null)
        {
            MsgSetDidDocument msg = new MsgSetDidDocument(didDocument: didDocument);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet, fee: fee);
        }

        /// Creates a new Did power up request for the given [pairwiseDid] and of the given [amount].
        /// Signs everything that needs to be signed (i.e. the signature JSON inside the payload) with the
        /// private key contained inside the given  [senderWallet] and the [privateKey].
        public static async Task<TransactionResult> requestDidPowerUp(Wallet senderWallet, String pairwiseDid, List<StdCoin> amount, RSAPrivateKey privateKey, StdFee fee = null)
        {
            // Get the timestamp
            String timestamp = GenericUtils.getTimeStamp();
            String senderDid = senderWallet.bech32Address;

            // Build and sign the signature
            byte[] signedSignatureHash = SignHelper.signPowerUpSignature(
                senderDid: senderDid,
                pairwiseDid: pairwiseDid,
                timestamp: timestamp,
                rsaPrivateKey: privateKey);

            //// Build the signature
            //DidPowerUpRequestSignatureJson signatureJson = new DidPowerUpRequestSignatureJson(
            //    pairwiseDid: pairwiseDid,
            //    timeStamp: timestamp 
            //);

            //byte[] signedJson = SignHelper.signSorted(signatureJson.toJson(), wallet);

            // Build the payload -*
            DidPowerUpRequestPayload payload = new DidPowerUpRequestPayload(
                senderDid: senderDid,
                pairwiseDid: pairwiseDid,
                timeStamp: timestamp,
                signature: Convert.ToBase64String(signedSignatureHash)
            );

            // =============
            // Encrypt proof
            // =============

            // Generate an AES-256 key
            KeyParameter aesKey =  KeysHelper.generateAesKey(); // await ?

            // Encrypt the payload
            byte[] encryptedProof = EncryptionHelper.encryptStringWithAesGCM(JsonConvert.SerializeObject(payload), aesKey);

            // =================
            // Encrypt proof key
            // =================

            // Encrypt the key using the Tumbler public RSA key
            RSAPublicKey rsaPubTkKey = await EncryptionHelper.getGovernmentRsaPubKey(senderWallet.networkInfo.lcdUrl);
            byte[] encryptedProofKey = EncryptionHelper.encryptBytesWithRsa(aesKey.GetKey(), rsaPubTkKey);
            
            // Build the message and send the tx
            MsgRequestDidPowerUp msg = new MsgRequestDidPowerUp(
                claimantDid: senderDid,
                amount: amount,
                powerUpProof: Convert.ToBase64String(encryptedProof),
                uuid: Guid.NewGuid().ToString(),
                encryptionKey: Convert.ToBase64String(encryptedProofKey)
            );

            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, senderWallet, fee: fee);
     }

    #endregion

    #region Helpers
    #endregion

    }
}
