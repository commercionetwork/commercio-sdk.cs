// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// AES Encryption/Decritpion Engine
//
//
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto;

namespace commercio.sdk
{
    public class AEScoder
    {
        public enum AESMode
        {
            cbc,
            cfb64,
            ctr,
            ecb,
            ofb64Gctr,
            ofb64,
            sic
        }

        #region Properties

        public KeyParameter key { get;  }

        private readonly BufferedBlockCipher _wkCipher;

        #endregion

        #region Constructors

        // Coder init
        public AEScoder(KeyParameter Aeskey)
        {
            // Works in EBC/PKCS7 only!
            // setup AES cipher in ECB mode with PKCS7 padding
            AesEngine engine = new AesEngine();
            _wkCipher = new PaddedBufferedBlockCipher(engine, new Pkcs7Padding()); //Default scheme is PKCS7
            // _wkCipher = (BufferedBlockCipher) CipherUtilities.GetCipher("AES/ECB/PKCS7Padding");
            this.key = Aeskey;
            _wkCipher.Reset();
        }


        #endregion

        #region Public Methods

        // Encrypt/decrypt using the given key
        public byte[] encode(byte[] data)
        {
            _wkCipher.Init(true, this.key);
            byte[] outputBytes = new byte[_wkCipher.GetOutputSize(data.Length)];
            int length = _wkCipher.ProcessBytes(data, outputBytes, 0);
            _wkCipher.DoFinal(outputBytes, length); //Do the final block
            return outputBytes;
        }

        // Encrypt/decrypt using the given key
        public byte[] decode(byte[] data)
        {
            _wkCipher.Init(false, this.key);
            byte[] outputBytes = new byte[_wkCipher.GetOutputSize(data.Length)];
            int length = _wkCipher.ProcessBytes(data, outputBytes, 0);
            _wkCipher.DoFinal(outputBytes, length); //Do the final block
            return outputBytes;
        }

        #endregion

        #region Helpers
        #endregion
    }
}

