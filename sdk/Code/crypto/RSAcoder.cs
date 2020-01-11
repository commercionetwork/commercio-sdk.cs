// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// RSA Encryption/Decription Engine
//
//

using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;


namespace commercio.sdk
{
    public class RSAcoder
    {
        #region Properties

        public AsymmetricKeyParameter pubkey { get; private set; }
        public AsymmetricKeyParameter privkey { get; private set; }
        private readonly Pkcs1Encoding _engine;

        #endregion

        #region static Methods
        public static AsymmetricCipherKeyPair GenerateKeys(int Strength)
        {
            // Another way to get a random...Unsure about the certaninty parameter
            var rsaKeyParams = new RsaKeyGenerationParameters(BigInteger.ProbablePrime(512, new Random()), new SecureRandom(), Strength, 25);
            var keyGen = new RsaKeyPairGenerator();
            keyGen.Init(rsaKeyParams);

            return keyGen.GenerateKeyPair();
        }

        #endregion

        #region Constructors

        // Init with a random key
        public RSAcoder(int Strength)
        {
            AsymmetricCipherKeyPair keys = GenerateKeys(Strength);
            this.pubkey = keys.Public;
            this.privkey = keys.Private;
            _engine = new Pkcs1Encoding(new RsaEngine());
        }

        public RSAcoder(AsymmetricKeyParameter key)
        {
            if (key.IsPrivate == false)
            {
                this.pubkey = key;
                this.privkey = null;
            }
            else
            {
                this.privkey = key;
                this.pubkey = null;
            }
            _engine = new Pkcs1Encoding(new RsaEngine());
        }

        #endregion

        #region Public Methods

        public byte[] Encrypt(byte[] buffer)
        {
            return Encrypt(buffer, 0, buffer.Length);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return Decrypt(buffer, 0, buffer.Length);
        }

        public byte[] Encrypt(byte[] buffer, int offSet, int length)
        {
            if (pubkey != null)
                return RsaProcessor(buffer, offSet, length, pubkey);
            else
                return null;
        }

        public byte[] Decrypt(byte[] buffer, int offSet, int length)
        {
            if (privkey != null)
                return RsaProcessor(buffer, offSet, length, privkey);
            else
                return null;
        }

        #endregion

        #region Helpers

        private byte[] RsaProcessor(byte[] data, int offset, int length, AsymmetricKeyParameter key)
        {
            Boolean forEncryption = !key.IsPrivate;

            _engine.Init(forEncryption, key);

            var blockSize = _engine.GetInputBlockSize();

            var result = new List<byte>();
            for (var i = offset; i < offset + length; i += blockSize)
            {
                var currentSize = Math.Min(blockSize, offset + length - i);
                result.AddRange(_engine.ProcessBlock(data, i, currentSize));
            }
            return result.ToArray();
        }

        #endregion

    }
}
