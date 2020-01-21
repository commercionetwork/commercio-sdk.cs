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
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}
