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
        public static RsaPublicKeyStructure parsePublicKeyFromPem(String pemString)
        {
            RsaPublicKeyStructure publicKeyStructure = null;

            TextReader sr = new StringReader(pemString);
            PemReader sslReader = new PemReader(sr);
            Object readObj = sslReader.ReadObject();
            if (readObj is RsaKeyParameters)
            {
                RsaKeyParameters publicKeyParameter = (RsaKeyParameters)readObj;
                publicKeyStructure = new RsaPublicKeyStructure(publicKeyParameter.Modulus, publicKeyParameter.Exponent);
            }
            else
            {
                System.ArgumentException argEx = new System.ArgumentException($"Cannot parse RSA public key - input PEM is: {pemString}");
                throw argEx;
            }
            return publicKeyStructure;
        }

        #endregion

        #region Helpers
        #endregion

        /*
        class RSAKeyParser {
          /// Reads the given [pemString] as a PEM-encoded RSA public key, returning
          /// the object representing the key itself.
          static pointy_castle.RSAPublicKey parsePublicKeyFromPem(String pemString) {
            List<int> publicKeyDER = _decodePEM(pemString);
            var asn1Parser = ASN1Parser(publicKeyDER);
            var topLevelSeq = asn1Parser.nextObject() as ASN1Sequence;
            var publicKeyBitString = topLevelSeq.elements[1];

            var publicKeyAsn = ASN1Parser(publicKeyBitString.contentBytes());
            ASN1Sequence publicKeySeq = publicKeyAsn.nextObject();
            var modulus = publicKeySeq.elements[0] as ASN1Integer;
            var exponent = publicKeySeq.elements[1] as ASN1Integer;

            return pointy_castle.RSAPublicKey(
              modulus.valueAsBigInteger,
              exponent.valueAsBigInteger,
            );
          }

          /// Decodes the given [pem] string returning the represented bytes.
          static List<int> _decodePEM(String pem) {
            pem = pem.replaceAll('\n', '');
            pem = pem.replaceAll('\r', '');

            var startsWith = [
              "-----BEGIN PUBLIC KEY-----",
              "-----BEGIN RSA PUBLIC KEY-----"
            ];
            var endsWith = ["-----END PUBLIC KEY-----", "-----END RSA PUBLIC KEY-----"];
            bool isOpenPgp = pem.contains('BEGIN PGP') != -1;

            for (var s in startsWith) {
              if (pem.startsWith(s)) {
                pem = pem.substring(s.length);
              }
            }

            for (var s in endsWith) {
              if (pem.endsWith(s)) {
                pem = pem.substring(0, pem.length - s.length);
              }
            }

            if (isOpenPgp) {
              var index = pem.indexOf('\r\n');
              pem = pem.substring(0, index);
            }

            return base64.decode(pem);
          }
        }

        */
    }
}
