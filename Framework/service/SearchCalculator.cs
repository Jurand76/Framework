using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Framework.model;
using Framework.page;

namespace Framework.service
{
    public class SearchCalculator
    {
        private readonly string searchText = "Google Cloud Platform Pricing Calculator";
        readonly IWebDriver driver = DriverSingleton.getDriver();
       
        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchInput;

        [FindsBy(How = How.ClassName, Using = "mb2a7b")]
        private IWebElement searchButton;

        [FindsBy(How = How.XPath, Using = "//b[text()='Google Cloud Pricing Calculator']")]
        private IWebElement googleCloudPricingCalculatorLink;

        [FindsBy(How = How.ClassName, Using = "gcsc-find-more-on-google")]
        private IWebElement findMoreOnGoogleLink;

        public SearchCalculator()
        {
            PageFactory.InitElements(driver, this);
        }

        public void startSearching()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(searchButton)).Click();
            searchInput.SendKeys(searchText + Keys.Enter);
        }

        public bool isResultVisible()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(findMoreOnGoogleLink));
                return googleCloudPricingCalculatorLink.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void enterSearchLink()
        {
            googleCloudPricingCalculatorLink.Click();
        }

        public bool isGoogleCalculatorVisible()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.SwitchTo().DefaultContent();
                wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[contains(@src, 'cloud.google.com/frame/products/calculator')]")));
                return true;
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine("Timeout while waiting for iframe: " + ex.Message);
                return false;
            }
            finally
            {
                driver.SwitchTo().DefaultContent();
            }
        }
    }
}
