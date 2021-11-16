using JuribaKayak.SearchUIAutomation.Helpers;
using JuribaKayak.SearchUIAutomation.Models;
using JuribaKayak.SearchUIAutomation.PageObjects;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace JuribaKayak.SearchUIAutomation.Steps
{
    [Binding]
    public sealed class Search
    {
        #region Fields and properties

        private IEnumerable<AmountOfTravelers> travelers;
        private FlightParams fps;

        #endregion

        #region Steps

        #endregion

        [Given(@"the next amount of travelers wants to fly out:")]
        public void GivenTheNextAmountOfTravelersWantsToFlyOut(Table table) =>
            travelers = table.CreateSet<AmountOfTravelers>();

        [Given(@"the rest of search field are filled out with the next values:")]
        public void GivenTheRestOfSearchFieldAreFilledOutWithTheNextValues(Table table) =>
            fps = table.CreateInstance<FlightParams>();

        [When(@"search button is pressed")]
        public void WhenSearchButtonIsPressed()
        {
            var resultPage = new SearchResultsPage()
                .Open<SearchResultsPage>(MiscUtils.BuildRequest(fps, travelers));
        }

        [Then(@"every item on the outcome first page has '(.*)' flights")]
        public void ThenEveryItemOnTheOutcomeFirstPageHasFlights(int p0)
        {
        }

        [Then(@"the flight has the right direction")]
        public void ThenTheFlightHasTheRightDirection()
        {
        }

        [Then(@"every item on the outcome first page has '(.*)' seat")]
        public void ThenEveryItemOnTheOutcomeFirstPageHasSeat(int p0)
        {
        }
    }
}
