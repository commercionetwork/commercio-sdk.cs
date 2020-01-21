using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using sdk_test.TestResources;


namespace commercio.sdk_test
{
    [TestClass]
    public class EncryptionHelper_test
    {
        public KeyParameter aesKey { get; }

        public EncryptionHelper_test()
        {
            aesKey = new KeyParameter(Encoding.UTF8.GetBytes(@"Xn2r5u8x/A?D(G+KbPdSgVkYp3s6v9y$"));
        }

        [TestMethod]
        // Check AES Encription with short STring
        public void TestAESencryptionWithString()
        {
            String DataToBeEncoded = "Test";
            String expectedResult = "567b77516c6d96978561ee8244b01afb";

            byte[] result = EncryptionHelper.encryptStringWithAes(DataToBeEncoded, aesKey);
            String hexResult = HexEncDec.ByteArrayToString(result);

            Assert.IsTrue(String.Equals(expectedResult, hexResult, StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        // Check AES Encription with Byte Array
        public void TestAESencryptionWithByteArray()
        {
            String originalData = "Super long test that should be encrypted properly";
            byte[] DataToBeEncoded = Encoding.UTF8.GetBytes(originalData);
            String expectedResult = "8031b6fb67ee4cf45e5bff5e6927f016675ed9c8e89b5aed8f9418c8ca04b65706c71a65039302e937342eed892be761251bb3596b64145060fd478a2fe839c7";

            byte[] result = EncryptionHelper.encryptBytesWithAes(DataToBeEncoded, aesKey);
            String hexResult = HexEncDec.ByteArrayToString(result);

            Assert.IsTrue(String.Equals(expectedResult, hexResult, StringComparison.InvariantCultureIgnoreCase));

            byte[] decodeResult = EncryptionHelper.decryptWithAes(result, aesKey);
            String decodeStrResult = Encoding.UTF8.GetString(decodeResult);

            Assert.IsTrue(String.Equals(decodeStrResult, originalData, StringComparison.InvariantCultureIgnoreCase));

        }

        [TestMethod]
        // Check AES decription 
        public void TestAESdecryption()
        {
            String originalData = "8031b6fb67ee4cf45e5bff5e6927f016675ed9c8e89b5aed8f9418c8ca04b65706c71a65039302e937342eed892be761251bb3596b64145060fd478a2fe839c7";
            String expectedResult = "Super long test that should be encrypted properly";
            byte[] DataToBeDecoded = HexEncDec.StringToByteArray(originalData);

            byte[] byteResult = EncryptionHelper.decryptWithAes(DataToBeDecoded, aesKey);
            String result = Encoding.UTF8.GetString(byteResult);

            Assert.IsTrue(String.Equals(expectedResult, result, StringComparison.InvariantCulture));
        }

        [TestMethod]
        // Check RSA encription and decription with string
        public void TestRSAencryptDecryptString()
        {
            KeyPair keys = KeysHelper.generateRsaKeyPair();
            String originalData = "Est Enim Quaedam Flere Voluptas - In girum Imus Nocte Et Consumimur Igni - 42";

            byte[] dataEncoded = EncryptionHelper.encryptStringWithRsa(originalData, keys.publicKey);

            byte[] dataDecoded = EncryptionHelper.decryptBytesWithRsa(dataEncoded, keys.privateKey);
            String decodedData = Encoding.UTF8.GetString(dataDecoded);

            Assert.IsTrue(String.Equals(originalData, decodedData, StringComparison.InvariantCulture));
        }

        [TestMethod]
        // Check RSA encription and decription with byte array
        public void TestRSAencryptDecryptByteArray()
        {
            KeyPair keys = KeysHelper.generateRsaKeyPair();
            String originalData = @"Eh bien , mon prince, Genes et Lucques ne sont plus que des apanages, des 'pomestja de la famille Buonaparte. Non, je vous préviens que si vous ne me dites pas que nous avons la guerre";
            byte[] encodedOriginal = Encoding.UTF8.GetBytes(originalData);
            byte[] dataEncoded = EncryptionHelper.encryptBytesWithRsa(encodedOriginal, keys.publicKey);

            byte[] dataDecoded = EncryptionHelper.decryptBytesWithRsa(dataEncoded, keys.privateKey);
            String decodedData = Encoding.UTF8.GetString(dataDecoded);

            Assert.IsTrue(String.Equals(originalData, decodedData, StringComparison.InvariantCulture));
        }

    }
}
