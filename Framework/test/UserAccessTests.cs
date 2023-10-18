using Framework.driver;
using Framework.model;
using Framework.service;
using OpenQA.Selenium;

namespace Framework
{
    public class UserAccessTests
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            UserCreator userCreator = new UserCreator();
            User userProperLogin = userCreator.withCredentials();
            User userWithEmptyUserName = userCreator.withEmptyUserName();
            User userWithEmptyPassword = userCreator.withEmptyUserPassword();
            driver = DriverSingleton.getDriver();
        }

        [Test]
        public void TestLoginUserProperData()
        {


            Assert.Pass();
        }
    }
}