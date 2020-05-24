// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily generate new keys either to be used with AES or RSA key.
//
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Asn1.X9;

namespace commercio.sdk
{
    public class KeysHelper
    {
        #region Properties
        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        /// Generate a random nonce
        public static byte[] generateRandomNonce(int length, int bit = 256) 
        {
            DigestRandomGenerator wkRandomGenerator = new Org.BouncyCastle.Crypto.Prng.DigestRandomGenerator(new Sha512Digest());
            SecureRandom secureRandomGenerator = new SecureRandom(wkRandomGenerator);
            List<byte> wkNonce = new List<byte>();
            for (int i = 0; i < length; i++)
            {
                wkNonce.Add((byte) secureRandomGenerator.Next(0, bit));
            }
            byte[] nonce = wkNonce.ToArray();
            return nonce;
        }

        public static byte[] generateRandomNonceUtf8(int length)
        {
            return generateRandomNonce(length, bit: 128);
        }

        /// Generates a new AES key having the desired [length].
        // public static async Task<KeyParameter> generateAesKey(int length = 256)
        public static KeyParameter generateAesKey(int length = 256)
        {
            // Get a secure random
            SecureRandom mySecureRandom = _getSecureRandom();
            // mySecureRandom.GenerateSeed generates byte[], so I must divide by 8 to have the intended number of bits (not by 16 as in Dart code)
            KeyParameter key = new KeyParameter(mySecureRandom.GenerateSeed(length / 8));
            return key;
        }

        /// Generates a new RSA key pair having the given [bytes] length.
        /// If no length is specified, the default is going to be 2048.
        // public static async Task<KeyPair> generateRsaKeyPair(int bytes = 2048)
        public static KeyPair generateRsaKeyPair(int bytes = 2048)
        {
            SecureRandom secureRandom = _getSecureRandom();
            RsaKeyGenerationParameters keyGenerationParameters = new RsaKeyGenerationParameters(new BigInteger("65537", 10), secureRandom, bytes, 5);

            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            AsymmetricCipherKeyPair rsaKeys = keyPairGenerator.GenerateKeyPair();
            RsaKeyParameters rsaPubRaw = (RsaKeyParameters)rsaKeys.Public;
            RsaKeyParameters rsaPrivRaw = (RsaKeyParameters)rsaKeys.Private;
            // Return the key
            return new KeyPair(new RSAPublicKey(rsaPubRaw), new RSAPrivateKey(rsaPrivRaw));
        }

        /// Generates a new random EC key pair.
        // public static async Task<KeyPair> generateEcKeyPair()
        public static KeyPair generateEcKeyPair()
        {
            var curve = ECNamedCurveTable.GetByName("secp256k1");
            ECDomainParameters domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            SecureRandom secureRandom = _getSecureRandom();
            ECKeyGenerationParameters keyParams = new ECKeyGenerationParameters(domainParams, secureRandom);
            ECKeyPairGenerator generator = new ECKeyPairGenerator("ECDSA");
            generator.Init(keyParams);
            AsymmetricCipherKeyPair keyPair = generator.GenerateKeyPair();

            ECPrivateKeyParameters privateKey = keyPair.Private as ECPrivateKeyParameters;
            ECPublicKeyParameters publicKey = keyPair.Public as ECPublicKeyParameters;

            return new KeyPair(new ECPublicKey(publicKey), new ECPrivateKey(privateKey));
        }

        #endregion

        #region Helpers

        /// Generates a SecureRandom
        /// C# has not a fortuna random generator, Just trying an approach with DigestRandomGenerator from BouncyCastle
        private static SecureRandom _getSecureRandom()
        {
            // Start from a crypto seed from C# libraries
            System.Security.Cryptography.RNGCryptoServiceProvider rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[32];
            rngCsp.GetBytes(randomBytes);
            // Get a first random generator from BouncyCastle
            VmpcRandomGenerator firstRandomGenerator = new Org.BouncyCastle.Crypto.Prng.VmpcRandomGenerator();
            firstRandomGenerator.AddSeedMaterial(randomBytes);
            byte[] seed = new byte[32];
            firstRandomGenerator.NextBytes(seed, 0, 32);
            // Create and seed the final Random Generator
            DigestRandomGenerator wkRandomGenerator = new Org.BouncyCastle.Crypto.Prng.DigestRandomGenerator(new Sha512Digest());
            SecureRandom secureRandomGenerator = new SecureRandom(wkRandomGenerator);
            secureRandomGenerator.SetSeed(seed);
            return secureRandomGenerator;
        }

        #endregion
    }
}