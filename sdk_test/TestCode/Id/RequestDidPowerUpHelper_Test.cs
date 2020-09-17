using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using Axe.SimpleHttpMock;
using commercio.sdk;
using commercio.sacco.lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using KellermanSoftware.CompareNetObjects;

namespace sdk_test
{
    [TestClass]
    public class RequestDidPowerUpHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "RequestDidPowerUp" object.'
        public async Task WellFormedRequestDidPowerUpFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            String lcdUrl = "http://url";
            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: lcdUrl);
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            Wallet pairwaisedWallet = Wallet.derive(mnemonic, networkInfo, lastDerivationPathSegment: "1");

            List<StdCoin> amount = new List<StdCoin> { new StdCoin(denom: "denom", amount: "10") };

            // Here the mock server part...
            // Build the mockup server
            String localTestUrl1 = $"{lcdUrl}/government/tumbler";
            String localTestUrl2 = $"{lcdUrl}/identities/did:com:14ttg3eyu88jda8udvxpwjl2pwxemh72w0grsau";

            var _server = new MockHttpServer();
            //  I need this in order to get the correct data out of the mock server
            Dictionary<string, object> nodeResponse1 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.tumblerAddressJson);
            Dictionary<String, Object> nodeResponse2 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.tumblerIdentityJson);
            // Initialize Server Response
            _server
                .WithService(localTestUrl1)
                .Api("", "GET", nodeResponse1);
            _server
                .WithService(localTestUrl2)
                .Api("", "GET", nodeResponse2);

            // Link the client to the retrieval class Network
            HttpClient client = new HttpClient(_server);
            Network.client = client;


            KeyPair keyPair = KeysHelper.generateRsaKeyPair();

            String powerUpProof = "powerUpProof";
            String uuid = Guid.NewGuid().ToString();
            String encryptionKey = "encryptionKey";

            RequestDidPowerUp expectedRequestDidPowerUp = new RequestDidPowerUp(
                claimantDid: wallet.bech32Address,
                amount: amount,
                powerUpProof: Convert.ToBase64String(Encoding.UTF8.GetBytes(powerUpProof)),
                uuid: uuid,
                encryptionKey: Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptionKey))
            );

            RequestDidPowerUp requestDidPowerUp = await RequestDidPowerUpHelper.fromWallet(
                wallet,
                pairwaisedWallet.bech32Address,
                amount,
                (RSAPrivateKey) keyPair.privateKey
            );

            Assert.AreEqual(compareLogic.Compare(requestDidPowerUp.claimantDid, expectedRequestDidPowerUp.claimantDid).AreEqual, true);
            Assert.AreEqual(requestDidPowerUp.amount.Count, expectedRequestDidPowerUp.amount.Count);
            Assert.AreEqual(compareLogic.Compare(requestDidPowerUp.amount[0].toJson(), expectedRequestDidPowerUp.amount[0].toJson()).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(requestDidPowerUp.uuid, expectedRequestDidPowerUp.uuid).AreEqual, false);
            Assert.AreEqual(compareLogic.Compare(requestDidPowerUp.powerUpProof, expectedRequestDidPowerUp.powerUpProof).AreEqual, false);
            Assert.AreEqual(compareLogic.Compare(requestDidPowerUp.encryptionKey, expectedRequestDidPowerUp.encryptionKey).AreEqual, false);

        }
    }
}
