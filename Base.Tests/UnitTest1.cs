using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Base.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestCategory("Test Tests")]
        [TestMethod("Teste demo")]
        [DataRow("abc")]
        [DataRow("efg")]
        public void TestMethod1(string test)
        {
            Assert.IsNotNull(test);
        }
    }
}
