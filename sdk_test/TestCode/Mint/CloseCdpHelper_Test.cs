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
    public class CloseCdpHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "CloseCdp" object.'
        public void WellFormedCloseCdpFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            int timeStamp = 64738;

            CloseCdp expectedCloseCdp = new CloseCdp(signerDid: wallet.bech32Address, timeStamp: timeStamp.ToString());

            CloseCdp closeCdp = CloseCdpHelper.fromWallet(wallet, timeStamp);

            Assert.AreEqual(compareLogic.Compare(closeCdp.toJson(), expectedCloseCdp.toJson()).AreEqual, true);

        }
    }
}
