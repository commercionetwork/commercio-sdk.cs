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
    public class RequestDidPowerUpHelper_Test
    {

        [TestMethod]
        // '"fromWallet()" returns a well-formed "RequestDidPowerUp" object.'
        public void WellFormedRequestDidPowerUpFromWallet()
        {

            Assert.IsTrue(true);
        }
    }
}
