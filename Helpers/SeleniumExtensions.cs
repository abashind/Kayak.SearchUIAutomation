using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ec = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace JuribaKayak.SearchUIAutomation.Helpers
{
    public static class SeleniumExtensions
    {
        #region Methods

        private static IWebElement FindElement(this IWebDriver driver, Func<IWebDriver, IWebElement> condition, int seconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(condition);
        }

        /// <summary>
        /// Find an element in some time.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        /// <param name="locator">Locator for search.</param>
        /// <param name="seconds">Time for search.</param>
        /// <returns>Found element.</returns>
        public static IWebElement FindElementT(this IWebDriver driver, By locator, int seconds)
        {
            return FindElement(driver, Ec.ElementExists(locator), seconds);
        }

        /// <summary>
        /// Find an element in 5 seconds.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        /// <param name="locator">Locator for search.</param>
        /// <returns></returns>
        public static IWebElement FindElementT(this IWebDriver driver, By locator)
        {
            return driver.FindElementT(locator, 5);
        }

        /// <summary>
        /// Figure out element is stale or not.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsStale(this IWebElement element)
        {
            try
            {
                return element == null || !element.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }

        /// <summary>
        /// Wait until element is invisible.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="secondsWaitTillElementIsHere"></param>
        /// <returns></returns>
        public static Func<IWebDriver, bool> InvisibilityOfElementLocated(By locator, int secondsWaitTillElementIsHere) => driver =>
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsWaitTillElementIsHere));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                var result = wait.Until(Ec.ElementExists(locator)).Displayed;
                return result;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return true;
            }
        };

        /// <summary>
        /// Find a WebElement with some text.
        /// This method is useful when there are several elements with the same locator .
        /// And the only way to get one the desired is to search by the text.
        /// </summary>
        /// <param name="driver">WedDriver where to search.</param>
        /// <param name="locator">WebElement locator.</param>
        /// <param name="text">A text for searching by.</param>
        /// <param name="intervalInMilliSecs">How much time sleep between attempts.</param>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public static IWebElement FindElementWithText(this IWebDriver driver, By locator, string text, int intervalInMilliSecs = 500, int attempts = 20)
        {
            for (var i = 0; i < attempts; i++)
            {
                var elements = driver.FindElements(locator);
                if (!elements.Any()) continue;
                var element = elements.FirstOrDefault(
                    e => e.FindElement(By.CssSelector("[class^=\"ListViewStyles__admissionItemName___\"]"))
                    .Text.Equals(text));
                if (element != null) return element;

                Thread.Sleep(intervalInMilliSecs);
            }

            throw new Exception(
                $"The element with locator - `{locator}` and text - `{text}` was not found in {intervalInMilliSecs * attempts / 1000} seconds.");
        }

        public static bool IsBrowserOpen(this IWebDriver driver)
        {
            try
            {
                var title = driver.Title;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webDrEl">WebDriver or WebElement</param>
        /// <param name="locator">Locator.</param>
        /// <returns></returns>
        public static bool ElementExists(this ISearchContext webDrEl, By locator)
        {
            try
            {
                webDrEl.FindElement(locator); 
            }
            catch (NoSuchElementException)
            {
                return false; 
            }
            return true;
        }

        #endregion

    }
}
