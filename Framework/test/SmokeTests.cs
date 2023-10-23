using Framework.driver;
using Framework.model;
using Framework.page;
using Framework.service;
using Framework.utils;
using OpenQA.Selenium;
using NLog;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using OpenQA.Selenium.Support.UI;

namespace Framework.test
{
    

    public class SmokeTests
    {
        protected IWebDriver driver;
        protected MainPage mainPage;
        protected ComputingInstance instance;
        protected SearchCalculator searchCalculator;
        protected UseCalculator useCalculator;
        protected YopmailPage yopmailPage;
        protected YopMailPageActions yopmailPageActions;
        protected string yopmailAddress = "";
        protected string amountFromEmail = "";
        protected string amountFromEstimation = "";
        protected Logger logger = LogManager.GetCurrentClassLogger();
        readonly ConfigModel configModel = ConfigModel.GetConfiguration();

        [SetUp]
        public void Setup()
        {
            searchCalculator = new SearchCalculator();
            useCalculator = new UseCalculator();
            driver = DriverSingleton.getDriver();
            mainPage = new MainPage();
            yopmailPage = new YopmailPage();
            yopmailPageActions = new YopMailPageActions();
            instance = new ComputingInstance();
        }

        [Test]
        public void TestCheckDriverIfNotNull()
        {
            try
            {
                logger.Info("Trying check webdriver if not null");
                Assert.NotNull(driver, "Webdriver is null");
            }
            catch (Exception ex)
            {
                logger.Error("Webdriver not initialized properly. Error: " + ex);
                Utilities.TakeScreenshot(driver, "CheckDriverIfNotNull");
                throw;
            }
        }

        [Test]
        public void TestConfigJSONContainsConfiguration()
        {
            try
            {
                logger.Info("Checking for configuration file with gooogle URL");
                string expectedUrl = "https://cloud.google.com/";
                string configuredUrl = configModel.Url_google_cloud;
                Assert.That(configuredUrl, Is.EqualTo(expectedUrl), "Not equal URL for google cloud search service");
            }
            catch (Exception ex)
            {
                logger.Error("URL in configuration file config.json not opened properly. Error: " + ex);
                Utilities.TakeScreenshot(driver, "ConfigJSONContainsConfiguration");
                throw;
            }
        }

        [Test]
        public void TestMainPageLoadedProperly()
        {
            try
            {
                logger.Info("Trying to open Google Search Service");
                mainPage.openPage();
                IWebElement element = driver.FindElement(By.XPath("//span[@class='nM3nl' and text()='Sign up for the Google Cloud newsletter']"));
                string pageEndText = "Sign up for the Google Cloud newsletter";
                string pageFoundText = element.Text;
                Assert.That(pageFoundText, Is.EqualTo(pageEndText), "Page not loaded properly");

            }
            catch (Exception ex)
            {
                logger.Error("Google Search Service Page not opened properly. Error: " + ex);
                Utilities.TakeScreenshot(driver, "MainPageLoadedProperly");
                throw;
            }
        }

        [Test]
        public void TestSearchResultsContainsCalculator()
        {
            try
            {
                logger.Info("Trying to verify results of searching");
                mainPage.openPage();
                searchCalculator.startSearching();
                IWebElement element = driver.FindElement(By.XPath("//b[text()='Google Cloud Pricing Calculator']"));
                Assert.NotNull(element, "Proper search results not found");
            }
            catch (Exception ex)
            {
                logger.Error("Not found expected search results. Error: " + ex);
                Utilities.TakeScreenshot(driver, "SearchResultsContainsCalculator");
                throw;
            }
        }

        [Test]
        public void TestCloudCalculatorLoadedProperly()
        {
            try
            {
                logger.Info("Trying to check if Google Cloud Pricing Calculator was loaded properly");
                mainPage.openPage();
                searchCalculator.startSearching();
                IWebElement element = driver.FindElement(By.XPath("//b[text()='Google Cloud Pricing Calculator']"));
                Assert.NotNull(element, "Google Cloud Pricing Calculator not loaded");
            }
            catch (Exception ex)
            {
                logger.Error("Google Cloud Pricing Calculator not loaded. Error: " + ex);
                Utilities.TakeScreenshot(driver, "CloudCalculatorLoadedProperly");
                throw;
            }
        }

        [Test]
        public void TestMailServiceIsOpening()
        {
            try
            {
                logger.Info("Trying to check opening of mail service");
                yopmailPage.openPage();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until((d) => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                Assert.Pass();
            }
            catch (Exception ex)
            {
                logger.Error("Mail service not loaded. Error: " + ex);
                Utilities.TakeScreenshot(driver, "MailServiceIsOpening");
                throw;
            }
        }
    }
}


