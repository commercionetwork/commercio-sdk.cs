// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
// Wrapper for Bouncycastle EC keys
//
using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace commercio.sdk
{
    /// Wrapper of the pointyCastle ECPublicKey
    public class ECPublicKey : PublicKey
    {
        #region Properties

        public ECPublicKeyParameters pubKey { get; set; }

        public String keyType { get; set; }

        #endregion

        #region Constructors

        public ECPublicKey(ECPublicKeyParameters ecPublicKey, String keyType = "Secp256k1VerificationKey2018")
        {
            this.pubKey = ecPublicKey;
            this.keyType = keyType;
        }

        #endregion

        #region Public Methods

        public String getType()
        {
            return (keyType);
        }


        public String getEncoded()
        {
            return Convert.ToBase64String(this.pubKey.Q.GetEncoded());
        }

        #endregion

        #region Helpers

        #endregion
    }

    /// Wrapper of the pointyCastle ECPrivateKey

    public class ECPrivateKey : PrivateKey
    {
        #region Properties

        public ECPrivateKeyParameters secretKey { get; set; }

        #endregion

        #region Constructors

        public ECPrivateKey(ECPrivateKeyParameters ecSecretKey)
        {
            this.secretKey = ecSecretKey;
        }

        #endregion

        #region Public Methods
        #endregion

        #region Helpers
        #endregion
    }
}
