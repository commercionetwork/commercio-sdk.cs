// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Contains the data that is returned from the [generateProof] method.
//
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;

namespace commercio.sdk
{
    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class ProofGenerationResult
    {
        #region Properties
        public String encryptedProof { get; set; }
        public String encryptedAesKey { get; set; }

        #endregion

        #region Constructors
        ProofGenerationResult(String encryptedProof, String encryptedAesKey)
        {
            Trace.Assert(encryptedProof != null);
            Trace.Assert(encryptedAesKey != null);
            this.encryptedProof = encryptedProof;
            this.encryptedAesKey = encryptedAesKey;
        }

        #endregion

        #region Public Methods

        /// Given a [payload], creates a new AES-256 key and uses that to encrypt
        /// the payload itself.
        /// This is now a static method of the class - we cannot have function outside class in C#, while Dart can...
        public static async Task<ProofGenerationResult> generateProof(Object payload, String lcdUrl)
        {
            // Generate the AES key - no await here...
            KeyParameter aesKey = KeysHelper.generateAesKey();

            // Encrypt the payload
            String encryptionData = JsonConvert.SerializeObject(payload);
            byte[] encryptedPayload = EncryptionHelper.encryptStringWithAes(encryptionData, aesKey);

            // Generate nonce, concatenate with payload and encode(?)
            //*** 20200524 - I am not sure of encoding - I am already working with arrays...No encode until extensive testing
            byte[] nonce = KeysHelper.generateRandomNonce(12);
            String encodedProof = Convert.ToBase64String(encryptedPayload.Concat(nonce).ToArray());

            // Encrypt the AES key
            RSAPublicKey rsaKey = await EncryptionHelper.getGovernmentRsaPubKey(lcdUrl);
            byte[] encryptedAesKey = EncryptionHelper.encryptBytesWithRsa(aesKey.GetKey(), rsaKey);
            String encodedAesKey = Convert.ToBase64String(encryptedAesKey);


            return new ProofGenerationResult(encryptedProof: encodedProof, encryptedAesKey: encodedAesKey);
        }

        #endregion

        #region Helpers
        #endregion
    }

}
