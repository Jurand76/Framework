﻿using System;
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

        public UseCalculator()
        {
            PageFactory.InitElements(driver, this);
            instance = new ComputingInstance();
        }

        public void FillCalculatorFields()
        {
            driver.SwitchTo().DefaultContent();

            // close pop-up window from bottom of site
            if (devsiteSnackbarButton.Displayed)
            {
                devsiteSnackbarButton.Click();
            }

            // switch to second iframe
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[contains(@src, 'cloud.google.com/frame/products/calculator')]")));
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));
            
            // enter quantity
            quantityInput.Click();
            quantityInput.Clear();
            quantityInput.SendKeys(instance.getNumberOfInstances());
            
            // enter series type
            seriesInput.Click();
            seriesInput.SendKeys(instance.getSeries());

            // choose machine type
            machineTypeInput.Click();
            IWebElement chooseMachineTypeInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//div[contains(text(), '{instance.getMachineType()}')]")));
            chooseMachineTypeInput.Click();

            // add GPU
            if (instance.getGPUExistence())
            {
                // choose type of GPU
                addGPUsCheckbox.Click();
                clickGPUselection.Click();
                IWebElement typeOfGPU = driver.FindElement(By.XPath($"//div[contains(text(), '{instance.getTypeOfGPU()}')]"));

                // Find the parent <md-option> element of the <div> to confirm GPU type
                IWebElement ancestorOfTypeOfGPU = typeOfGPU.FindElement(By.XPath("./ancestor::md-option"));
                ancestorOfTypeOfGPU.Click();

                // select number of GPU
                numberOfGPUselection.Click();
                
                // find the option with the number of GPU and click it
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

            // add local SDD
            localSSDselection.Click();
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

            IWebElement typeOfSSDoption = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(localSSDselectText)));
            wait.Until(ExpectedConditions.ElementToBeClickable(typeOfSSDoption));
            typeOfSSDoption.Click();

        }
    }
}
