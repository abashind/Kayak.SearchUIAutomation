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
            using (new AssertionScope())
            {
                foreach (var item in resultPage.TicketItems)
                {
                    CLogs.Info($"Checking To flight.");
                    item.Flights.ToArray()[0].From.Should().Be(fps.From,
                        "The To flight should have right origin airport.");
                    item.Flights.ToArray()[0].To.Should().Be(fps.To,
                        "The To flight should have right destination airport.");

                    if (fps.WhenBack == "--") continue;

                    CLogs.Info($"Checking Back flight.");
                    item.Flights.ToArray()[1].From.Should().Be(fps.To,
                        "The Back flight should have right origin airport.");
                    item.Flights.ToArray()[1].To.Should().Be(fps.From,
                        "The Back flight should have right destination airport.");
                }
            }
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

        #endregion
    }
}
