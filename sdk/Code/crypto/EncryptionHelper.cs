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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

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
        public static async Task<RSAPublicKey> getGovernmentRsaPubKey()
        // public static RSAPublicKey getGovernmentRsaPubKey()
        {
            Object response = await Network.query("http://localhost:8080/government/publicKey");
            if (response == null)
            {
                System.ArgumentException argEx = new System.ArgumentException("Cannot get government RSA public key");
                throw argEx;
            }
            RsaKeyParameters rsaPublicKey = RSAKeyParser.parsePublicKeyFromPem(response.ToString());
            return new RSAPublicKey(rsaPublicKey);
        }

        /// Encrypts the given [data] with AES using the specified [key].
        public static byte[] encryptStringWithAes(String data, KeyParameter key)
        {
            AEScoder coder = new AEScoder(key);
            return coder.encode(System.Text.Encoding.UTF8.GetBytes(data));
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

        /*
        class EncryptionHelper {
          /// Returns the RSA public key associated to the government that should be used when
          /// encrypting the data that only it should see.
          static Future<RSAPublicKey> getGovernmentRsaPubKey() async {
            final response =
                await Network.query("http://localhost:8080/government/publicKey");
            if (response == null) {
              throw FormatException("Cannot get government RSA public key");
            }
            final rsaPublicKey = RSAKeyParser.parsePublicKeyFromPem(response);
            return RSAPublicKey(rsaPublicKey);
          }

          /// Encrypts the given [data] with AES using the specified [key].
          static Uint8List encryptStringWithAes(String data, Key key) {
            return AES(key, mode: AESMode.ecb).encrypt(utf8.encode(data)).bytes;
          }

          /// Encrypts the given [data] with AES using the specified [key].
          static Uint8List encryptBytesWithAes(Uint8List data, Key key) {
            return AES(key, mode: AESMode.ecb).encrypt(data).bytes;
          }

          /// Decrypts the given [data] with AES using the specified [key].
          static Uint8List decryptWithAes(Uint8List data, Key key) {
            return AES(key, mode: AESMode.ecb).decrypt(Encrypted(data));
          }

          /// Encrypts the given [data] with RSA using the specified [key].
          static Uint8List encryptStringWithRsa(String data, RSAPublicKey key) {
            return RSA(publicKey: key.pubKey).encrypt(utf8.encode(data)).bytes;
          }

          /// Encrypts the given [data] with RSA using the specified [key].
          static Uint8List encryptBytesWithRsa(Uint8List data, RSAPublicKey key) {
            return RSA(publicKey: key.pubKey).encrypt(data).bytes;
          }

          /// Decrypts the given data using the specified private [key].
          static Uint8List decryptBytesWithRsa(Uint8List data, RSAPrivateKey key) {
            return RSA(privateKey: key.secretKey).decrypt(Encrypted(data));
          }
        }

        */
    }
}
