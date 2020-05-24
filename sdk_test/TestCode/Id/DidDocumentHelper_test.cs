using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using KellermanSoftware.CompareNetObjects;

namespace sdk_test
{
    [TestClass]
    public class DidDocumentHelperTest
    {
        [TestMethod]
        public void DidDocumentWellFormedFromWalletTest()
        {
            /* All these tests are removed from SDK Dart 2.1
            
            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            BigInteger modulus = new BigInteger("71849", 10);
            BigInteger exponent = new BigInteger("17", 10);
            RSAPublicKey rsaPubKey = new RSAPublicKey(new RsaKeyParameters(isPrivate: false, modulus: modulus, exponent: exponent));

            DidDocumentPublicKey expectedPubKey = new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-2",
                type: DidDocumentPubKeyType.RSA,
                controller: wallet.bech32Address,
                publicKeyHex: ((String) "300802030118a9020111").ToUpper()
            );
            DidDocumentPublicKey expectedAuthKey = new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-1",
                type: DidDocumentPubKeyType.SECP256K1,
                controller: wallet.bech32Address,
                publicKeyHex: ((String)"0261789822ac69c632dcbab267bf3ff544fdd8ea55a373ef0c320bef9f55f8611e").ToUpper()
            );

            DidDocumentProof expectedComputedProof = new DidDocumentProof(
                type: "LinkedDataSignature2015",
                iso8601creationTimestamp: GenericUtils.getTimeStamp(),
                creatorKeyId: expectedPubKey.id,
                signatureValue: ((String)"625ec032c14affec8f1e4d78b55f7eeb225c4ef98daed5f601d0f436e0bb8bd27bbf3f208af28af9fe59b6beed364765f14423e0ebcd675e2a9dead77d73d892").ToUpper()
            );

            DidDocument expectedDidDocument = new DidDocument(
                context: "https://www.w3.org/ns/did/v1",
                id: wallet.bech32Address,
                publicKeys: new List<DidDocumentPublicKey> { expectedAuthKey, expectedPubKey },
                // authentication: new List<string> { expectedAuthKey.id },
                proof: expectedComputedProof,
                service: new List<DidDocumentService>()
            );

            DidDocument didDocument = DidDocumentHelper.fromWallet(wallet, new List<PublicKey> { rsaPubKey });

            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            // Check it - we use compareNet objects here
            Assert.AreEqual(compareLogic.Compare(didDocument.context, expectedDidDocument.context).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.id, expectedDidDocument.id).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.publicKeys, expectedDidDocument.publicKeys).AreEqual, true);
            // Assert.AreEqual(compareLogic.Compare(didDocument.authentication, expectedDidDocument.authentication).AreEqual, true);
        */
        }
    }

}
