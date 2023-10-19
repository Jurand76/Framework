using Framework.driver;
using Framework.model;
using Framework.page;
using Framework.service;
using OpenQA.Selenium;

namespace Framework
{
    public class OpenMainPage
    {
        protected IWebDriver driver;
        protected MainPage mainPage;
        protected User user;
        protected SearchCalculator searchCalculator;

        [SetUp]
        public void Setup()
        {
            searchCalculator = new SearchCalculator();
            driver = DriverSingleton.getDriver();
            mainPage = new MainPage();
        }

        [Test, Order(0)]
        public void TestSearchService()
        {
            mainPage.openPage();
            searchCalculator.startSearching();
            Assert.IsTrue(searchCalculator.isResultVisible(), "Search results not visible");
        }

        [Test, Order(1)]
        public void TestOpenCalculator()
        {
            searchCalculator.enterSearchLink();
            Assert.IsTrue(searchCalculator.isGoogleCalculatorVisible(), "Google Calculator is not opened");
        }

    }
}