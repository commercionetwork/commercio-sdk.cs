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
    public class burnCCCHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "burnCCC" object.'
        public void WellFormedburnCCCFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            int timeStamp = 64738;

            burnCCC expectedburnCCC = new burnCCC(signerDid: wallet.bech32Address, timeStamp: timeStamp.ToString());

            burnCCC burnCCC = burnCCCHelper.fromWallet(wallet, timeStamp);

            Assert.AreEqual(compareLogic.Compare(burnCCC.toJson(), expectedburnCCC.toJson()).AreEqual, true);

        }
    }
}
