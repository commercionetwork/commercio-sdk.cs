using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using commercio.sdk;
using commercio.sacco.lib;
using Axe.SimpleHttpMock;
using KellermanSoftware.CompareNetObjects;

namespace sdk_test
{
    [TestClass]
    public class DidDocumentHelper_Test
    {
        [TestMethod]
        public void DidDocumentWellFormedFromWalletTest()
        {

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            // Numbers are different from Dart version as BouncyCastle didn't like the Dart ones (namely 125 and 126)
            BigInteger modulusVerification = new BigInteger("8377", 10);
            BigInteger exponentVerification = new BigInteger("131", 10);
            RSAPublicKey rsaPubKeyVerification = new RSAPublicKey(new RsaKeyParameters(isPrivate: false, modulus: modulusVerification, exponent: exponentVerification));

            DidDocumentPublicKey verificationPubKey = new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-1",
                type: "RsaVerificationKey2018",
                controller: wallet.bech32Address,
                publicKeyPem: rsaPubKeyVerification.getEncoded()
            );

            // Numbers are different from Dart version as BouncyCastle didn't like the Dart ones (namely 135 and 136)
            BigInteger modulusSignature = new BigInteger("135", 10);
            BigInteger exponentSignature = new BigInteger("139", 10);
            RSAPublicKey rsaPubKeySignature = new RSAPublicKey(new RsaKeyParameters(isPrivate: false, modulus: modulusVerification, exponent: exponentVerification), keyType: "RsaSignatureKey2018");

            DidDocumentPublicKey signaturePubKey = new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-2",
                type: "RsaSignatureKey2018",
                controller: wallet.bech32Address,
                publicKeyPem: rsaPubKeySignature.getEncoded()
            );

            DidDocumentProofSignatureContent proofSignatureContent = new DidDocumentProofSignatureContent(
                context: "https://www.w3.org/ns/did/v1",
                id: wallet.bech32Address,
                publicKeys: new List<DidDocumentPublicKey> { verificationPubKey, signaturePubKey }
            );


            DidDocumentProof expectedComputedProof = new DidDocumentProof(
                type: "EcdsaSecp256k1VerificationKey2019",
                timestamp: GenericUtils.getTimeStamp(), //*** Here we should have a ISO-8601 time stamp!
                proofPurpose: "authentication",
                controller: wallet.bech32Address,
                verificationMethod: wallet.bech32PublicKey,
                signatureValue: Convert.ToBase64String(
                    SignHelper.signSorted(proofSignatureContent.toJson(), wallet)
                )
            );

            DidDocument expectedDidDocument = new DidDocument(
                context: "https://www.w3.org/ns/did/v1",
                id: wallet.bech32Address,
                publicKeys: new List<DidDocumentPublicKey> { verificationPubKey, signaturePubKey },
                proof: expectedComputedProof,
                service: new List<DidDocumentService>()
            );

            // This is only to be sure that timestampp is different
            System.Threading.Thread.Sleep(2000);

            DidDocument didDocument = DidDocumentHelper.fromWallet(wallet, new List<PublicKey> { rsaPubKeyVerification, rsaPubKeySignature });

            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            // Check it - we use compareNet objects here
            Assert.AreEqual(compareLogic.Compare(didDocument.context, expectedDidDocument.context).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.id, expectedDidDocument.id).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.publicKeys, expectedDidDocument.publicKeys).AreEqual, true);

            Assert.AreEqual(compareLogic.Compare(didDocument.proof.type, expectedDidDocument.proof.type).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.proof.proofPurpose, expectedDidDocument.proof.proofPurpose).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.proof.controller, expectedDidDocument.proof.controller).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.proof.verificationMethod, expectedDidDocument.proof.verificationMethod).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(didDocument.proof.timestamp, expectedComputedProof.timestamp).AreEqual, false);
            // The difference depends on the "secureRandom" method used at the time of the signature.
            Assert.AreEqual(compareLogic.Compare(didDocument.proof.signatureValue, expectedDidDocument.proof.signatureValue).AreEqual, false);

            Assert.AreEqual(didDocument.proof.signatureValue.Length, expectedDidDocument.proof.signatureValue.Length);
            // WE also verify the json here

            Dictionary<String, Object> expectedDidDocumentJson = expectedDidDocument.toJson();
            Dictionary<String, Object> didDocumentJson = didDocument.toJson();
            // Assert.AreEqual(compareLogic.Compare(expectedDidDocumentJson, didDocumentJson).AreEqual, true);
            // They cannot be equal because of time stamps

        }


        //[TestMethod]
        //public async Task DidDocumentFromWalletJsonTest()
        //{
        //    String lcdUrl = "http://url";
        //    NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: lcdUrl);
        //    String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
        //    List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
        //    Wallet wallet = Wallet.derive(mnemonic, networkInfo);

        //    // Here the mock server part...
        //    // Build the mockup server

        //    String localTestUrl1 = $"{lcdUrl}/auth/accounts/did:com:1gkfhddf8hxj38x74zjxla072wyppej7xv9psfg";
        //    // String localTestUrl2 = $"{lcdUrl}/identities/did:com:14ttg3eyu88jda8udvxpwjl2pwxemh72w0grsau";

        //    var _server = new MockHttpServer();
        //    //  I need this in order to get the correct data out of the mock server
        //    Dictionary<string, object> nodeResponse1 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.accountResponse);
        //    // Dictionary<String, Object> nodeResponse2 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.tumblerIdentityJson);
        //    // Initialize Server Response
        //    _server
        //        .WithService(localTestUrl1)
        //        .Api("", "GET", nodeResponse1);
        //    //_server
        //    //    .WithService(localTestUrl2)
        //    //    .Api("", "GET", nodeResponse2);

        //    // Link the client to the retrieval class Network
        //    HttpClient client = new HttpClient(_server);
        //    Network.client = client;

        //    KeyPair rsaKeyPair = KeysHelper.generateRsaKeyPair();
        //    KeyPair ecKeyPair = KeysHelper.generateEcKeyPair();

        //    List<PublicKey> pubKeys = new List<PublicKey>();
        //    pubKeys.Add(rsaKeyPair.publicKey);
        //    pubKeys.Add(ecKeyPair.publicKey);

        //    DidDocument didDocument = DidDocumentHelper.fromWallet(wallet, pubKeys);
        //    TransactionResult results = await IdHelper.setDidDocument(didDocument, wallet);

        //    // Just to test no exception in the code
        //    Assert.AreEqual(true, true);

        //}
    }

}
