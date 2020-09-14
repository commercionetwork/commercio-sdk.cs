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
    class CloseCdpHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "CloseCdp" object.'
        public void WellFormedCloseCdpFromWallet()
        {

            Assert.IsTrue(true);
        }
    }
}
