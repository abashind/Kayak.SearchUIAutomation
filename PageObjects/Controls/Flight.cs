using OpenQA.Selenium;
using System.Collections.Generic;

namespace JuribaKayak.SearchUIAutomation.PageObjects.Controls
{
    public class Flight : BaseControl
    {
        public Flight(By locator) : base(locator) { }

        #region Fields and properties.

        public string Direction;

        public string DepartTime;

        public string ArrivalTime;

        public IEnumerable<string> Companies;

        #endregion

        #region Method

        #endregion
    }
}
