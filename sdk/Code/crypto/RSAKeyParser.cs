// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily parse a PEM-encoded RSA public key.
//
using System;
using System.IO;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace commercio.sdk
{
    public class RSAKeyParser
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Reads the given [pemString] as a PEM-encoded RSA public key, returning
        /// the object representing the key itself.
        /// The aprroach is quite different from Dart code, we let Bouncycatle do all the work...
        public static RsaKeyParameters parsePublicKeyFromPem(String pemString)
        {
            // RsaPublicKeyStructure publicKeyStructure = null;
            RsaKeyParameters publicKeyParameter = null;

            TextReader sr = new StringReader(pemString);
            PemReader sslReader = new PemReader(sr);
            Object readObj = sslReader.ReadObject();
            if (readObj is RsaKeyParameters)
            {
                publicKeyParameter = (RsaKeyParameters) readObj;
                // publicKeyStructure = new RsaPublicKeyStructure(publicKeyParameter.Modulus, publicKeyParameter.Exponent);
            }
            else
            {
                System.ArgumentException argEx = new System.ArgumentException($"Cannot parse RSA public key - input PEM is: {pemString}");
                throw argEx;
            }
            // return publicKeyStructure;
            return publicKeyParameter;
        }

        #endregion

        #region Helpers
        #endregion

    }
}
