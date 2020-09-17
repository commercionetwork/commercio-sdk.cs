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
    public class OpenCdpHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "OpenCdp" object.'
        public void WellFormedOpenCdpFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin(denom: "commercio", amount: "10") };

            OpenCdp expectedOpenCdp = new OpenCdp(
                depositAmount: depositAmount,
                signerDid: wallet.bech32Address
            );

            OpenCdp openCdp = OpenCdpHelper.fromWallet(wallet, depositAmount);

            Assert.AreEqual(compareLogic.Compare(openCdp.toJson(), expectedOpenCdp.toJson()).AreEqual, true);

        }
    }
}
