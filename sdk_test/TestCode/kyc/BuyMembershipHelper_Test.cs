using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using KellermanSoftware.CompareNetObjects;
using Axe.SimpleHttpMock;
using System.Net.Http;
using System.Threading.Tasks;

namespace sdk_test
{
    [TestClass]
    public class BuyMembershipHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "BuyMembership" object.'
        public async Task WellFormedBuyMembershipFromWalletAsync()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();
            String lcdUrl = "http://localhost:1317";
            string didcom = "1fvwfjx2yealxyw5hktqnvm5ynljlc8jqkkd8kl";
            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:"+didcom, lcdUrl: "");
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            MembershipType membershipType = MembershipType.GREEN;

            BuyMembership expectedBuyMembership = new BuyMembership(membershipType: MyEnumExtensions.ToEnumMemberAttrValue(membershipType), buyerDid: wallet.bech32Address, tsp: "");

            BuyMembership buyMembership = BuyMembershipHelper.fromWallet(wallet, MembershipType.GREEN, tsp: "");

            MsgBuyMembership MsgMember = new MsgBuyMembership(buyMembership);
            String localTestUrl1 = $"{lcdUrl}/commercio/MsgBuyMembership";

            var _server = new MockHttpServer();
            //////  I need this in order to get the correct data out of the mock server
            ////Dictionary<string, object> nodeResponse1 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.accountResponse);
            ////// Dictionary<String, Object> nodeResponse2 = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.tumblerIdentityJson);
            ////// Initialize Server Response
            _server
                .WithService(localTestUrl1)
                .Api("", "GET", MsgMember);

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




            Assert.AreEqual(compareLogic.Compare(buyMembership.toJson(), expectedBuyMembership.toJson()).AreEqual, true);

        }
    }
}
