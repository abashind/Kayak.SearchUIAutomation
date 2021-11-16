using System;
using JuribaKayak.SearchUIAutomation.Helpers;
using JuribaKayak.SearchUIAutomation.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace JuribaKayak.SearchUIAutomation.PageObjects
{
    [Binding]
    public abstract class BasePage
    {
        protected IWebDriver Driver;

        protected BasePage()
        {
            Driver = DriverSetup.CurrentDriver;
        }

        #region Fields and properties

        private By _checkLocator;

        public By CheckLocator
        {
            get => _checkLocator ?? throw new Exception("Locator for checking if a page exists was not initialized.");
            set => _checkLocator = value;
        }

        protected Type PageTypeInfo => GetType();

        public string Name => PageTypeInfo.Name;

        #endregion

        #region Methods

        /// <summary>
        /// Check if the page is opened and its checking element is downloaded.
        /// So be careful assigning CheckLocator, it should be unique for the page.
        /// </summary>
        /// <param name="secondsToWait"></param>
        /// <returns></returns>
        public virtual bool Exists(int secondsToWait = 20)
        {
            var result = false;
            try
            {
                Driver.FindElementT(CheckLocator, secondsToWait);
                result = true;
            }
            catch (Exception) { }

            CLogs.Info($"Tried to check if {Name} was opened. Result {result}.");
            return result;
        }

        public void CloseBrowser()
        {
            CLogs.Info("Closing the WebDriver!");
            Driver.Quit();
        }

        public T Open<T>(string url) where T : BasePage
        {
            CLogs.Info($"Opening page {url}");

            if (Driver == null)
                throw new Exception("Selenium WebDriver was not initialized, " +
                    "check Hooks.DriverSetup.InitializeScenarioLiveDriver attributes");
            Driver.Navigate().GoToUrl(url);
            if (Exists())
            {
                CLogs.Info("The page was successfuly open.");
                return (T)this;
            }

            throw new UiException($"{Name} was not open.");
        }

        #endregion
    }
}
