using JuribaKayak.SearchUIAutomation.PageObjects.Controls;
using OpenQA.Selenium;

namespace JuribaKayak.SearchUIAutomation.PageObjects
{
    class SearchResultsPage : BasePage
    {
        public SearchResultsPage() : base()
        {
            this.CheckLocator = By.Id("searchResultsList");
            this.MoreButton = new Button(By.CssSelector("[id$=\"loadMore\"]"));
        }

        #region Fields and properties

        public Button MoreButton;

        #endregion

        #region Methods

        /// <summary>
        /// Example of the polymorphism.
        /// </summary>
        /// <param name="secondsToWait"></param>
        /// <returns></returns>
        public override bool Exists(int secondsToWait = 20) =>
            base.Exists(secondsToWait) && MoreButton.Exists();

        #endregion
    }
}
//Design:

//Find all ticket items elements.

//Transform every element to TicketItem POM.

//TicketItem has: NumberOfFlights(return int), GetFlights(return Flight POM), NumberOfSeats(return int).

//Flight has: direction, depart time, arrival time, company.

//Do checks.

