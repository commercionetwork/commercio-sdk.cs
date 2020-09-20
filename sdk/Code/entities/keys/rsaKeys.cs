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
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.OpenSsl;

namespace commercio.sdk
{
    /// Wrapper of the pointyCastle RSAPublicKey
    public class RSAPublicKey : PublicKey
    {
        #region Properties

        public RsaKeyParameters pubKey { get; set; }

        public String keyType { get; set; }

        #endregion

        #region Constructors

        public RSAPublicKey(RsaKeyParameters ecPublicKey, String keyType = "RsaVerificationKey2018")
        {
            this.pubKey = ecPublicKey;
            // I need to manage this this way due to the way C# treats optional values...
            this.keyType = (keyType is null ? "RsaVerificationKey2018" : keyType);
            // this.keyType = keyType;
        }

        #endregion

        #region Public Methods

        public String getType()
        {
            return (keyType);
        }

        public String getEncoded()
        {
            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(pubKey);
            pemWriter.Writer.Flush();
            return textWriter.ToString();
            //*** 20200524 RC: Encoding to be checked against rel. 2.1 Dart!
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

        String encodePrivateKeyToPemPKCS1()
        {

            TextWriter textWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(secretKey);
            pemWriter.Writer.Flush();
            return textWriter.ToString();
            //*** 20200524 RC: Encoding to be checked against rel. 2.1 Dart!
        }

        #endregion

        #region Helpers
        #endregion
    }
}
