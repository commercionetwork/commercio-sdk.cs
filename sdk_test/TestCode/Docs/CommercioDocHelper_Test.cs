using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sdk;
using commercio.sacco.lib;
using KellermanSoftware.CompareNetObjects;

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

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
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

        }

    }
}
