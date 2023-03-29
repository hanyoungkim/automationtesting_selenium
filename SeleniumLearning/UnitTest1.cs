using NUnit.Framework;

namespace SeleniumLearning
{
    public class Tests
    {
        //[SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Setup method exeucution");
        }

        //[Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("Test1 method exeucution");
            Assert.Pass();
        }

        //[Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("Test2 method exeucution");
            Assert.Pass();
        }

        //[TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Close method exeucution");
        }
    }
}