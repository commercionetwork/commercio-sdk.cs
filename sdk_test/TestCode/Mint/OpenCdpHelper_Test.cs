using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using KellermanSoftware.CompareNetObjects;
using Axe.SimpleHttpMock;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace sdk_test
{
    [TestClass]
    public class mintCCCHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "mintCCC" object.'
        public void WellFormedmintCCCFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();
            String lcdUrl = "https://lcd-demo.commercio.network";
            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: lcdUrl);
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin(denom: "commercio", amount: "10") };
            String uuid = System.Guid.NewGuid().ToString();
            mintCCC expectedmintCCC = new mintCCC(
                depositAmount: depositAmount,
                signerDid: wallet.bech32Address
            );

            mintCCC mintCCC = mintCCCHelper.fromWallet(wallet, depositAmount);

            Assert.AreEqual(compareLogic.Compare(mintCCC.toJson(), expectedmintCCC.toJson()).AreEqual, true);

        }


        [TestMethod]
        public async Task MintCCCTest()
        {
            String lcdUrl = "https://lcd-demo.commercio.network";
            // string didcom = "1fvwfjx2yealxyw5hktqnvm5ynljlc8jqkkd8kl";
            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: lcdUrl);
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            //Prepara data
            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin(denom: "commercio", amount: "1000") };

            ////mintCCC expectedmintCCC = new mintCCC(
            ////    depositAmount: depositAmount,
            ////    signerDid: wallet.bech32Address
            ////);

            mintCCC mintCCC = mintCCCHelper.fromWallet(wallet, depositAmount);

            // Here the mock server part...
            // Build the mockup server

            ///commerciomint/etps/${address}  for read all past transaction

            String localTestUrl1 = $"{lcdUrl}/commerciomint/conversion_rate";
            ////String localTestUrl1 = $"{lcdUrl}/commerciomint/etps/did:com:1fvwfjx2yealxyw5hktqnvm5ynljlc8jqkkd8kl";
            // http://localhost:1317/commerciomint/conversion_rate
            // String localTestUrl2 = $"{lcdUrl}/identities/did:com:1fvwfjx2yealxyw5hktqnvm5ynljlc8jqkkd8kl";

            ////var _server = new MockHttpServer();
            //////  I need this in order to get the correct data out of the mock server
            ////Dictionary<string, object> nodeResponse1 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.accountResponse);
            ////// Dictionary<String, Object> nodeResponse2 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.tumblerIdentityJson);
            ////// Initialize Server Response
            ////_server
            ////    .WithService(localTestUrl1)
            ////    .Api("", "GET", mintCCC);



            // Link the client to the retrieval class Network
            HttpClient client = new HttpClient();
            Network.client = client;
            // Get the server response
            HttpResponseMessage response = await client.GetAsync(localTestUrl1);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                System.ArgumentException argEx = new System.ArgumentException($"Expected status code OK (200) but got ${response.StatusCode} - ${response.ReasonPhrase}");
                throw argEx;
            }

            // Parse the data
            String jsonResponse = await response.Content.ReadAsStringAsync();

            
            // Just to test no exception in the code
            Assert.AreEqual(true, true);

        }
    }
}
