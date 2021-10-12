using CopyDirectory;
using CopyDirectory.UserInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestMethod]
        public void BasicCopyTest()
        {
            Assert.IsTrue(Target.GetFiles().Length == 0);
            Copier.Copy(Source.FullName, Target.FullName, true);
            Assert.IsTrue(Target.GetFiles().Length == Source.GetFiles().Length);
        }

    }
}
