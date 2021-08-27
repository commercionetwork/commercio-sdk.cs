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
    public class CommercioDocReceiptHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "CommercioDocReceipt" object.'
        public void WellFormedCommercioDocReceiptFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);


            String uuid = Guid.NewGuid().ToString();
            String recipientDid = "did:com:id";
            String txHash = "txHash";
            String documentId = "documentId";

            CommercioDocReceipt expectedDocReceipt = new CommercioDocReceipt(
                uuid: uuid,
                senderDid: wallet.bech32Address,
                recipientDid: recipientDid,
                txHash: txHash,
                documentUuid: documentId
            );

            CommercioDocReceipt commercioDocReceipt = CommercioDocReceiptHelper.fromWallet(
                wallet: wallet,
                recipient: recipientDid,
                txHash: txHash,
                documentId: documentId
            );

            Assert.AreEqual(compareLogic.Compare(commercioDocReceipt.uuid, expectedDocReceipt.uuid).AreEqual, false);
            Assert.AreEqual(compareLogic.Compare(commercioDocReceipt.senderDid, expectedDocReceipt.senderDid).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(commercioDocReceipt.recipientDid, expectedDocReceipt.recipientDid).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(commercioDocReceipt.txHash, expectedDocReceipt.txHash).AreEqual, true);
            Assert.AreEqual(compareLogic.Compare(commercioDocReceipt.documentUuid, expectedDocReceipt.documentUuid).AreEqual, true);

        }
    }
}
