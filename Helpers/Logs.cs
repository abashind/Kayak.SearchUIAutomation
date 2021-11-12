using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using OpenQA.Selenium;

namespace JuribaKayak.SearchUIAutomation.Helpers
{
    /// <summary>
    /// Custom logging class - CLogs.
    /// </summary>
    internal static class CLogs
    {
        /// <summary>
        /// Write the message with step definition to TeamCity metadata.
        /// </summary>
        /// <param name="value"></param>
        public static void Step(string value)
        {
            Console.WriteLine($"##teamcity[testMetadata value='{value}']");
            Debug.WriteLine($"STEP:  {value}");
        }

        /// <summary>
        /// Writes the message to the Build Log in TeamCity.
        /// </summary>
        /// <param name="value"></param>
        public static void Info(string value)
        {
            var message = $"  INFO:  {value}";
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        /// <summary>
        /// Write the message with an error to TeamCity metadata.
        /// </summary>
        /// <param name="value"></param>
        public static void Error(string value)
        {
            Console.WriteLine($"##teamcity[testMetadata value='Error!: {value}']");
            Debug.WriteLine($"    ERROR:  {value}");
        }

        /// <summary>
        /// Takes a screen shot from browser and safes it to artifacts\screenshots directory.
        /// And writes the screen shot to TeamCity metadata.
        /// </summary>
        /// <param name="driver"></param>
        public static void Screen(IWebDriver driver)
        {
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
            var name = $"screenShot_{DateTime.UtcNow.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture)}.png";
            var curDir = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(Path.Combine(curDir, "screenshots"));
            screenShot.SaveAsFile(Path.Combine(curDir, "screenshots", name));
            Console.WriteLine($"##teamcity[testMetadata type='image' value='screenshots/{name}']");
        }
    }
}