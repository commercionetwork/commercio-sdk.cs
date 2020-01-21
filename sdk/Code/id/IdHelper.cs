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
            String url = $"{wallet.networkInfo.lcdUrl}/identities/{did}";
            // This needs to be checked...
            List<Dictionary<String, Object>> response = (List<Dictionary<String, Object>>)(await Network.queryChain(url));
            if (response == null)
            {
                return null;
            }
            return new DidDocument(response[0]);
        }

        /// Performs a transaction setting the specified [didDocument] as being
        /// associated with the address present inside the specified [wallet].
        public static Task<TransactionResult> setDidDocument(DidDocument didDocument, Wallet wallet)
        {
            MsgSetDidDocument msg = new MsgSetDidDocument(didDocument: didDocument);
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet);
        }

        /// Creates a new Did deposit request for the given [recipient] and of the given [amount].
        /// Signs everything that needs to be signed (i.e. the signature JSON inside the payload) with the
        /// private key contained inside the given [wallet].
        public static async Task<TransactionResult> requestDidDeposit(String recipient, List<StdCoin> amount, Wallet wallet)
        {
            // Get the timestamp
            String timestamp = GenericUtils.getTimeStamp();

            // Build the signature
            DidDepositRequestSignatureJson signatureJson = new DidDepositRequestSignatureJson(
                recipient: recipient,
                timeStamp: timestamp
            );

            byte[] signedJson = SignHelper.signSorted(signatureJson.toJson(), wallet);

            // Build the payload
            DidDepositRequestPayload payload = new DidDepositRequestPayload(
                recipient: recipient,
                timeStamp: timestamp,
                signature: HexEncDec.ByteArrayToString(signedJson)
            );

            // Build the proof
            ProofGenerationResult result = await ProofGenerationResult.generateProof(payload);

            // Build the message and send the tx
            MsgRequestDidDeposit msg = new MsgRequestDidDeposit(
                recipientDid: recipient,
                amount: amount,
                depositProof: HexEncDec.ByteArrayToString(result.encryptedProof),
                encryptionKey: HexEncDec.ByteArrayToString(result.encryptedAesKey),
                senderDid: wallet.bech32Address
            );
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet);
        }

        /// Creates a new Did power up request for the given [pairwiseDid] and of the given [amount].
        /// Signs everything that needs to be signed (i.e. the signature JSON inside the payload) with the
        /// private key contained inside the given [wallet].
        public static async Task<TransactionResult> requestDidPowerUp(String pairwiseDid, List<StdCoin> amount, Wallet wallet)
        {
            // Get the timestamp
            String timestamp = GenericUtils.getTimeStamp();

            // Build the signature
            DidPowerUpRequestSignatureJson signatureJson = new DidPowerUpRequestSignatureJson(
                pairwiseDid: pairwiseDid,
                timeStamp: timestamp 
            );

            byte[] signedJson = SignHelper.signSorted(signatureJson.toJson(), wallet);

            // Build the payload
            DidPowerUpRequestPayload payload = new DidPowerUpRequestPayload(
                pairwiseDid: pairwiseDid,
                timeStamp: timestamp,
                signature: HexEncDec.ByteArrayToString(signedJson)
            );

            // Build the proof
            ProofGenerationResult result = await ProofGenerationResult.generateProof(payload);

            // Build the message and send the tx
            MsgRequestDidPowerUp msg = new MsgRequestDidPowerUp(
                claimantDid: wallet.bech32Address,
                amount: amount,
                powerUpProof: HexEncDec.ByteArrayToString(result.encryptedProof),
                encryptionKey: HexEncDec.ByteArrayToString(result.encryptedAesKey)
            );
            // Careful here, Eugene: we are passing a list of BaseType containing the derived MsgSetDidDocument msg
            return await TxHelper.createSignAndSendTx(new List<StdMsg> { msg }, wallet);
     }

    #endregion

    #region Helpers
    #endregion

    }
}
