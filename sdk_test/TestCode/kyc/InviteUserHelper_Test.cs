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
    public class InviteUserHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "InviteUser" object.'
        public void WellFormedInviteUserFromWallet()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            NetworkInfo networkInfo = new NetworkInfo(bech32Hrp: "did:com:", lcdUrl: "");
            String mnemonicString = "gorilla soldier device force cupboard transfer lake series cement another bachelor fatigue royal lens juice game sentence right invite trade perfect town heavy what";
            List<String> mnemonic = new List<String>(mnemonicString.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            Wallet wallet = Wallet.derive(mnemonic, networkInfo);

            String recipientDid = "did:com:id";

            InviteUser expectedInviteUser = new InviteUser(recipientDid: recipientDid, senderDid: wallet.bech32Address);

            InviteUser inviteUser = InviteUserHelper.fromWallet(wallet, recipientDid);

            Assert.AreEqual(compareLogic.Compare(inviteUser.toJson(), expectedInviteUser.toJson()).AreEqual, true);

        }
    }
}
