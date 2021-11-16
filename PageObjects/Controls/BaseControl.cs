using System;
using System.Threading;
using JuribaKayak.SearchUIAutomation.Helpers;
using JuribaKayak.SearchUIAutomation.Hooks;
using OpenQA.Selenium;

namespace JuribaKayak.SearchUIAutomation.PageObjects.Controls
{
    public abstract class BaseControl
    {
        protected BaseControl(By locator)
        {
            Locator = locator;
            Driver = DriverSetup.CurrentDriver;
        }

        #region Fields and properties

        public readonly By Locator;

        protected readonly IWebDriver Driver;

        protected IWebElement WebElement => Driver.FindElementT(Locator);

        protected int AttemptsCount;

        #endregion

        #region Methods

        public void Click(int attempts = 10, int attemptDelay = 500) =>
            DoActionWithControl(() => WebElement.Click(), attempts, attemptDelay);

        protected void DoActionWithControl(Action action, int attempts = 10, int attemptDelay = 500)
        {
            try
            {
                action.Invoke();
                AttemptsCount = 0;
            }
            catch (Exception e) when (e is ElementNotInteractableException || e is StaleElementReferenceException)
            {
                if (AttemptsCount < attempts)
                {
                    AttemptsCount++;
                    Thread.Sleep(TimeSpan.FromMilliseconds(attemptDelay));
                    Click(attempts, attemptDelay);
                }
                else
                    throw new UiException($"Control {GetType().Name} with locator {Locator} is bad." +
                                                       $"\n The reason is: {e.Message}");
            }
            catch (WebDriverTimeoutException e)
            {
                throw new UiException($"Control {GetType().Name} with locator {Locator} was not found." +
                    $"\n The reason is: {e.Message}" +
                    $"\n {e.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Check if element exists.
        /// </summary>
        /// <param name="waitElementIs">Seconds.</param>
        /// <param name="waitElementIsVisible">Seconds.</param>
        /// <returns></returns>
        public bool Exists(int waitElementIs = 10, int waitElementIsVisible = 10)
        {
            var result = false;
            try
            {
                var el = Driver.FindElementT(Locator, waitElementIs);
                CLogs.Info($"Element with locator {Locator} has been found.");
                for (int i = 0; i < waitElementIsVisible; i++)
                {
                    result = el.Displayed && el.Enabled;
                    if (result) break;
                    CLogs.Info($"Element with locator {Locator} is not visible, waiting.");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception) { }

            CLogs.Info($"Checked that element {this.GetType().Name} exists and visible. Result {result}.");
            return result;
        }

        #endregion
    }
}
