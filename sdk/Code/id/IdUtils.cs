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
        public byte[] encryptedProof { get; set; }
        public byte[] encryptedAesKey { get; set; }

        #endregion

        #region Constructors
        ProofGenerationResult(byte[] encryptedProof, byte[] encryptedAesKey)
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
        public static async Task<ProofGenerationResult> generateProof(Object payload)
        {
            // Generate the AES key - no await here...
            KeyParameter aesKey = KeysHelper.generateAesKey();

            // Encrypt the payload
            String encryptionData = JsonConvert.SerializeObject(payload);
            byte[] encryptedPayload = EncryptionHelper.encryptStringWithAes(encryptionData, aesKey);

            // Encrypt the AES key
            RSAPublicKey rsaKey = await EncryptionHelper.getGovernmentRsaPubKey();
            byte[] encryptedAesKey = EncryptionHelper.encryptBytesWithRsa(aesKey.GetKey(), rsaKey);

            return new ProofGenerationResult(encryptedProof: encryptedPayload, encryptedAesKey: encryptedAesKey);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // Here we have to address the Dart direct methods - to be encapsulated in a class in C#
    //***
    /*
     /// Contains the data that is returned from the [generateProof] method.
    class ProofGenerationResult extends Equatable {
      final Uint8List encryptedProof;
      final Uint8List encryptedAesKey;

      ProofGenerationResult({
        @required this.encryptedProof,
        @required this.encryptedAesKey,
      })  : assert(encryptedProof != null),
            assert(encryptedAesKey != null);

      @override
      List<Object> get props {
        return [encryptedProof, encryptedAesKey];
      }
    }

    /// Given a [payload], creates a new AES-256 key and uses that to encrypt
    /// the payload itself.
    Future<ProofGenerationResult> generateProof(dynamic payload) async {
      // Generate the AES key
      final aesKey = await KeysHelper.generateAesKey();

      // Encrypt the payload
      final encryptionData = jsonEncode(payload);
      final encryptedPayload = EncryptionHelper.encryptStringWithAes(
        encryptionData,
        aesKey,
      );

      // Encrypt the AES key
      final rsaKey = await EncryptionHelper.getGovernmentRsaPubKey();
      final encryptedAesKey = EncryptionHelper.encryptBytesWithRsa(
        aesKey.bytes,
        rsaKey,
      );

      return ProofGenerationResult(
        encryptedProof: encryptedPayload,
        encryptedAesKey: encryptedAesKey,
      );
    }

     */
}
