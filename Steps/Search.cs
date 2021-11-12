using JuribaKayak.SearchUIAutomation.PageObjects;
using TechTalk.SpecFlow;

namespace JuribaKayak.SearchUIAutomation.Steps
{
    [Binding]
    public sealed class Search
    {
        [Given(@"the next amount of travelers wants to fly out:")]
        public void GivenTheNextAmountOfTravelersWantsToFlyOut(Table table)
        {
            var resultPage = new SearchResultsPage()
                .Open<SearchResultsPage>("https://www.kayak.com/flights/UFA-MOW/2021-11-19/2021-11-20/1adults/1seniors/children-17?sort=bestflight_a");
            resultPage.CloseBrowser();
        }

        [Given(@"the rest of search field are filled out with the next values:")]
        public void GivenTheRestOfSearchFieldAreFilledOutWithTheNextValues(Table table)
        {
        }

        [When(@"search button is pressed")]
        public void WhenSearchButtonIsPressed()
        {
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
