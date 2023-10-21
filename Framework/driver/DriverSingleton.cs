using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Framework.model;

namespace Framework.driver
{
    public class DriverSingleton
    {
        private static IWebDriver? driver;

        private DriverSingleton() {}
        
        public static IWebDriver getDriver()
        {
            if (null == driver)
            {
                ConfigModel configModel = ConfigModel.GetConfiguration();
                
                var browser = configModel.Browser;
               
                switch (browser)
                {
                    case "Chrome":
                        driver = new ChromeDriver();
                        break;
                    case "Edge":
                        driver = new EdgeDriver();
                        break;
                    default:
                        throw new ArgumentException($"Browser not yet implemented: {browser}");
                }
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
