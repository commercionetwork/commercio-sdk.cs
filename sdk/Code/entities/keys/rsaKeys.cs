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
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

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
            //TextWriter textWriter = new StringWriter();
            //PemWriter pemWriter = new PemWriter(textWriter);
            //pemWriter.WriteObject(pubKey);
            //pemWriter.Writer.Flush();
            //String pemString = textWriter.ToString();

            // I am forced to use this type of PEM coding, although I am not sure if it is right.
            // The previous code from Bouncy Castle, although technicaly correct, doesn't output the same coding od Dart code.
            DerSequence pubKeySequence = new DerSequence ( new DerInteger(pubKey.Modulus), new DerInteger(pubKey.Exponent));
            string dataBase64 = Convert.ToBase64String(pubKeySequence.GetEncoded());
            string pemString = $"-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A{dataBase64}\n-----END PUBLIC KEY-----";

            return pemString;
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
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(secretKey);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
            string serializedPrivate = Convert.ToBase64String(serializedPrivateBytes);
            RsaPrivateCrtKeyParameters localPprivateKey = (RsaPrivateCrtKeyParameters) PrivateKeyFactory.CreateKey(Convert.FromBase64String(serializedPrivate));

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
