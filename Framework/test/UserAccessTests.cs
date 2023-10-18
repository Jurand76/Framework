using Framework.driver;
using Framework.model;
using Framework.page;
using Framework.service;
using OpenQA.Selenium;

namespace Framework
{
    public class UserAccessTests
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected User user;
        protected UserCreator userCreator;

        [SetUp]
        public void Setup()
        {
            userCreator = new UserCreator();
            driver = DriverSingleton.getDriver();
            loginPage = new LoginPage();
        }

        [Test]
        public void TestLoginUserProperData()
        {
            user = userCreator.withCredentials();
            loginPage.openPage();
            loginPage.rodoPopupClose();
            Assert.Pass();
        }

        public void TestLoginUserWithNoName()
        {
            Assert.Pass();
        }

        public void TestLoginUserWithNoPassword()
        {
            Assert.Pass();
        }
    }
}