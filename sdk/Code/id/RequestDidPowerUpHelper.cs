// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a Did Powerup Request and perform common related operations
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    /// Allows to easily create a RequestDidPowerUp
    /// and perform common related operations
    public class RequestDidPowerUpHelper
    {
        public static async Task<RequestDidPowerUp> fromWallet(Wallet senderWallet, String pairwiseDid, List<StdCoin> amount, RSAPrivateKey privateKey)
        {
            // Get the timestamp
            String timestamp = GenericUtils.getTimeStamp(); // RC 20200913: Be careful, the Dart version uses a different timestamp format non ISO-8601 - Why?
            String senderDid = senderWallet.bech32Address;

            // Build and sign the signature
            byte[] signedSignatureHash = SignHelper.signPowerUpSignature(
                senderDid: senderDid,
                pairwiseDid: pairwiseDid,
                timestamp: timestamp,
                rsaPrivateKey: privateKey);

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
            KeyParameter aesKey = KeysHelper.generateAesKey(); // await ?

            // Encrypt the payload
            byte[] encryptedProof = EncryptionHelper.encryptStringWithAesGCM(JsonConvert.SerializeObject(payload), aesKey);

            // =================
            // Encrypt proof key
            // =================

            // Encrypt the key using the Tumbler public RSA key
            RSAPublicKey rsaPubTkKey = await EncryptionHelper.getGovernmentRsaPubKey(senderWallet.networkInfo.lcdUrl);
            byte[] encryptedProofKey = EncryptionHelper.encryptBytesWithRsa(aesKey.GetKey(), rsaPubTkKey);

            // Build the message 
            RequestDidPowerUp request = new RequestDidPowerUp(
                claimantDid: senderDid,
                amount: amount,
                powerUpProof: Convert.ToBase64String(encryptedProof),
                uuid: Guid.NewGuid().ToString(),
                encryptionKey: Convert.ToBase64String(encryptedProofKey)
            );

            return (request);

        }
    }
}
