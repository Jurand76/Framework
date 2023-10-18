using Framework.driver;
using OpenQA.Selenium;

namespace Framework
{
    public class Tests
    {
        protected IWebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = DriverSingleton.getDriver();            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}