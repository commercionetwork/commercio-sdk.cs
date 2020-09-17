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
    public class BuyMembershipHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "BuyMembership" object.'
        public void WellFormedBuyMembershipFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "dash ordinary anxiety zone slot rail flavor tortoise guilt divert pet sound ostrich increase resist short ship lift town ice split payment round apology";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            MembershipType membershipType = MembershipType.BLACK;

            BuyMembership expectedBuyMembership = new BuyMembership(membershipType: MyEnumExtensions.ToEnumMemberAttrValue(membershipType), buyerDid: wallet.bech32Address);

            BuyMembership buyMembership = BuyMembershipHelper.fromWallet(wallet, MembershipType.BLACK);

            Assert.AreEqual(compareLogic.Compare(buyMembership.toJson(), expectedBuyMembership.toJson()).AreEqual, true);

        }
    }
}
