using OpenQA.Selenium;

namespace JuribaKayak.SearchUIAutomation.PageObjects
{
    class SearchResultsPage : BasePage
    {
        public SearchResultsPage() : base()
        {
            this.CheckLocator = By.Id("searchResultsList");
        }
    }
}
//Design:

//Wait show more results button.

//Find all ticket items elements.

//Transform every element to TicketItem POM.

//TicketItem has: NumberOfFlights(return int), GetFlights(return Flight POM), NumberOfSeats(return int).

//Flight has: direction, depart time, arrival time, company.

//Do checks.

