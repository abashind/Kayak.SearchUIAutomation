using System.Drawing;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecRun;
using JuribaKayak.SearchUIAutomation.Helpers;
using System;

namespace JuribaKayak.SearchUIAutomation.Hooks
{
    [Binding]
    public class DriverSetup
    {
        private readonly TestRunContext _testRunContext;

        public static IWebDriver CurrentDriver;

        public DriverSetup(TestRunContext testRunContext) =>
            _testRunContext = testRunContext;

        internal IWebDriver CreateChromeDriver()
        {
            IWebDriver driver;

            var chDir = _testRunContext.TestDirectory;
            if (Constants.HeadlessMode)
            {
                var chOpt = new ChromeOptions();
                chOpt.AddArgument("headless");
                driver = new ChromeDriver(chDir, chOpt);
                driver.Manage().Window.Size = new Size(1920, 1080);
            }
            else
            {
                driver = new ChromeDriver(chDir);
                driver.Manage().Window.Maximize();
            }
            return driver;
        }

        internal IWebDriver CreateFFDriver() =>
            throw new NotImplementedException("Getting Firefox driver method should be implemented before using it.");

        [BeforeScenario]
        public void InitializeDriver()
        {
            CLogs.Info($"Initializing Chrome ");
            if (Constants.CurrentDriverType == Models.Drivers.Chrome)
                CurrentDriver = CreateChromeDriver();
            else if (Constants.CurrentDriverType == Models.Drivers.FireFox)
                CurrentDriver = CreateFFDriver();
            else
                throw new ArgumentException("Wrong driver type was passed into InitializeDriver driver");
                
        }
    }
}
