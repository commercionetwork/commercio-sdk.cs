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
    public class Utils_Test
    {

        [TestMethod]
        // 'Function "calculateDefaultFee" returns expected values;'
        public void CorrectCalculateFee()
        {
            const int defaultAmount = 100;
            const String defaultDenom = "commercio";
            const int defaultGas = 200;
            int msgsNumber = 2;

            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            StdFee expectedFee = new StdFee(
                amount: new List<StdCoin>
                {
                    new StdCoin(
                        denom: defaultDenom,
                        amount: (defaultAmount * msgsNumber).ToString()
                    )
                },
                gas: (defaultGas * msgsNumber).ToString()
            );

            StdFee fee = GenericUtils.calculateDefaultFee(
                msgsNumber: msgsNumber,
                fee: defaultAmount,
                denom: defaultDenom,
                gas: defaultGas
            );

            Assert.AreEqual(compareLogic.Compare(fee.toJson(), expectedFee.toJson()).AreEqual, true);
        }

        [TestMethod]
        // 'Verify the usage of EnumExtension'
        public void VeryfyCorrectEnumExtension()
        {
            //This is the comparison class
            CompareLogic compareLogic = new CompareLogic();

            MembershipType mblack = MembershipType.BLACK;
            MembershipType mbronze = MembershipType.BRONZE;
            MembershipType mgold = MembershipType.GOLD;
            MembershipType msilver = MembershipType.SILVER;

            String sblack = MyEnumExtensions.ToEnumMemberAttrValue(mblack);
            String sbronze = MyEnumExtensions.ToEnumMemberAttrValue(mbronze);
            String sgold = MyEnumExtensions.ToEnumMemberAttrValue(mgold);
            String ssilver = MyEnumExtensions.ToEnumMemberAttrValue(msilver);

            Assert.AreEqual(sblack, "black");
            Assert.AreEqual(sbronze, "bronze");
            Assert.AreEqual(sgold, "gold");
            Assert.AreEqual(ssilver, "silver");

            BroadcastingMode modeAsync = BroadcastingMode.ASYNC;
            BroadcastingMode modeBlock = BroadcastingMode.BLOCK;
            BroadcastingMode modeSync = BroadcastingMode.SYNC;

            Assert.AreEqual(MyEnumExtensions.ToEnumMemberAttrValue(modeAsync), "async");
            Assert.AreEqual(MyEnumExtensions.ToEnumMemberAttrValue(modeBlock), "block");
            Assert.AreEqual(MyEnumExtensions.ToEnumMemberAttrValue(modeSync), "sync");

        }
    }
}
