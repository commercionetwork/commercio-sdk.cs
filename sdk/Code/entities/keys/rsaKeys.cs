// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Wrapper of the pointyCastle RSAPublicKey
//

using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Pkcs;


namespace commercio.sdk
{
    /// Wrapper of the pointyCastle ECPublicKey
    public class RSAPublicKey : PublicKey
    {
        #region Properties

        public RsaPublicKeyStructure pubKey { get; set; }

        #endregion

        #region Constructors

        public RSAPublicKey(RsaPublicKeyStructure ecPublicKey)
        {
            this.pubKey = ecPublicKey;
        }

        #endregion

        #region Public Methods

        public byte[] getEncoded()
        {
            Asn1EncodableVector asn1Vect = new Asn1EncodableVector();
            asn1Vect.Add(new DerInteger(this.pubKey.Modulus));
            asn1Vect.Add(new DerInteger(this.pubKey.PublicExponent));
            DerSequence sequence = new DerSequence(asn1Vect);
            return sequence.GetEncoded();
        }

        #endregion

        #region Helpers

        #endregion
    }

    /// Wrapper of the pointyCastle RSAPrivateKey

    public class RSAPrivateKey : PrivateKey
    {
        #region Properties

        public RsaPrivateKeyStructure secretKey { get; set; }

        #endregion

        #region Constructors

        public RSAPrivateKey(RsaPrivateKeyStructure ecSecretKey)
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
