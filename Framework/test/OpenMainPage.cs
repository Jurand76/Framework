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
        protected ComputingInstance user;
        protected SearchCalculator searchCalculator;
        protected UseCalculator useCalculator;
        protected YopmailPage yopmailPage;
        protected YopMailPageActions yopmailPageActions;

        [SetUp]
        public void Setup()
        {
            searchCalculator = new SearchCalculator();
            useCalculator = new UseCalculator();
            driver = DriverSingleton.getDriver();
            mainPage = new MainPage();
            yopmailPage = new YopmailPage();
            yopmailPageActions = new YopMailPageActions();
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

        [Test, Order(2)]
        public void TestFillCalculatorFields()
        {
            useCalculator = new UseCalculator();
            useCalculator.FillCalculatorFields();
            Assert.IsTrue(useCalculator.AreEstimatedCostsGenerated(), "Estimated costs not generated");
        }

        [Test, Order(3)]
        public void TestGenerateMail()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            yopmailPage.openPage();
            yopmailPageActions.CloseCookiesPopup();
            yopmailPageActions.GenerateMail();
        }
    }
}