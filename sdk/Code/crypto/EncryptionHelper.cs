// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to perform common encryption operations such as
/// RSA/AES encryption and decryption.
//
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace commercio.sdk
{
    public class EncryptionHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Returns the RSA public key associated to the government that should be used when
        /// encrypting the data that only it should see.
        public static async Task<RSAPublicKey> getGovernmentRsaPubKey(String lcdUrl)
        // public static RSAPublicKey getGovernmentRsaPubKey()
        {
            Object tumblerResponse = await Network.query($"{lcdUrl}/government/tumbler");
            if (tumblerResponse == null)
            {
                System.ArgumentException argEx = new System.ArgumentException("getGovernmentRsaPubKey: Cannot get tumbler address");
                throw argEx;
            }

            TumblerResponse tumbler = new TumblerResponse(JObject.Parse(tumblerResponse.ToString()));
            String tumblerAddress = tumbler.result.tumblerAddress;

            Object identityResponseRaw = await Network.query($"{lcdUrl}/identities/{tumblerAddress}");
            if (identityResponseRaw == null)
            {
                System.ArgumentException argEx = new System.ArgumentException("getGovernmentRsaPubKey: Cannot get government RSA public key");
                throw argEx;
            }

            IdentityResponse identityResponse = new IdentityResponse(JObject.Parse(identityResponseRaw.ToString()));
            String publicSignatureKeyPem = identityResponse.result.didDocument.publicKeys[1].publicKeyPem;

            RsaKeyParameters rsaPublicKey = RSAKeyParser.parsePublicKeyFromPem(publicSignatureKeyPem);

            return new RSAPublicKey(rsaPublicKey);
        }

        /// Encrypts the given [data] with AES using the specified [key].
        public static byte[] encryptStringWithAes(String data, KeyParameter key)
        {
            AEScoder coder = new AEScoder(key);
            return coder.encode(System.Text.Encoding.UTF8.GetBytes(data));
        }

        //*** 20200524 - This one needs to be checked for consistency with Dart code!
        public static byte[] encryptStringWithAesGCM(String data, KeyParameter key)
        {
            // Generate a random 96-bit nonce N
            byte[] nonce = KeysHelper.generateRandomNonceUtf8(12);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            //*** 20200524 - Is 96 the right number?
            AeadParameters parameters = new AeadParameters(key, 96, nonce);
            cipher.Init(true, parameters);
            byte[] utf8Data = Encoding.UTF8.GetBytes(data);
            int bufSize = utf8Data.Length;
            byte[] cipherText = new byte[cipher.GetOutputSize(bufSize)];
            int len = cipher.ProcessBytes(utf8Data, 0, bufSize, cipherText, 0);
            cipher.DoFinal(cipherText, len);
            using (MemoryStream combinedStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(combinedStream))
                {
                    binaryWriter.Write(nonce);
                    binaryWriter.Write(cipherText);
                }
                return combinedStream.ToArray();
            }
        }


        /// Encrypts the given [data] with AES using the specified [key].
        public static byte[] encryptBytesWithAes(byte[] data, KeyParameter key)
        {
            AEScoder coder = new AEScoder(key);
            return coder.encode(data);
        }

        /// Decrypts the given [data] with AES using the specified [key].
        public static byte[] decryptWithAes(byte[] data, KeyParameter key)
        {
            AEScoder coder = new AEScoder(key);
            return coder.decode(data);
        }

        /// Encrypts the given [data] with RSA using the specified [key].
        public static byte[] encryptStringWithRsa(String data, PublicKey key)
        {
            RSAcoder coder = new RSAcoder((AsymmetricKeyParameter) ((RSAPublicKey)key).pubKey);
            return coder.Encrypt(System.Text.Encoding.UTF8.GetBytes(data));
        }

        /// Encrypts the given [data] with RSA using the specified [key].
        public static byte[] encryptBytesWithRsa(byte[] data, PublicKey key)
        {
            RSAcoder coder = new RSAcoder((AsymmetricKeyParameter)((RSAPublicKey)key).pubKey);
            return coder.Encrypt(data);
        }

        /// Decrypts the given data using the specified private [key].
        public static byte[] decryptBytesWithRsa(byte[] data, PrivateKey key)
        {
            RSAcoder coder = new RSAcoder((AsymmetricKeyParameter)((RSAPrivateKey)key).secretKey);
            return coder.Decrypt(data);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
