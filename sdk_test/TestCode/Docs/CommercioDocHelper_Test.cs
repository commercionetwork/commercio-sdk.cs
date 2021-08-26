using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace sdk_test
{
    [TestClass]
    public class CommercioDocHelper_Test
    {

        [TestMethod]
        // "fromWallet()" returns a well-formed "CommercioDoc" object.
        public async Task WellFormedCommercioDocFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "http://localhost:1317");
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            String senderDid = wallet.bech32Address;
            List<String> recipientDids = new List<String>() { "recipient1", "recipient2" };
            String uuid = System.Guid.NewGuid().ToString();
            CommercioDocMetadata metadata = new CommercioDocMetadata(
                contentUri: "https://example.com/document/metadata",
                schema: new CommercioDocMetadataSchema(
                    uri: "https://example.com/custom/metadata/schema",
                    version: "7.0.0'"
                )
            );

            CommercioDoc expectedCommercioDoc = new CommercioDoc(
                senderDid: senderDid,
                recipientDids: recipientDids,
                uuid: uuid,
                metadata: metadata
            );


            CommercioDoc commercioDoc = await CommercioDocHelper.fromWallet(
                wallet: wallet,
                recipients: recipientDids,
                id: uuid,
                metadata: metadata
            );

            Assert.AreEqual(compareLogic.Compare(commercioDoc.toJson(), expectedCommercioDoc.toJson()).AreEqual, true);

            //Per visualizzare json della classe
            var dataString = JsonConvert.SerializeObject(commercioDoc);

        }

        [TestMethod]
        public async Task test_broadcastStdTx()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "http://localhost:1317");
            
            //primo mnemonic
            String mnemonicString1 = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString1.Split(" ", StringSplitOptions.RemoveEmptyEntries));

            //secondo mnemonic
            String mnemonicString2 = "daughter conduct slab puppy horn wrap bone road custom acoustic adjust target price trip unknown agent infant proof whip picnic exact hobby phone spin";
            List<String> mnemonic2 = new List<String>(mnemonicString2.Split(" ", StringSplitOptions.RemoveEmptyEntries));


            Wallet wallet = Wallet.derive(mnemonic, networkInfo);
            Wallet recipientWallet = Wallet.derive(mnemonic2, networkInfo);
           
           
            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin(denom: "ucommercio", amount: "10000") };

            var dict = new Dictionary<string, object>();
            dict.Add("from_address", wallet.bech32Address);
            dict.Add("to_address", recipientWallet.bech32Address);
            dict.Add("amount", depositAmount);


            StdMsg testmsg = new StdMsg("cosmos-sdk/MsgSend", dict);
            List <StdMsg> Listtestmsg = new List<StdMsg>();
            Listtestmsg.Add(testmsg);

            StdFee fee = new StdFee(depositAmount, "200000");

            //Invio 
            try
            {
                var stdTx = TxBuilder.buildStdTx(Listtestmsg, "", fee);

                var signedStdTx = await TxSigner.signStdTx(wallet: wallet, stdTx: stdTx);
                var result = await TxSender.broadcastStdTx(wallet: wallet, stdTx: signedStdTx);
                if (result.success)
                {
                    Console.WriteLine("Tx send successfully:\n$lcdUrl/txs/${result.hash}");
                }
                else
                {
                    Console.WriteLine("Tx error message:\n${result.error?.errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while testing Sacco:\n$error");
            }




        }

    }
}
