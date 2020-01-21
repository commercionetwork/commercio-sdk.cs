using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using System.Net.Http;
using System.Threading.Tasks;
using Axe.SimpleHttpMock;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using sdk_test.TestResources;

namespace sdk_test
{
    [TestClass]
    public class NetworkTest
    {
        [TestMethod]
        public void NetworkQueryReturnsCorrectData()
        {
            const String localTestUrl = "http://example.com";
            
            // Build the mockup server
            var _server = new MockHttpServer();
            //  I need this in order to get the correct data out of the mock server
            Dictionary<String, Object> nodeResponse = JsonConvert.DeserializeObject<Dictionary<String, Object>>(TestResources.TestResources.SentDocumentResponseJson);
            // Initialize Server Response
            _server
                .WithService(localTestUrl)
                .Api("", "GET",  nodeResponse);

            // Link the client to the retrieval class Network
            HttpClient client = new HttpClient(_server);
            Network.client = client;

            // Test response
            List<Dictionary<String, Object>> resultList = (List<Dictionary<String, Object>>) Network.queryChain(localTestUrl).Result;

            List<NetworkTestData> testDataList = resultList.Select(json => new NetworkTestData(json)).ToList();

            Assert.IsTrue(testDataList.Count == 4);
            Assert.IsTrue("did:com:1zfhgwfgex8rc9t00pk6jm6xj6vx5cjr4ngy32v" == testDataList[0].sender);
            Assert.IsTrue("6a881ef0-04da-4524-b7ca-6e5e3b7e61dc" == testDataList[1].uuid);
        }
    }

    // Aux class to collect answer Data
    public class NetworkTestData
    {
        [JsonProperty("sender")]
        public String sender { get; set; }
        [JsonProperty("uuid")]
        public String uuid { get; set; }

        [JsonConstructor]
        public NetworkTestData(String sender, String uuid)
        {
            this.sender = sender;
            this.uuid = uuid;
        }

        public NetworkTestData(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("sender", out outValue))
                this.sender = outValue as String;
            if (json.TryGetValue("uuid", out outValue))
                this.uuid = outValue as String;
        }
    }
}
