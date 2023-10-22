using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Framework.driver;
using Framework.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using NLog;

namespace Framework.service
{
    public class UseCalculator
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        readonly ComputingInstance instance;
        private readonly WebDriverWait wait;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        [FindsBy(How = How.Id, Using = "input_99")]
        private IWebElement quantityInput;

        [FindsBy(How = How.Name, Using = "series")]
        private IWebElement seriesInput;

        [FindsBy(How = How.Id, Using = "select_126")]
        private IWebElement machineTypeInput;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'Add GPUs')]")]
        private IWebElement addGPUsCheckbox;

        [FindsBy(How = How.ClassName, Using = "devsite-snackbar-action")]
        private IWebElement devsiteSnackbarButton;

        [FindsBy(How = How.Id, Using = "select_506")]
        private IWebElement clickGPUselection;

        [FindsBy(How = How.Id, Using = "select_508")]
        private IWebElement numberOfGPUselection;

        [FindsBy(How = How.Id, Using = "select_465")]
        private IWebElement localSSDselection;

        [FindsBy(How = How.Id, Using = "select_132")]
        private IWebElement datacenterSelection;

        [FindsBy(How = How.Id, Using = "select_139")]
        private IWebElement committedUsageSelection;

        [FindsBy(How = How.CssSelector, Using = ".md-raised.md-primary.cpc-button")]
        private IWebElement addToEstimateButton;

        [FindsBy(How = How.Id, Using = "Email Estimate")]
        private IWebElement sendEmailPopupButton;

        [FindsBy(How = How.Id, Using = "input_616")]
        private IWebElement fieldForEmailAddress;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(), 'Send Email')]")]
        private IWebElement sendEmailButton;
     

        public UseCalculator()
        {
            PageFactory.InitElements(driver, this);
            instance = new ComputingInstance();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SwitchToSecondIframe()
        {
            driver.SwitchTo().DefaultContent();
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[contains(@src, 'cloud.google.com/frame/products/calculator')]")));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));
        }

        public void FillCalculatorFields()
        {
            driver.SwitchTo().DefaultContent();

            // close pop-up window from bottom of site
            logger.Info("Closing popup window at calculator page");
            if (devsiteSnackbarButton.Displayed)
            {
                devsiteSnackbarButton.Click();
            }

            // switch to second iframe
            SwitchToSecondIframe();

            // enter quantity
            logger.Info("Entering quantity: " + instance.getNumberOfInstances());
            quantityInput.Click();
            quantityInput.Clear();
            quantityInput.SendKeys(instance.getNumberOfInstances());

            // enter series type
            logger.Info("Entering series: " + instance.getSeries());
            seriesInput.Click();
            seriesInput.SendKeys(instance.getSeries());

            // choose machine type
            logger.Info("Entering machine type: " + instance.getMachineType());
            machineTypeInput.Click();
            IWebElement chooseMachineTypeInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(text(), '{instance.getMachineType()}')]")));
            chooseMachineTypeInput.Click();

            // add GPU
            if (instance.getGPUExistence())
            {
                // scroll
                logger.Info("Entering GPU");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", addGPUsCheckbox);
                Actions scroll1 = new Actions(driver);
                scroll1.MoveToElement(addGPUsCheckbox).Click().Perform();

                // choose type of GPU
                logger.Info("Choosing type of GPU: " + instance.getTypeOfGPU());
                addGPUsCheckbox.Click();
                clickGPUselection.Click();
                IWebElement typeOfGPU = driver.FindElement(By.XPath($"//div[contains(text(), '{instance.getTypeOfGPU()}')]"));
                IWebElement ancestorOfTypeOfGPU = typeOfGPU.FindElement(By.XPath("./ancestor::md-option"));
                ancestorOfTypeOfGPU.Click();

                // select number of GPU
                numberOfGPUselection.Click();

                // find the option with the number of GPU and click it
                logger.Info("Choosing number of GPU: " + instance.getNumberOfGPU());
                int number = instance.getNumberOfGPU();
                string select_option_text = "select_option_515";
                
                if (number == 1)
                {
                    select_option_text = "select_option_516";
                }
                if (number == 2)
                {
                    select_option_text = "select_option_517";
                }
                if (number == 4)
                {
                    select_option_text = "select_option_518";
                }

                IWebElement numberOfGPUoption = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(select_option_text)));
                wait.Until(ExpectedConditions.ElementToBeClickable(numberOfGPUoption));
                numberOfGPUoption.Click();
            }

                        
            // scroll
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", localSSDselection);
            Actions scroll2 = new Actions(driver);
            scroll2.MoveToElement(localSSDselection).Click().Perform();

            // add local SDD
            logger.Info("Adding local SSD: " + instance.getLocalSSD());
            string[] localSSD = instance.getLocalSSD().Split('x');
            string localSSDselectText = "select_option_";

            if (localSSD[0] == "16")
            {
                localSSDselectText = "select_option_498";
            }
            else if (localSSD[0] == "24")
            {
                localSSDselectText = "select_option_499";
            }
            else
            {
                localSSDselectText = localSSDselectText + (Convert.ToInt32(localSSD[0]) + 489).ToString();
            }

            localSSDselection.Click();
            IWebElement typeOfSSDoption = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(localSSDselectText)));
            wait.Until(ExpectedConditions.ElementToBeClickable(typeOfSSDoption));
            typeOfSSDoption.Click();

            // datacenter selection
            logger.Info("Adding datacenter: " + instance.getDatacenter());
            datacenterSelection.Click();
            IWebElement dataCenter = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//md-option//div[contains(text(), '{instance.getDatacenter()}')]")));
            Thread.Sleep(1000);       // needed to proper data center selection !!!
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", dataCenter);

            // committed usage
            logger.Info("Adding committed usage: " + instance.getCommittedUsage());
            committedUsageSelection.Click();
            IWebElement commitedUsage = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//md-option//div[contains(text(), '{instance.getCommittedUsage()}')]")));
            Thread.Sleep(1000);       // needed to proper data committed usage selection !!!
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", commitedUsage);

            // estimate button
            logger.Info("Calculating estimation costs");
            addToEstimateButton.Click();
        }

        public bool AreEstimatedCostsGenerated()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath($"//b[contains(text(), 'Total Estimated Cost:')]")));
                return true;
            }
            catch (WebDriverTimeoutException) 
            {
                return false;
            }
        }

        public string GetTotalCostFromEstimation()
        {
            string priceText = "";

            try
            {
                IWebElement priceElement = driver.FindElement(By.XPath("//div[@class='cpc-cart-total']//b[contains(text(), 'USD')]"));
                
                Match match = Regex.Match(priceElement.Text, @"USD\s([\d,]+\.\d{2})");
                if (match.Success)
                {
                    priceText = "USD " + match.Groups[1].Value;
                    logger.Info("Estimation costs from google calculator: " + priceText);
                }
            }
            catch (NoSuchElementException ex)
            {
                logger.Error("Estimation costs not found. Error: " + ex);
            }

            return priceText;
        }

        public bool SendEstimatedCostByMail(string email)
        {
            SwitchToSecondIframe();
            logger.Info("Sending email with estimation costs");
            wait.Until(ExpectedConditions.ElementToBeClickable(sendEmailPopupButton));
            sendEmailPopupButton.Click();
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(fieldForEmailAddress));
                fieldForEmailAddress.Click();
                fieldForEmailAddress.SendKeys(email);
                sendEmailButton.Click();
                return true;
            }
            catch (WebDriverTimeoutException ex) 
            {
                logger.Error("Mail hasn't been sent. Error: " + ex);
                return false;
            }
        }
    }
}
