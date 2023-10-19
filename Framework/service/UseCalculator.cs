using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.driver;
using Framework.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace Framework.service
{
    public class UseCalculator
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        readonly ComputingInstance instance;


        [FindsBy(How = How.Id, Using = "input_99")]
        private IWebElement quantityInput;

        [FindsBy(How = How.Name, Using = "series")]
        private IWebElement seriesInput;

        [FindsBy(How = How.Id, Using = "select_126")]
        private IWebElement machineTypeInput;

        [FindsBy(How = How.XPath, Using = "//md-checkbox[@aria-label='Add GPUs']")]
        private IWebElement addGPUsCheckbox;

        public UseCalculator()
        {
            PageFactory.InitElements(driver, this);
            instance = new ComputingInstance();
        }

        public void FillCalculatorFields()
        {
            driver.SwitchTo().DefaultContent();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[contains(@src, 'cloud.google.com/frame/products/calculator')]")));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));
            quantityInput.Click();
            quantityInput.Clear();
            quantityInput.SendKeys(instance.getNumberOfInstances());
            seriesInput.Click();
            seriesInput.SendKeys(instance.getSeries());
            machineTypeInput.Click();
            machineTypeInput.SendKeys(instance.getMachineType());

            if (instance.getGPUExistence())
            {
                addGPUsCheckbox.Click();
            }
        }
    }
}
