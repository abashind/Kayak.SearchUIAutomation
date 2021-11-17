using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace JuribaKayak.SearchUIAutomation.PageObjects.Controls
{
    public class Flight : BaseControl
    {
        public Flight(By locator) : base(locator) { }

        #region Fields and properties.

        public string From => el.FindElement(fromLocator).Text;

        public string To => el.FindElement(toLocator).Text;

        public string DepartTime => throw new NotImplementedException("Getting DepartTime isn't implemented.");

        public string ArrivalTime => throw new NotImplementedException("Getting ArrivalTime isn't implemented.");

        public IEnumerable<string> Companies => throw new NotImplementedException("Getting Companies isn't implemented.");

        private By fromLocator = By.CssSelector("[id$=\"origin-airport\"]");

        private By toLocator = By.CssSelector("[id$=\"destination-airport\"]");

        #endregion

        #region Methods

        #endregion
    }
}
