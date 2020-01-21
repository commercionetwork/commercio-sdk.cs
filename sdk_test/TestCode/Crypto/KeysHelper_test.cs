using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using sdk_test.TestResources;


namespace commercio.sdk_test
{
    [TestClass]
    public class KeysHelper_test
    {

        [TestMethod]
        // Check RSA Keys for unicity
        public void TestUniqueRSAKeys()
        {
            int numKeys = 20;
            List <KeyPair> keys = new List<KeyPair>();
            for (int i = 0; i < numKeys; i++)
            {
                keys.Add(KeysHelper.generateRsaKeyPair());
            }
            Dictionary<BigInteger, RSAPublicKey> keysModulus = new Dictionary<BigInteger, RSAPublicKey>();
            keysModulus = keys.ToDictionary
            (
                //Define key
                element => ((RSAPublicKey) element.publicKey).pubKey.Modulus,
                //Define value
                element => (((RSAPublicKey)element.publicKey))
            );

            Assert.IsTrue(keysModulus.Count == numKeys);
        }

        [TestMethod]
        // Check AES Keys for unicity
        public void TestUniqueAESKeys()
        {
            int numKeys = 100;
            List<KeyParameter> keys = new List<KeyParameter>();
            for (int i = 0; i < numKeys; i++)
            {
                KeyParameter k = KeysHelper.generateAesKey();
                keys.Add(k);
                Assert.IsTrue(k.GetKey().GetLength(0) == 32);
            }
            Dictionary<String, KeyParameter> keysBytes = new Dictionary<String, KeyParameter>();
            keysBytes = keys.ToDictionary
            (
                //Define key
                element => Convert.ToBase64String(element.GetKey()),
                //Define value
                element => element
            );

            Assert.IsTrue(keysBytes.Count == numKeys);
        }

        [TestMethod]
        // Check EC Keys for unicity - Useful?
        public void TestUniqueECKeys()
        {
            int numKeys = 200;
            List<KeyPair> keys = new List<KeyPair>();
            for (int i = 0; i < numKeys; i++)
            {
                keys.Add(KeysHelper.generateEcKeyPair());
            }
            Dictionary<ECPoint, ECPublicKey> keysModulus = new Dictionary<ECPoint, ECPublicKey>();
            keysModulus = keys.ToDictionary
            (
                //Define key
                element => ((ECPublicKey)element.publicKey).pubKey.Q,
                //Define value
                element => ((ECPublicKey)element.publicKey)
            );
            Assert.IsTrue(keysModulus.Count == numKeys);
        }


    }
}
