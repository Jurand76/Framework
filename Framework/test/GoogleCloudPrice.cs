using Framework.driver;
using Framework.model;
using Framework.page;
using Framework.service;
using Framework.utils;
using OpenQA.Selenium;
using NLog;

namespace Framework
{
    public class GoogleCloudPrice
    {
        protected IWebDriver driver;
        protected MainPage mainPage;
        protected ComputingInstance user;
        protected SearchCalculator searchCalculator;
        protected UseCalculator useCalculator;
        protected YopmailPage yopmailPage;
        protected YopMailPageActions yopmailPageActions;
        protected string yopmailAddress = "";
        protected string amountFromEmail = "";
        protected string amountFromEstimation = "";

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
            try
            {
                mainPage.openPage();
                searchCalculator.startSearching();
                Assert.IsTrue(searchCalculator.isResultVisible(), "Search results not visible");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "SearchService");
                throw;
            }
        }

        [Test, Order(1)]
        public void TestOpenCalculator()
        {
            try
            {
                searchCalculator.enterSearchLink();
                Assert.IsTrue(searchCalculator.isGoogleCalculatorVisible(), "Google Calculator is not opened");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "OpenCalculator");
                throw;
            }
        }

        [Test, Order(2)]
        public void TestFillCalculatorFields()
        {
            try
            {
                useCalculator = new UseCalculator();
                useCalculator.FillCalculatorFields();
                Assert.IsTrue(useCalculator.AreEstimatedCostsGenerated(), "Estimated costs not generated");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "FillCalculatorFields");
                throw;
            }
        }

        [Test, Order(3)]
        public void TestGetTotalCostFromEstimationCard()
        {
            try
            {
                amountFromEstimation = useCalculator.GetTotalCostFromEstimation();
                Console.WriteLine("Cost by est: " + amountFromEstimation);
                Assert.IsNotEmpty(amountFromEstimation, "Total cost from estimation card hasn't been read");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "GetTotalCostFromEstimationCard");
                throw;
            }
        }

        [Test, Order(4)]
        public void TestGenerateMail()
        {
            try
            {
                yopmailPageActions.CreateNewTab();
                yopmailPage.openPage();
                yopmailPageActions.CloseCookiesPopup();
                yopmailPageActions.GenerateMail();
                yopmailAddress = yopmailPageActions.GetGeneratedMail();
                yopmailPageActions.SwitchToPreviousTab();
                Assert.IsNotEmpty(yopmailAddress, "Yopmail address not generated");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "GenerateMail");
                throw;
            }
            
        }

        [Test, Order(5)]
        public void TestSendEmailWithCosts()
        {
            try
            {
                Assert.IsTrue(useCalculator.SendEstimatedCostByMail(yopmailAddress), "Error during sending mail");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "SendEmailWithCosts");
                throw;
            }
        }

        [Test, Order(6)]
        public void TestReadReceivedEmailAndGetAmount()
        {
            try
            {
                Thread.Sleep(3000);    // wait 3 seconds for mail
                yopmailPageActions.SwitchToNextTab();
                amountFromEmail = yopmailPageActions.CheckEmailBoxAndReceiveAmount();
                Console.WriteLine("Cost from mail: " + amountFromEmail);
                Assert.IsNotEmpty(amountFromEmail, "Total cost from email hasn't been read");
            }
            catch (Exception ex)
            {
                Utilities.TakeScreenshot(driver, "ReadReceivedEmailAndGetAmount");
                throw;
            }
        }

        [Test, Order(7)]
        public void TestCompareAmountsFromEstimationAndMail()
        {
            try
            {
                Assert.That(amountFromEmail, Is.EqualTo(amountFromEstimation));
            }
            catch
            {
                Utilities.TakeScreenshot(driver, "CompareAmountFromEstimationAndMail");
                throw;
            }
        }
    }
}