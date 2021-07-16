using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using sdk_test.TestResources;


namespace commercio.sdk_test
{
    [TestClass]
    public class RSAKeyParser_test
    {

        [TestMethod]
        public void TestParsingKeys()
        {
            String RSAPublicText = TestResources.RSAPublicText;

            // Test the parsing
            RsaKeyParameters pubKey = RSAKeyParser.parsePublicKeyFromPem(RSAPublicText);
            // Here to make debug simpler...
            BigInteger modulus = pubKey.Modulus;
            BigInteger exp = pubKey.Exponent;
            BigInteger testModulus = new BigInteger(HexEncDec.StringToByteArray(TestResources.EncodedHexPubModulus));
            BigInteger testExp = new BigInteger(TestResources.PubExponentTextValue.ToString());
            // Check the RSA
            Assert.IsTrue(modulus.CompareTo(testModulus) == 0);
            Assert.IsTrue(exp.CompareTo(testExp) == 0);
        }
    }
}
