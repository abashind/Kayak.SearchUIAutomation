using FluentAssertions;
using FluentAssertions.Execution;
using JuribaKayak.SearchUIAutomation.Helpers;
using JuribaKayak.SearchUIAutomation.Models;
using JuribaKayak.SearchUIAutomation.PageObjects;
using System.Collections.Generic;
using System.Linq;
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
        private SearchResultsPage resultPage;

        #endregion

        #region Steps

        [Given(@"the next amount of travelers wants to fly out:")]
        public void GivenTheNextAmountOfTravelersWantsToFlyOut(Table table) =>
            travelers = table.CreateSet<AmountOfTravelers>();

        [Given(@"the rest of search field are filled out with the next values:")]
        public void GivenTheRestOfSearchFieldAreFilledOutWithTheNextValues(Table table) =>
            fps = table.CreateInstance<FlightParams>();

        [When(@"search button is pressed")]
        public void WhenSearchButtonIsPressed() => 
            resultPage = new SearchResultsPage()
                .Open<SearchResultsPage>(MiscUtils.BuildRequest(fps, travelers));

        [Then(@"every item on the outcome first page has '(.*)' flights")]
        public void ThenEveryItemOnTheOutcomeFirstPageHasFlights(int numberOfFlights)
        {
            using (new AssertionScope())
            {
                foreach (var item in resultPage.TicketItems)
                {
                    CLogs.Info($"Check number of flights for `{item.Name}` ticket item.");
                    item.NumberOfFlights()
                        .Should().Be(numberOfFlights, "Found ticket items should have designated number of flights.");
                }
            }
        }

        [Then(@"the flight/s have the right direction")]
        public void ThenTheFlightHasTheRightDirection()
        {
        }

        [Then(@"every item on the outcome first page has '(.*)' seat")]
        public void ThenEveryItemOnTheOutcomeFirstPageHasSeat(int numberOfSeats)
        {
            using (new AssertionScope())
            {
                foreach (var item in resultPage.TicketItems)
                {
                    CLogs.Info($"Check number of seats for `{item.Name}` ticket item.");
                    item.NumberOfSeats
                        .Should().Be(numberOfSeats, "Found ticket items should have designated number of seats.");
                }
            }
        }

        [Then(@"no duplicates - not sure I need it")]
        public void ThenNoDuplicates_NotSureINeedIt()
        {
        }

        #endregion
    }
}
