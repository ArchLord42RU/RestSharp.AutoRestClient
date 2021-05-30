using AutoRest.Client.Processing;
using NUnit.Framework;

namespace AutoRest.Client.Tests
{
    public class UtilsTests
    {
        [Test]
        public void Should_Combine_Two_Uri()
        {
            const string left = "/foo";
            const string right = "/bar";
            
            Assert.AreEqual("/foo/bar", ProcessingUtils.CombineUrl(left, right));
        }
    }
}