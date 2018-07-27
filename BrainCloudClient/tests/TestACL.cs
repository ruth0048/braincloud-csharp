using NUnit.Core;
using NUnit.Framework;
using System.Collections.Generic;
using JsonFx.Json;
using BrainCloud.Common;

namespace BrainCloudTests
{
    [TestFixture]
    public class TestACL : TestFixtureBase
    {
        [Test]
        public void Test()
        {
            ACL acl = new ACL(ACL.Access.ReadWrite);

            Dictionary<string, object> jsonObj = new Dictionary<string, object> { { "other", 1 } };
            
            acl = ACL.CreateFromJson(jsonObj);

            Assert.AreEqual(ACL.Access.ReadOnly, acl.Other);
        }
    }
}