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

        #endregion

        #region Constructors

        public ECPublicKey(ECPublicKeyParameters ecPublicKey)
        {
            this.pubKey = ecPublicKey;
        }

        #endregion

        #region Public Methods

        public byte[] getEncoded()
        {
            return this.pubKey.Q.GetEncoded();
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
