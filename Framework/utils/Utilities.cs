using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.model;
using Framework.driver;

namespace Framework.utils
{
    public static class Utilities
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            ConfigModel configModel = ConfigModel.GetConfiguration();

            try
            {
                // Generate filename with date and time
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                string screenshotName = $"{testName}_{timestamp}.png";
                string screenshotDir = configModel.Screenshots_directory;
                string screenshotPath = Path.Combine(screenshotDir, screenshotName);
                
                // Take the screenshot and save it
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

                Console.WriteLine($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while taking screenshot: {ex.Message}");
            }
        }
    }
}
