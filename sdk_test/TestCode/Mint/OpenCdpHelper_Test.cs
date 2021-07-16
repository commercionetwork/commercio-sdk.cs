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
    public class mintCCCHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "mintCCC" object.'
        public void WellFormedmintCCCFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            List<StdCoin> depositAmount = new List<StdCoin> { new StdCoin(denom: "commercio", amount: "10") };

            mintCCC expectedmintCCC = new mintCCC(
                depositAmount: depositAmount,
                signerDid: wallet.bech32Address
            );

            mintCCC mintCCC = mintCCCHelper.fromWallet(wallet, depositAmount);

            Assert.AreEqual(compareLogic.Compare(mintCCC.toJson(), expectedmintCCC.toJson()).AreEqual, true);

        }
    }
}
