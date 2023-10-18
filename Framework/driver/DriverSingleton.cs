using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework.driver
{
    public class DriverSingleton
    {
        private static IWebDriver? driver;

        private DriverSingleton() { }

        public static IWebDriver getDriver()
        {
            if (null == driver)
            {
                driver = new ChromeDriver();
            }
       
            return driver;
        }

        public static void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
