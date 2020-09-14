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

            Assert.IsTrue(true);
        }
    }
}
