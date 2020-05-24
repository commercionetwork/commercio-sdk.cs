// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
///Wrapper of the Ed25519_hd_key
//
using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using commercio.sacco.lib;

namespace commercio.sdk
{
    ///Wrapper of the Ed25519_hd_key
    public class Ed25519PublicKey : PublicKey
    {
        #region Properties

        public String Seed { get; }

        public String keyType { get; set; }

        #endregion

        #region Constructors

        public Ed25519PublicKey(String seed, String keyType = "Ed25519VerificationKey2018")
        {
            this.Seed = seed;
            this.keyType = keyType;
        }

        #endregion

        #region Public Methods

        public String getType()
        {
            return (keyType);
        }


        // **** Code to be reviewed - it compiles, but he has little if any relationship with the Dart code...
        // ****
        public String getEncoded()
        {
            byte[] seedBytes = HexEncDec.StringToByteArray(Seed);
            Ed25519KeyPairGenerator wkGen = new Ed25519KeyPairGenerator();
            SecureRandom rnd = new SecureRandom(seedBytes);
            Ed25519KeyGenerationParameters genPar = new Ed25519KeyGenerationParameters(rnd);
            wkGen.Init(genPar);
            AsymmetricCipherKeyPair keys = wkGen.GenerateKeyPair();
            Ed25519PublicKeyParameters key = (Ed25519PublicKeyParameters) keys.Public;
            byte[] output = key.GetEncoded();
            return Convert.ToBase64String(output);
        }

        #endregion

        #region Helpers

        #endregion
    }

}
