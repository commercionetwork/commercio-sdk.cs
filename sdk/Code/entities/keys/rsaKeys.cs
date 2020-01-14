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

namespace commercio.sdk
{
    /// Wrapper of the pointyCastle RSAPublicKey
    public class RSAPublicKey : PublicKey
    {
        #region Properties

        public RsaKeyParameters pubKey { get; set; }

        #endregion

        #region Constructors

        public RSAPublicKey(RsaKeyParameters ecPublicKey)
        {
            this.pubKey = ecPublicKey;
        }

        #endregion

        #region Public Methods

        public byte[] getEncoded()
        {
            Asn1EncodableVector asn1Vect = new Asn1EncodableVector();
            asn1Vect.Add(new DerInteger(this.pubKey.Modulus));
            asn1Vect.Add(new DerInteger(this.pubKey.Exponent));
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

        public RsaKeyParameters secretKey { get; set; }

        #endregion

        #region Constructors

        public RSAPrivateKey(RsaKeyParameters ecSecretKey)
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
